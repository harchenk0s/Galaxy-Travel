using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SyncShipSpeed : MonoBehaviour
{
    private float _speed = 100f;
    private Ship _ship;
    private Rigidbody _rigidbody;

    public void ChangeShip(GameObject ship)
    {
        _ship = ship.GetComponent<Ship>();
        _ship.ChangedSpeed.AddListener(OnChangeSpeed);
    }

    private void Awake()
    {
        LevelBuilder levelBuilder = FindObjectOfType<LevelBuilder>();
        levelBuilder.ShipChanged.AddListener(ChangeShip);
        _rigidbody = GetComponent<Rigidbody>();
        _ship = FindObjectOfType<Ship>();
        _speed = _ship.CurrentSpeed;
        _ship.ChangedSpeed.AddListener(OnChangeSpeed);
    }

    private void OnEnable()
    {
        _rigidbody.velocity = -Vector3.forward * _speed;
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnChangeSpeed(float speed)
    {
        _speed = speed;
        _rigidbody.velocity = -Vector3.forward * _speed;
    }

    private void OnDestroy()
    {
        _ship.ChangedSpeed.RemoveListener(OnChangeSpeed);
    }
}
