using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooller : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        Garbage poolObject = null;

        if(collision.gameObject.TryGetComponent<Garbage>(out poolObject))
        {
            poolObject.ResetValues();
            _pool.Push(collision.gameObject);
        }
    }
}
