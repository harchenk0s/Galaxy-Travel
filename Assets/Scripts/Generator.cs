using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Pool))]
public class Generator : MonoBehaviour
{
    [SerializeField] private GenerationAlgorithm _algorithm;

    private Pool _pool;
    private IEnumerator _generationCorutine;

    public UnityEvent EndWaveEvent;

    public void GenerateWave(float duration)
    {
        if (_generationCorutine != null)
        {
            StopCoroutine(_generationCorutine);
            _algorithm.StopGenerate();
        }

        _generationCorutine = GeneratingWaveCorutine(duration);
        StartCoroutine(_generationCorutine);
    }

    public void ChangeAlgorithm(Type newAlgorithm)
    {
        if(newAlgorithm.BaseType == typeof(GenerationAlgorithm))
        {
            if (_generationCorutine != null)
            {
                StopCoroutine(_generationCorutine);
                _algorithm.StopGenerate();
            }

            if(TryGetComponent<GenerationAlgorithm>(out _algorithm))
            {
                Destroy(_algorithm);
            }
            
            _algorithm = (GenerationAlgorithm)gameObject.AddComponent(newAlgorithm);
            _pool.Clear();
            _pool.CreateObjects(_algorithm.GetPrefabsList());
        }
    }

    public void Spawn(Vector2 position)
    {
        _pool.Pop().transform.position = new Vector3(position.x, position.y, transform.position.z);
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
    }

    private void Start()
    {
        _algorithm = GetComponent<GenerationAlgorithm>();
        if (_algorithm == null)
        {
            throw new UnityException("Add GenerationAlgorithm");
        }
        else
        {
            _pool.CreateObjects(_algorithm.GetPrefabsList());
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
