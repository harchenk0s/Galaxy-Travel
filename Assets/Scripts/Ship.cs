using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[Serializable]
public class ChangeSpeedEvent : UnityEvent<float> { }

public class Ship : MonoBehaviour
{
    [SerializeField] private int _maxSpeed = 300;
    [SerializeField] private float _handling = 10f;
    [SerializeField] private int _maxArmor = 10;
    [SerializeField] [Range(1, 15)] private int _rotationSpeed = 5;
    
    private Pointer _pointer = null;
    private Transform _pointerTransform = null;
    private float _currentSpeed = 0;
    private int _currentArmor;

    public ChangeSpeedEvent ChangeSpeedEvent;
    public UnityEvent ShipHitEvent;

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
            }
            else
            {
                ShipDead();
            }
        }
    }

    private void Awake()
    {
        _pointer = FindObjectOfType<Pointer>();
        _handling /= 100;
        CurrentSpeed = 300;
        _currentArmor = _maxArmor;
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
        ChangeSpeedEvent.RemoveAllListeners();
    }

    private void ShipHit()
    {
        CurrentArmor--;
        ShipHitEvent.Invoke();
    }

    private void ShipDead()
    {
        Debug.LogError("Ship dead!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Garbage>(out _))
        {
            ShipHit();
        }
    }
}