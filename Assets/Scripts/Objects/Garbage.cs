using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Garbage : MonoBehaviour
{
    [SerializeField] private float _minRotationSpeed = -200f;
    [SerializeField] private float _maxRotationSpeed = 200;
    [SerializeField] private ParticleSystem _explosionEffect = null;
    [SerializeField] private GameObject _garbageObject = null;

    private float _rotationSpeed;
    private Vector3 _rotationVector;
    private Rigidbody _rigidbody;

    public void Explosion()
    {
        _garbageObject.SetActive(false);
        _explosionEffect.Play();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.angularDrag = 0;
        _rigidbody.drag = 0;
    }

    private void NewParameters()
    {
        _garbageObject.SetActive(true);
        _rotationVector = new Vector3(Random.value, Random.value, Random.value);
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        _rigidbody.AddTorque(_rotationVector * _rotationSpeed);
    }

    private void OnEnable()
    {
        NewParameters();
    }
}
