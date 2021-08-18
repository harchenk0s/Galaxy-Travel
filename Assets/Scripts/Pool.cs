using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private List<GameObject> _pool = new List<GameObject>();

    public void Push(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
        _pool.Add(poolObject);
    }

    public GameObject Pop()
    {
        if(_pool.Count > 0)
        {
            GameObject poolObject = _pool[Random.Range(0, _pool.Count - 1)];
            _pool.Remove(poolObject);
            poolObject.SetActive(true);
            return poolObject;
        }
        else
        {
            throw new UnityException("Pool is empty!");
        }
    }

    public GameObject CreateObject(GameObject prefab)
    {
        GameObject poolObject = Instantiate(prefab, transform);
        poolObject.SetActive(false);
        _pool.Add(poolObject);
        return poolObject;
    }
}
