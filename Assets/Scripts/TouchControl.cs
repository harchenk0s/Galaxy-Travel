using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject pointer;
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
        pointerTransform.position += new Vector3(eventData.delta.x, 0, 0) * Sentivity;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        pointerTransform.position = ship.transform.position;
    }

    void Move(float delta)
    {

    }
}
