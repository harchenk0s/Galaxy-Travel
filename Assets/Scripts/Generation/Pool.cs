using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private List<GameObject> _pool = new List<GameObject>();
    private List<GameObject> _allPoolObjects = new List<GameObject>();

    public void Push(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
        _pool.Add(poolObject);
    }

    public GameObject Pop()
    {
        GameObject poolObject;

        if(_pool.Count > 0)
        {
            poolObject = _pool[Random.Range(0, _pool.Count - 1)];
        }
        else
        {
            poolObject = CreateObject(_allPoolObjects[Random.Range(0, _allPoolObjects.Count - 1)]);
        }

        _pool.Remove(poolObject);
        poolObject.SetActive(true);
        return poolObject;
    }

    public void Clear()
    {
        _pool.Clear();
        foreach (GameObject poolObject in _allPoolObjects)
        {
            Destroy(poolObject);
        }
        _allPoolObjects.Clear();
    }

    public GameObject CreateObject(GameObject prefab)
    {
        GameObject poolObject = Instantiate(prefab, transform);
        poolObject.SetActive(false);
        _pool.Add(poolObject);
        _allPoolObjects.Add(poolObject);
        return poolObject;
    }

    public void CreateObjects(List<GameObject> prefabs)
    {
        foreach (GameObject prefab in prefabs)
        {
            GameObject poolObject = Instantiate(prefab, transform);
            poolObject.SetActive(false);
            _pool.Add(poolObject);
            _allPoolObjects.Add(poolObject);
        }
    }
}
