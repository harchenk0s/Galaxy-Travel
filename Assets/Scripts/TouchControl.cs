using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Pointer pointer;
    public GameObject ship;
    public float Sentivity = 0.05f;
    Transform pointerTransform;

    private void Start()
    {
        pointerTransform = pointer.transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        pointer.X += eventData.delta.x * Sentivity;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        pointerTransform.position = ship.transform.position;
    }

    void Move(float delta)
    {

    }
}
