using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    [SerializeField] private float _minRotationSpeed = -0.8f;
    [SerializeField] private float _maxRotationSpeed = 0.8f;
    private float _rotationSpeed;
    private Vector3 _rotation;

    private void Start()
    {
        _rotation = new Vector3(Random.value, Random.value, Random.value).normalized;
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
    }
    private void FixedUpdate()
    {
        transform.Rotate(_rotation * _rotationSpeed);
    }
}
