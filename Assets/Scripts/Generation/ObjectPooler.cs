using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private Pool _garbagePool;

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
    }
}
