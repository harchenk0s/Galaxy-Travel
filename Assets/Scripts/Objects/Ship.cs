using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Ship : MonoBehaviour
{
    [SerializeField] private int _maxSpeed = 300;
    [SerializeField] private float _handling = 0.1f;
    [SerializeField] private int _maxArmor = 10;
    [SerializeField] [Range(0, 15)] private int _rotationSpeed = 5;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _mesh;

    private float _startSpeed = 100;
    private float _currentSpeed = 0;
    private int _currentArmor;
    private Pointer _pointer = null;
    private Transform _pointerTransform = null;
    private IEnumerator _changingSpeedCoroutine = null;
    private Collider _collider;

    public FloatEvent ChangedSpeed;
    public IntEvent ChangedArmor;
    public UnityEvent SpeedDowned;
    public UnityEvent SpeedUped;
    public UnityEvent ShipHited;
    public UnityEvent ShipDied;

    public float StartSpeed
    {
        get { return _startSpeed; }

        set
        {
            _startSpeed = Mathf.Clamp(value, 150, _maxSpeed);
        }
    }

    public float CurrentSpeed
    {
        get { return _currentSpeed; }

        private set
        {
            _currentSpeed = Mathf.Clamp(value, 0, _maxSpeed);
            ChangedSpeed.Invoke(_currentSpeed);
        }
    }

    public int MaxArmor
    {
        get { return _maxArmor; }
    }

    public int CurrentArmor
    {
        get { return _currentArmor; }

        private set
        {
            if(value >= 0)
            {
                _currentArmor = Mathf.Clamp(value, 0, _maxArmor);
                ChangedArmor.Invoke(_currentArmor);
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
        _pointerTransform.position = Vector3.zero;
        _collider.enabled = true;
        _mesh.SetActive(true);
        CurrentArmor = _maxArmor;
        ChangeSpeed(0);
    }

    public void StartMove()
    {
        CurrentSpeed = StartSpeed;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
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
        ShipDied.RemoveAllListeners();
        ShipHited.RemoveAllListeners();
        ChangedSpeed.RemoveAllListeners();
        ChangedArmor.RemoveAllListeners();
        SpeedUped.RemoveAllListeners();
        SpeedDowned.RemoveAllListeners();
    }

    private void ShipHit(Garbage garbage)
    {
        if (CurrentArmor > 0)
        {
            ShipHited.Invoke();
            garbage.Explosion();
        }

        CurrentArmor -= garbage.Strength;
    }

    private void ShipDead()
    {
        _collider.enabled = false;
        _mesh.SetActive(false);
        _explosion.Play();
        ChangeSpeed(0);
        ShipDied.Invoke();
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
            SpeedUped.Invoke();
        else
            SpeedDowned.Invoke();

        _changingSpeedCoroutine = ChangingSpeedCouroutine(targetSpeed);
        StartCoroutine(_changingSpeedCoroutine);
    }

    private IEnumerator ChangingSpeedCouroutine(float targetSpeed)
    {
        while(CurrentSpeed != targetSpeed)
        {
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, targetSpeed, 4);
            yield return null;
        }
    }
}