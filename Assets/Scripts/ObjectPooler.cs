using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private Pool _garbagePool;
    [SerializeField] private Pool _gatePool;

    private void Start()
    {
        if (_garbagePool == null || _gatePool == null)
            throw new UnityException("Pools not found");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Garbage>(out _))
        {
            _garbagePool.Push(other.gameObject);
        }
        else if(other.gameObject.TryGetComponent<SpaceGate>(out _))
        {
            _gatePool.Push(other.gameObject);
        }
    }
}
