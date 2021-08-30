using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Generator _garbageGenerator;

    private List<Wave> _waves = new List<Wave>();
    private Wave _gateWave;
    private IEnumerator _generationCourutine;
    private Ship _ship;

    public UnityEvent EndLevelEvent;

    private void Awake()
    {
        _ship = FindObjectOfType<Ship>();
        GateWaveReset();
    }

    private void GateWaveReset()
    {
        _gateWave = new Wave(typeof(CenterAlg), "CenterAlgDefault", 5f);
    }

    private void Start()
    {
        if(_garbageGenerator == null)
        {
            throw new UnityException("No GarbageGenerator");
        }
    }

    public void StartGenerate(GameMode mode)
    {
        _waves = mode.GetWaves();

        if (_generationCourutine == null)
            _generationCourutine = GenerationCourutine();
        else
            StopCoroutine(_generationCourutine);

        StartCoroutine(_generationCourutine);
    }

    private IEnumerator GenerationCourutine()
    {
        foreach (Wave wave in _waves)
        {
            _gateWave.StartWave(100, _garbageGenerator);
            yield return new WaitUntil(() => _gateWave.IsWaveEnd);
            GateWaveReset();
            wave.StartWave(_ship.CurrentPercentSpeed, _garbageGenerator);
            yield return new WaitUntil(() => wave.IsWaveEnd);
            yield return new WaitForSecondsRealtime(4);
        }

        EndLevelEvent.Invoke();
        //TODO: Delete debug logs
        Debug.LogError("END!");
        _waves.Clear();
        _generationCourutine = null;
    }
}
