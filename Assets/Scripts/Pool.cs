using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private int _maxPoolElements = 20;
    [SerializeField] private List<GameObject> _prefabs = new List<GameObject>();

    private Queue<GameObject> _pool = new Queue<GameObject>();

    public void Push(GameObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
        _pool.Enqueue(poolObject);
    }

    public GameObject Pop()
    {
        if(_pool.Count >= 1)
        {
            GameObject poolObject = _pool.Dequeue();
            poolObject.SetActive(true);
            return poolObject;
        }
        else
        {
            CreateObject();
            return _pool.Dequeue();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Pop();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }

    private void Start()
    {

    }

    private GameObject CreateObject()
    {
        GameObject poolObject = Instantiate(_prefabs[Random.Range(0, _prefabs.Count)], this.transform);
        _pool.Enqueue(poolObject);
        return poolObject;
    }
}
