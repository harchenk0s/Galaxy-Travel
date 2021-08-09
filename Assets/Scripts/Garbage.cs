using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    [SerializeField] private float _minRotationSpeed = -50.6f;
    [SerializeField] private float _maxRotationSpeed = 50.6f;
    [SerializeField] private float _delayBeforeDestroy = 20f;
    [SerializeField] private float _speed = 0.01f;
    private float _rotationSpeed;
    private Vector3 _rotation;

    private void Start()
    {
        _rotation = new Vector3(Random.value, Random.value, Random.value).normalized;
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        StartCoroutine("DestroyForTime");
    }
    private void FixedUpdate()
    {
        transform.Rotate(_rotation * _rotationSpeed);
        transform.position += new Vector3(0,0, -_speed);
    }

    private IEnumerator DestroyForTime()
    {
        yield return new WaitForSeconds(_delayBeforeDestroy);
        Destroy(this.gameObject);
    }
}
