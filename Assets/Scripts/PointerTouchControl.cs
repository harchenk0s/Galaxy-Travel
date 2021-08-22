using UnityEngine;
using UnityEngine.EventSystems;

public class PointerTouchControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] [Range(0.01f, 0.1f)] private float _sentivity = 0.05f;

    private Pointer _pointer = null;

    private void Awake()
    {
        _pointer = FindObjectOfType<Pointer>();
    }

    private void Start()
    {
        if (_pointer == null)
        {
            enabled = false;
            throw new UnityException("Add Pointer on scene");
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