using UnityEngine;
using UnityEngine.EventSystems;

public class PointerTouchControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Range(0.01f,0.1f)]
    [SerializeField] private float _sentivity = 0.05f;

    private Pointer _pointer = null;

    private void Start()
    {
        _pointer = FindObjectOfType<Pointer>();

        if (_pointer == null)
        {
            enabled = false;
            throw new UnityException("Pointer not found");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _pointer.StartMove();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _pointer.Move(eventData.delta * _sentivity);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _pointer.EndMove();
    }
}