using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBorder : MonoBehaviour
{
    public Color GizmoColor = Color.red;
    public Vector3 GizmoSize = new Vector3(0.01f, 1, 1);
    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawCube(transform.position, GizmoSize);
    }
}
