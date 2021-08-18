using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private Pool _pool;

    private void Awake()
    {
        _pool = FindObjectOfType<Pool>();

        if (_pool == null)
            throw new UnityException("Pool not found");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Garbage>(out _))
            _pool.Push(other.gameObject);
    }
}
