using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Pool))]
public class GarbageGenerator : MonoBehaviour
{
    public GenerationAlgorithm _algorithm;
    private Pool _pool;
    private Vector2 _minBorders = Vector2.zero;
    private Vector2 _maxBorders = Vector2.zero;
    private IEnumerator _generationCorutine;

    public UnityEvent EndWaveEvent;

    public void GenerateWave(float duration)
    {
        _generationCorutine = GeneratingWaveCorutine(duration);
        StartCoroutine(_generationCorutine);
    }

    public void ChangeAlgorithm(GenerationAlgorithm newAlgorithm)
    {
        if(_generationCorutine != null)
        {
            StopCoroutine(_generationCorutine);
            _algorithm.StopGenerate();
        }

        Destroy(_algorithm);
        _algorithm = (GenerationAlgorithm)gameObject.AddComponent(newAlgorithm.GetType());
        _pool.Clear();
        _pool.CreateObjects(_algorithm.GetPrefabsList());
    }

    public void Spawn(Vector2 position)
    {
        _pool.Pop().transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    public void GetBorders(out Vector2 minBorders, out Vector2 maxBorders)
    {
        minBorders = _minBorders;
        maxBorders = _maxBorders;
    }

    private void Awake()
    {
        _pool = GetComponent<Pool>();
        _algorithm = GetComponent<GenerationAlgorithm>();

        if(_algorithm == null)
        {
            throw new UnityException("Add GenerationAlgorithm");
        }
        else
        {
            _pool.CreateObjects(_algorithm.GetPrefabsList());
        }
    }

    private void Start()
    {
        PointerBorders pointerBorders = FindObjectOfType<PointerBorders>();

        if (pointerBorders != null)
        {
            _minBorders = pointerBorders.MinBorders;
            _maxBorders = pointerBorders.MaxBorders;
        }
        else
        {
            throw new UnityException("Add PointerBorders on scene!");
        }
    }

    private IEnumerator GeneratingWaveCorutine(float duration)
    {
        _algorithm.StartGenerate();
        yield return new WaitForSeconds(duration);
        _algorithm.StopGenerate();
        EndWaveEvent.Invoke();
        _generationCorutine = null;
    }
}
