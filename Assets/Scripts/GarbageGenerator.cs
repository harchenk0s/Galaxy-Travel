using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageGenerator : MonoBehaviour
{
    [SerializeField] private float _waveIntervalSec = 5f;

    private Pool _pool;
    private Vector2 _minBorders;
    private Vector2 _maxBorders;
    private bool _generating = false;

    public void GenerateWave(float seconds, float timeInterval)
    {

    }

    private IEnumerator GeneratingCycle(float timeInterval)
    {
        while (_generating)
        {
            transform.position =
                new Vector3(Random.Range(_minBorders.x, _maxBorders.x), Random.Range(_minBorders.y, _maxBorders.y), 1000);
            _pool.Pop().transform.position = transform.position;
            yield return new WaitForSeconds(timeInterval);
        }
    }

    private void Awake()
    {
        _pool = FindObjectOfType<Pool>();
    }

    private void Start()
    {
        Pointer pointer = FindObjectOfType<Pointer>();

        if (pointer != null)
        {
            _minBorders = pointer._minBorders;
            _maxBorders = pointer._maxBorders;
        }
        _generating = true;

        StartCoroutine(GeneratingCycle(0.2f));
    }
}
