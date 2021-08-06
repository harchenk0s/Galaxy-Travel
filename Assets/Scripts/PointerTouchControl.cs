using UnityEngine;
using UnityEngine.EventSystems;

public class PointerTouchControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Range(0.01f,0.1f)]
    [SerializeField] private float _sentivity = 0.05f;
    [SerializeField] private GameObject _objectOfControl = null;

    private Pointer _pointer = null;
    private Transform pointerTransform;

    private void Start()
    {
        _pointer = FindObjectOfType<Pointer>();

        if (_pointer == null)
        {
            this.enabled = false;
            throw new UnityException("Pointer not found");
        }

        pointerTransform = _pointer.transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _pointer.Move(eventData.delta.x * _sentivity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _pointer.Move(eventData.delta.x * _sentivity);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        pointerTransform.position = _objectOfControl.transform.position;
    }
}