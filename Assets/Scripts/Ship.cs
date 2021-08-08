using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private int _maxSpeed;
    [SerializeField] private float _handling;
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
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _pointerTransform.position.x, _handling),0,0);
    }
}