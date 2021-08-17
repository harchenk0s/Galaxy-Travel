using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Garbage : MonoBehaviour
{
    [SerializeField] private float _minRotationSpeed = -200f;
    [SerializeField] private float _maxRotationSpeed = 200;
    [SerializeField] private float _speed = 0.01f;

    private float _rotationSpeed;
    private Vector3 _rotation;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ResetValues();
    }

    private void OnEnable()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        transform.position = transform.parent.position;
        _rigidbody.velocity = new Vector3(0, 0, -1) * _speed;
        _rigidbody.AddTorque(_rotation * _rotationSpeed);
        _rotation = new Vector3(Random.value, Random.value, Random.value);
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
    }

}
