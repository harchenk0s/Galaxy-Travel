using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject ship;

    private void Start()
    {
        Debug.LogError("START!");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        ship.transform.position += new Vector3(eventData.delta.x, 0, 0) * 0.05f;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
    }
}
