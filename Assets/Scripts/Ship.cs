using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private int _maxSpeed;
    [SerializeField] private float _handling = 10f;
    [SerializeField] private int _armor;
    [Range(1,15)]
    [SerializeField] private int _rotationSpeed = 5;

    private Pointer _pointer = null;
    private Transform _pointerTransform = null;

    private void Awake()
    {
        _pointer = FindObjectOfType<Pointer>();
        _handling /= 100;
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
        transform.position = Vector3.Lerp(transform.position, _pointerTransform.position, _handling);
        transform.rotation = Quaternion.Euler((transform.position.y - _pointerTransform.position.y) * _rotationSpeed,
            0, (transform.position.x - _pointerTransform.position.x) * _rotationSpeed);
    }
}