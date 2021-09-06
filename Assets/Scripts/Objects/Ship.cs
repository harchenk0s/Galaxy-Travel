using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

[Serializable]
public class FloatEvent : UnityEvent<float> { }
[Serializable]
public class IntEvent : UnityEvent<int> { }

[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour
{
    [SerializeField] private int _maxSpeed = 300;
    [SerializeField] private float _handling = 0.1f;
    [SerializeField] private int _maxArmor = 10;
    [SerializeField] [Range(0, 15)] private int _rotationSpeed = 5;

    private Pointer _pointer = null;
    private Transform _pointerTransform = null;
    private float _currentSpeed = 0;
    private int _currentArmor;
    private IEnumerator _changingSpeedCoroutine = null;

    public FloatEvent ChangeSpeedEvent;
    public IntEvent ChangeArmorEvent;
    public UnityEvent SpeedDownEvent;
    public UnityEvent SpeedUpEvent;
    public UnityEvent ShipHitEvent;
    public UnityEvent ShipDeadEvent;

    public float CurrentSpeed
    {
        get { return _currentSpeed; }

        private set
        {
            _currentSpeed = Mathf.Clamp(value, 0, _maxSpeed);
            ChangeSpeedEvent.Invoke(_currentSpeed);
        }
    }

    public int CurrentArmor
    {
        get { return _currentArmor; }

        private set
        {
            if(value >= 0)
            {
                _currentArmor = Mathf.Clamp(value, 0, _maxArmor);
                ChangeArmorEvent.Invoke(_currentArmor);
            }
            else
            {
                ShipDead();
            }
        }
    }

    public float CurrentPercentSpeed
    {
        get { return CurrentSpeed * 100 / _maxSpeed; }
    }

    public void Reset()
    {
        CurrentArmor = _maxArmor;
        ChangeSpeed(0);
    }

    public void StartMove()
    {
        ChangeSpeed(_maxSpeed / 2);
    }

    private void Awake()
    {
        _pointer = FindObjectOfType<Pointer>();
        CurrentArmor = _maxArmor;
        CurrentSpeed = 0;
    }

    private void Start()
    {
        if (_pointer == null)
        {
            enabled = false;
            throw new UnityException("Pointer not found");
        }
        else
        {
            _pointerTransform = _pointer.transform;
        }
    }

    private void LateUpdate()
    {
        MoveToPointer();
    }

    private void MoveToPointer()
    {
        transform.position = Vector3.Lerp(transform.position, _pointerTransform.position, _handling);
        transform.rotation = Quaternion.Euler((transform.position.y - _pointerTransform.position.y) * _rotationSpeed,
            0, (transform.position.x - _pointerTransform.position.x) * _rotationSpeed);
    }

    private void OnDestroy()
    {
        ShipDeadEvent.RemoveAllListeners();
        ShipHitEvent.RemoveAllListeners();
        ChangeSpeedEvent.RemoveAllListeners();
        ChangeArmorEvent.RemoveAllListeners();
        SpeedUpEvent.RemoveAllListeners();
        SpeedDownEvent.RemoveAllListeners();
    }

    private void ShipHit(Garbage garbage)
    {
        if (CurrentArmor > 0)
        {
            ShipHitEvent.Invoke();
            garbage.Explosion();
        }

        CurrentArmor -= garbage.Strength;
    }

    private void ShipDead()
    {
        ChangeSpeed(0);
        ShipDeadEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        SpaceGate spaceGate;
        Garbage garbage;

        if(other.TryGetComponent(out garbage))
        {
            ShipHit(garbage);
        }
        else if (other.TryGetComponent(out spaceGate))
        {
            float targetSpeed = _maxSpeed * spaceGate.BoostPercent / 100;
            ChangeSpeed(targetSpeed);
        }
    }

    private void ChangeSpeed(float targetSpeed)
    {
        if(_changingSpeedCoroutine != null)
        {
            StopCoroutine(_changingSpeedCoroutine);
        }

        if (targetSpeed > CurrentSpeed)
            SpeedUpEvent.Invoke();
        else
            SpeedDownEvent.Invoke();

        _changingSpeedCoroutine = ChangingSpeedCouroutine(targetSpeed);
        StartCoroutine(_changingSpeedCoroutine);
    }

    private IEnumerator ChangingSpeedCouroutine(float targetSpeed)
    {
        while(CurrentSpeed != targetSpeed)
        {
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, targetSpeed, 2);
            yield return null;
        }
    }
}