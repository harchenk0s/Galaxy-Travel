using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveGenerator : MonoBehaviour
{
    private List<Wave> _waves = new List<Wave>();
    private GarbageGenerator _generator;
    private IEnumerator _generationCourutine;

    public UnityEvent EndLevelEvent;

    private void Awake()
    {
        _generator = FindObjectOfType<GarbageGenerator>();
    }

    private void Start()
    {
        if(_generator == null)
        {
            throw new UnityException("No GarbageGenerator on scene");
        }

        if (PlayerPrefs.GetInt("FirstLaunch") == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                _waves.Add(new Wave(typeof(GridRandomAlg), "OnlyCows", 10f, _generator));
            }
        }
    }

    public void StartGenerate()
    {
        if(_waves.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                _waves.Add(new Wave(typeof(GridRandomAlg), "GridRandomAlgDefault", 4f, _generator));
            }

            _waves.Add(new Wave(typeof(GridRandomAlg), "OnlyAsteroids", 30f, _generator));
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
            //TODO: Replace wait for seconds to WaitUntil Gates speed up/down ship
        }

        EndLevelEvent.Invoke();
        _waves.Clear();
        _generationCourutine = null;
    }
}
