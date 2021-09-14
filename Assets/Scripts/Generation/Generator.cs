using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Pool))]
public class Generator : MonoBehaviour
{
    [SerializeField] private WaveEnder _waveEnder = null;

    private GenerationAlgorithm _algorithm = null;
    private Pool _pool;
    private IEnumerator _generationCorutine;

    public UnityEvent EndWaveEvent;

    public void GenerateWave(float duration)
    {
        _pool.CreateObjects(_algorithm.GetPrefabsList());
        _generationCorutine = GeneratingWaveCorutine(duration);
        StartCoroutine(_generationCorutine);
    }

    public void ChangeAlgorithm(Type newAlgorithm)
    {
        if(newAlgorithm.BaseType == typeof(GenerationAlgorithm))
        {
            if (TryGetComponent(out _algorithm))
            {
                Destroy(_algorithm);
            }

            _pool.Clear();
            _algorithm = (GenerationAlgorithm)gameObject.AddComponent(newAlgorithm);
            _pool.CreateObjects(_algorithm.GetPrefabsList());
        }
    }

    public void Spawn(Vector2 position)
    {
        _pool.Pop().transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    public void SpawnWaveEnder()
    {
        _waveEnder.transform.position = transform.position;
        _waveEnder.gameObject.SetActive(true);
    }

    public void StopGenerate()
    {
        StopCoroutine(_generationCorutine);
        _generationCorutine = null;
        _algorithm.StopGenerate();
    }

    public void GetBorders(out Vector2 minBorders, out Vector2 maxBorders)
    {
        PointerBorders pointerBorders = FindObjectOfType<PointerBorders>();

        if (pointerBorders != null)
        {
            pointerBorders.GetBorders(out minBorders, out maxBorders);
        }
        else
        {
            throw new UnityException("Add PointerBorders on scene!");
        }
    }

    private void Awake()
    {
        _pool = GetComponent<Pool>();
        _waveEnder.EndWaveEvent.AddListener(EndWaveEvent.Invoke);
    }

    private IEnumerator GeneratingWaveCorutine(float duration)
    {
        _algorithm.StartGenerate();
        yield return new WaitForSeconds(duration);
        _algorithm.StopGenerate();
    }
}