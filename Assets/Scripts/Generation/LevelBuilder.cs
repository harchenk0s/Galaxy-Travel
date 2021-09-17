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

    public UnityEvent GameStarted;
    public UnityEvent LevelEnded;
    public UnityEvent Defeated;
    public GameObjectEvent ShipChanged;
    public GameObjectEvent GameModeChanged;

    public void ChangeGameMode(GameMode gameMode)
    {
        PlayerPrefs.SetString(Strings.PlayerPrefs.Mode, gameMode.name);
        PlayerPrefs.Save();
        GameModeChanged.Invoke(gameMode.gameObject);
        Destroy(_gameMode.gameObject);
        _gameMode = gameMode;
        _ship.StartSpeed = _gameMode.StartSpeedProperty;
    }

    public void ChangeShip(Ship ship)
    {
        _ship.gameObject.SetActive(false);
        PlayerPrefs.SetString(Strings.PlayerPrefs.Ship, ship.gameObject.name);
        PlayerPrefs.Save();
        GameObject newShip = Instantiate(ship.gameObject, Vector3.zero, Quaternion.identity);
        Destroy(_ship.gameObject);
        _ship = newShip.GetComponent<Ship>();
        _ship.name = ship.name;
        _ship.StartSpeed = _gameMode.StartSpeedProperty;
        ShipChanged.Invoke(_ship.gameObject);
        _ship.ShipDied.AddListener(Defeat);
    }

    public void StartGenerate()
    {
        _waves = _gameMode.GetWaves();
        _addGates = _gameMode.AddGatesProperty;
        _ship.StartMove();
        _generationCourutine = GenerationCourutine();
        GameStarted.Invoke();
        StartCoroutine(_generationCourutine);
    }

    public void Reset()
    {
        _waves.Clear();
        _ship.Reset();
    }

    private void Awake()
    {
        _ship = Resources.Load<Ship>(PlayerPrefs.GetString(Strings.PlayerPrefs.Ship));
        _ship = Instantiate(_ship, Vector3.zero, Quaternion.identity);
        _ship.name = PlayerPrefs.GetString(Strings.PlayerPrefs.Ship);
        _gameMode = Resources.Load<GameMode>(PlayerPrefs.GetString(Strings.PlayerPrefs.Mode));
        _gameMode = Instantiate(_gameMode, Vector3.zero, Quaternion.identity);
        _gameMode.name = PlayerPrefs.GetString(Strings.PlayerPrefs.Mode);
        ShipChanged.Invoke(_ship.gameObject);
        GameModeChanged.Invoke(_gameMode.gameObject);
        _ship.ShipDied.AddListener(Defeat);
        _ship.StartSpeed = _gameMode.StartSpeedProperty;
    }

    private void GateWaveReset()
    {
        _gateWave = new Wave(typeof(CenterAlg), Strings.AlgorithmsParameters.CenterAlg.CenterAlgDefault, 1f);
    }

    private void Start()
    {
        if (_garbageGenerator == null)
        {
            throw new UnityException("No GarbageGenerator");
        }
        GameModeChanged.Invoke(_gameMode.gameObject);
    }

    private void Defeat()
    {
        Defeated.Invoke();
        StopCoroutine(_generationCourutine);
        _garbageGenerator.StopGenerate();
    }

    private void Win()
    {
        LevelEnded.Invoke();
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
