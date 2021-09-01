using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Generator _garbageGenerator;
    [SerializeField] private GameMode _defaultGameMode;

    private List<Wave> _waves = new List<Wave>();
    private Wave _gateWave;
    private IEnumerator _generationCourutine;
    private Ship _ship;
    private bool _addGates = false;
    private GameMode _gameMode;

    public UnityEvent EndLevelEvent;

    private void Awake()
    {
        _ship = FindObjectOfType<Ship>();
        _ship.ShipDeadEvent.AddListener(GameOver);
        _gameMode = _defaultGameMode;
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

    public void SetGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
    }

    public void StartGenerate()
    {
        _waves = _gameMode.GetWaves();
        _addGates = _gameMode.AddGates;
        _generationCourutine = GenerationCourutine();
        StartCoroutine(_generationCourutine);
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
