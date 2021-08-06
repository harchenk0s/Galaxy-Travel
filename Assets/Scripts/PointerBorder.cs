using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBorder : MonoBehaviour
{
    public Color GizmoColor = Color.red;
    public Vector3 GizmoSize = new Vector3(0.01f, 1, 1);
    public Pointer pointer;

    private void Awake()
    {
        pointer = FindObjectOfType<Pointer>();
    }

    private void Start()
    {
        if(pointer != null)
        {
            Vector3 pointerPos = pointer.transform.position;
            transform.position = new Vector3(transform.position.x, pointerPos.y, pointerPos.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawCube(transform.position, GizmoSize);
    }
}
