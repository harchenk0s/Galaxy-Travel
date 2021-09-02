using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ChangeShipEvent : UnityEvent<GameObject> { }

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Generator _garbageGenerator = null;

    private List<Wave> _waves = new List<Wave>();
    private Wave _gateWave;
    private IEnumerator _generationCourutine;
    private Ship _ship;
    private bool _addGates = false;
    private GameMode _gameMode;

    public UnityEvent EndLevelEvent;
    public ChangeShipEvent ChangeShipEvent;

    private void Awake()
    {
        _ship = Resources.Load<Ship>(PlayerPrefs.GetString("Ship"));
        _ship = Instantiate(_ship, Vector3.zero, Quaternion.identity);
        _gameMode = Resources.Load<GameMode>(PlayerPrefs.GetString("Mode"));
        _gameMode = Instantiate(_gameMode, Vector3.zero, Quaternion.identity);
        ChangeShipEvent.Invoke(_ship.gameObject);
        _ship.ShipDeadEvent.AddListener(GameOver);
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
    }

    public void ChangeGameMode(GameMode gameMode)
    {
        Destroy(_gameMode.gameObject);
        _gameMode = gameMode;
    }

    public void StartGenerate()
    {
        _waves = _gameMode.GetWaves();
        _addGates = _gameMode.AddGates;
        _generationCourutine = GenerationCourutine();
        StartCoroutine(_generationCourutine);
    }

    public void ChangeShip(GameObject ship)
    {
        if (ship.TryGetComponent<Ship>(out _))
        {
            _ship.gameObject.SetActive(false);
            PlayerPrefs.SetString("Ship", ship.name);
            PlayerPrefs.Save();
            GameObject newShip = Instantiate(ship, Vector3.zero, Quaternion.identity);
            Debug.Log(PlayerPrefs.GetString("Ship"));
            Destroy(_ship.gameObject);
            _ship = newShip.GetComponent<Ship>();
            ChangeShipEvent.Invoke(_ship.gameObject);
            
        }
    }

    private IEnumerator GenerationCourutine()
    {
        _ship.StartMove();

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

        EndLevelEvent.Invoke();
        _waves.Clear();
        _ship.ResetShip();
    }

    private void GameOver()
    {
        StopCoroutine(_generationCourutine);
        _waves.Clear();
        _ship.ResetShip();
    }
}
