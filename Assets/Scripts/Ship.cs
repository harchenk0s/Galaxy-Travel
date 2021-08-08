using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private int _maxSpeed;
    [SerializeField] private float _handling = 0.1f;
    [SerializeField] private int _armor;

    private Pointer _pointer = null;
    private Transform _pointerTransform = null;

    private void Awake()
    {
        _pointer = FindObjectOfType<Pointer>();
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
        transform.rotation = Quaternion.Euler((transform.position.y - _pointerTransform.position.y) * 5,
            0, (transform.position.x - _pointerTransform.position.x) * 5);
    }
}