using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private Pool _garbagePool;
    [SerializeField] private WaveEnder _waveEnder;

    private void Start()
    {
        if (_garbagePool == null)
            throw new UnityException("Pool not found");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Garbage>(out _))
        {
            _garbagePool.Push(other.gameObject);
        }
        else
        {
            if (other.TryGetComponent<WaveEnder>(out _))
            {
                _waveEnder.gameObject.SetActive(false);
                _waveEnder.ReturnToStart();
            }
        }
    }
}
