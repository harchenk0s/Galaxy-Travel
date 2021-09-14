using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Generator _garbageGenerator = null;

    private List<Wave> _waves = new List<Wave>();
    private Wave _gateWave;
    private IEnumerator _generationCourutine;
    private Ship _ship;
    private bool _addGates = false;
    private GameMode _gameMode;

    public UnityEvent StartGameEvent;
    public UnityEvent EndLevelEvent;
    public UnityEvent DefeatEvent;
    public GameObjectEvent ChangeShipEvent;
    public GameObjectEvent ChangeGameModeEvent;

    public void ChangeGameMode(GameMode gameMode)
    {
        PlayerPrefs.SetString("Mode", gameMode.name);
        PlayerPrefs.Save();
        ChangeGameModeEvent.Invoke(gameMode.gameObject);
        Destroy(_gameMode.gameObject);
        _gameMode = gameMode;
        _ship.StartSpeed = _gameMode.StartSpeed;
    }

    public void ChangeShip(GameObject ship)
    {
        if (ship.TryGetComponent<Ship>(out _))
        {
            _ship.gameObject.SetActive(false);
            PlayerPrefs.SetString("Ship", ship.name);
            PlayerPrefs.Save();
            GameObject newShip = Instantiate(ship, Vector3.zero, Quaternion.identity);
            Destroy(_ship.gameObject);
            _ship = newShip.GetComponent<Ship>();
            _ship.name = ship.name;
            _ship.StartSpeed = _gameMode.StartSpeed;
            ChangeShipEvent.Invoke(_ship.gameObject);
            _ship.ShipDeadEvent.AddListener(Defeat);
        }
    }

    public void StartGenerate()
    {
        _waves = _gameMode.GetWaves();
        _addGates = _gameMode.AddGates;
        _ship.StartMove();
        _generationCourutine = GenerationCourutine();
        StartGameEvent.Invoke();
        StartCoroutine(_generationCourutine);
    }

    public void Reset()
    {
        _waves.Clear();
        _ship.Reset();
    }

    private void Awake()
    {
        _ship = Resources.Load<Ship>(PlayerPrefs.GetString("Ship"));
        _ship = Instantiate(_ship, Vector3.zero, Quaternion.identity);
        _ship.name = PlayerPrefs.GetString("Ship");
        _gameMode = Resources.Load<GameMode>(PlayerPrefs.GetString("Mode"));
        _gameMode = Instantiate(_gameMode, Vector3.zero, Quaternion.identity);
        _gameMode.name = PlayerPrefs.GetString("Mode");
        ChangeShipEvent.Invoke(_ship.gameObject);
        ChangeGameModeEvent.Invoke(_gameMode.gameObject);
        _ship.ShipDeadEvent.AddListener(Defeat);
        _ship.StartSpeed = _gameMode.StartSpeed;
    }

    private void GateWaveReset()
    {
        _gateWave = new Wave(typeof(CenterAlg), "CenterAlgDefault", 1f);
    }

    private void Start()
    {
        if (_garbageGenerator == null)
        {
            throw new UnityException("No GarbageGenerator");
        }
        ChangeGameModeEvent.Invoke(_gameMode.gameObject);
    }

    private void Defeat()
    {
        DefeatEvent.Invoke();
        StopCoroutine(_generationCourutine);
        _garbageGenerator.StopGenerate();
    }

    private void Win()
    {
        EndLevelEvent.Invoke();
    }

    private IEnumerator GenerationCourutine()
    {
        foreach (Wave wave in _waves)
        {
            if (_addGates)
            {
                GateWaveReset();
                _gateWave.StartWave(_ship.CurrentPercentSpeed, _garbageGenerator);
                yield return new WaitUntil(() => _gateWave.IsWaveEnd);
            }

            wave.StartWave(_ship.CurrentPercentSpeed, _garbageGenerator);
            yield return new WaitUntil(() => wave.IsWaveEnd);
        }
        Win();
    }
}
