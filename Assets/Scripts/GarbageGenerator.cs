using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 _rangeDelay = new Vector2(0.01f, 0.1f);
    [SerializeField] private List<GameObject> _garbagePrefabs = new List<GameObject>();
    private Pointer _pointer = null;
    private Ship _ship = null;
    private Vector2 _generatorConstraint;
    private Vector2 _minBorders;
    private Vector2 _maxBorders;
    private IEnumerator _generingCourutine;

    private void Awake()
    {
        _pointer = FindObjectOfType<Pointer>();
        _ship = FindObjectOfType<Ship>();

        if(_pointer == null || _ship == null)
        {
            enabled = false;
            throw new UnityException("Pointer or ship not found");
        }
        else
        {
            _minBorders = _pointer._minBorders;
            _maxBorders = _pointer._maxBorders;
        }

    }

    private void Start()
    {
        StartCoroutine("StartGenering");
    }

    private IEnumerator StartGenering()
    {
        while (true)
        {
            Vector3 randomVector = new Vector3(Random.Range(-13, 13), Random.Range(-5, 20), 0);
            Instantiate(_garbagePrefabs[Random.Range(0, _garbagePrefabs.Count)], transform.position + randomVector, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(_rangeDelay.x, _rangeDelay.y));
        }
    }


}
