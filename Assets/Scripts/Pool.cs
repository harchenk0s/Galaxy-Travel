using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _objectsParent;
    [SerializeField] private List<GameObject> _prefabs = new List<GameObject>();

    private List<GameObject> _pool = new List<GameObject>();

    public void Push(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
        _pool.Add(poolObject);
    }

    public GameObject Pop()
    {
        if(_pool.Count >= 1)
        {
            GameObject poolObject = _pool[Random.Range(0, _pool.Count - 1)];
            _pool.Remove(poolObject);
            poolObject.SetActive(true);
            return poolObject;
        }
        else
        {
            CreateObject();
            return Pop();
        }
    }

    private void Awake()
    {
        if(_objectsParent == null)
        {
            _objectsParent = gameObject;
        }

        foreach (GameObject item in _prefabs)
        {
            CreateObject(item);
            CreateObject(item);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Pop();
        }
    }

    private GameObject CreateObject()
    {
        int rand = Random.Range(0, _prefabs.Count);
        GameObject poolObject = Instantiate(_prefabs[rand], _objectsParent.transform);
        _pool.Add(poolObject);
        return poolObject;
    }

    private GameObject CreateObject(GameObject prefab)
    {
        GameObject poolObject = Instantiate(prefab, _objectsParent.transform);
        poolObject.SetActive(false);
        _pool.Add(poolObject);
        return poolObject;
    }

    private void Start()
    {
        
    }
}
