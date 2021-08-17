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
        {
            throw new UnityException("Pool not found");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Garbage poolObject = null;

        if (other.gameObject.TryGetComponent<Garbage>(out poolObject))
        {
            _pool.Push(other.gameObject);
        }
    }
}
