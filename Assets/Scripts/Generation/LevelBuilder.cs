﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

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

    private void Awake()
    {
        _ship = Resources.Load<Ship>(PlayerPrefs.GetString("Ship"));
        _ship = Instantiate(_ship, Vector3.zero, Quaternion.identity);
        _gameMode = Resources.Load<GameMode>(PlayerPrefs.GetString("Mode"));
        _gameMode = Instantiate(_gameMode, Vector3.zero, Quaternion.identity);
        ChangeShipEvent.Invoke(_ship.gameObject);
        _ship.ShipDeadEvent.AddListener(Defeat);
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

    public void ChangeGameMode(GameMode gameMode)
    {
        Destroy(_gameMode.gameObject);
        _gameMode = gameMode;
        ChangeGameModeEvent.Invoke(gameMode.gameObject);
    }

    public void StartGenerate()
    {
        _waves = _gameMode.GetWaves();
        _addGates = _gameMode.AddGates;
        _generationCourutine = GenerationCourutine();
        StartGameEvent.Invoke();
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
            Destroy(_ship.gameObject);
            _ship = newShip.GetComponent<Ship>();
            ChangeShipEvent.Invoke(_ship.gameObject);
            _ship.ShipDeadEvent.AddListener(Defeat);
        }
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

    private void Reset()
    {
        _waves.Clear();
        _ship.Reset();
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
        Win();
    }
}
