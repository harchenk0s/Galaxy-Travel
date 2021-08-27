using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] private Generator _garbageGenerator;
    private List<Wave> _waves = new List<Wave>();
    private Wave _gateWave;
    private IEnumerator _generationCourutine;

    public UnityEvent EndLevelEvent;

    private void Awake()
    {
        GateWaveReset();
    }

    private void GateWaveReset()
    {
        _gateWave = new Wave(typeof(CenterAlg), "CenterAlgDefault", 5f, _garbageGenerator);
    }

    private void Start()
    {
        if(_garbageGenerator == null)
        {
            throw new UnityException("No GarbageGenerator");
        }

        if (PlayerPrefs.GetInt("FirstLaunch") == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                _waves.Add(new Wave(typeof(GridRandomAlg), "OnlyCows", 10f, _garbageGenerator));
            }
        }
    }

    public void StartGenerate()
    {
        if(_waves.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                _waves.Add(new Wave(typeof(GridRandomAlg), "GridRandomAlgDefault", 4f, _garbageGenerator));
            }

            _waves.Add(new Wave(typeof(GridRandomAlg), "OnlyAsteroids", 30f, _garbageGenerator));
        }
        else
        {
            PlayerPrefs.SetInt("FirstLaunch", 0);
            PlayerPrefs.Save();
        }

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
            wave.StartWave();
            yield return new WaitUntil(() => wave.IsWaveEnd);
            yield return new WaitForSecondsRealtime(4);
            _gateWave.StartWave();
            yield return new WaitUntil(() => _gateWave.IsWaveEnd);
            GateWaveReset();
        }

        EndLevelEvent.Invoke();
        _waves.Clear();
        _generationCourutine = null;
    }
}
