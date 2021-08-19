﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Garbage : MonoBehaviour
{
    [SerializeField] private float _minRotationSpeed = -200f;
    [SerializeField] private float _maxRotationSpeed = 200;
    [SerializeField] private float _speed = 100f;

    private float _rotationSpeed;
    private Vector3 _rotationVector;
    private Rigidbody _rigidbody;
    private Ship _ship;

    private void Awake()
    {
        _ship = FindObjectOfType<Ship>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.angularDrag = 0;
        _rigidbody.drag = 0;
    }

    private void Start()
    {
        OnEnable();
        _ship.ChangeSpeedEvent.AddListener(OnChangeSpeed);
        _speed = _ship.CurrentSpeed;
    }

    private void OnEnable()
    {
        _rigidbody.velocity = -Vector3.forward * _speed;
        _rotationVector = new Vector3(Random.value, Random.value, Random.value);
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        _rigidbody.AddTorque(_rotationVector * _rotationSpeed);
    }

    private void OnChangeSpeed(float speed)
    {
        _speed = speed;
        _rigidbody.velocity = -Vector3.forward * _speed;
    }

    private void OnDestroy()
    {
        _ship.ChangeSpeedEvent.RemoveListener(OnChangeSpeed);
    }
}
