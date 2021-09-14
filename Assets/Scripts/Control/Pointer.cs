using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour
{
    [SerializeField] private GameObject _objectOfControl = null;

    private IEnumerator _returnToObject = null;
    private PointerBorders _pointerBorders = null;
    private Vector2 _minBorders;
    private Vector2 _maxBorders;

    public void StartMove()
    {
        if (_returnToObject != null)
        {
            StopCoroutine(_returnToObject);
            _returnToObject = null;
            transform.position = _objectOfControl.transform.position;
        }
    }

    public void Move(Vector2 delta)
    {
        float deltaSumX = transform.position.x + delta.x;
        float deltaSumY = transform.position.y + delta.y;

        float resultX = Mathf.Clamp(deltaSumX, _minBorders.x, _maxBorders.x);
        float resultY = Mathf.Clamp(deltaSumY, _minBorders.y, _maxBorders.y);

        transform.position = new Vector3(resultX, resultY, 0);
    }

    public void EndMove()
    {
        _returnToObject = ReturnToObjectCourutine();
        StartCoroutine(_returnToObject);
    }

    public void SetObjectOfControl(GameObject objectOfControl)
    {
        _objectOfControl = objectOfControl;
    }

    private void Awake()
    {
        _pointerBorders = FindObjectOfType<PointerBorders>();
    }

    private void Start()
    {
        if(_pointerBorders == null)
        {
            throw new UnityException("Add PointerBorders on scene");
        }
        else
        {
            _pointerBorders.GetBorders(out _minBorders, out _maxBorders);
        }
    }

    private IEnumerator ReturnToObjectCourutine()
    {
        while (transform.position != _objectOfControl.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _objectOfControl.transform.position, 0.5f);
            yield return null;
        }
        _returnToObject = null;
    }
}