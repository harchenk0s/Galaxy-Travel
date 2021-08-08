using UnityEngine;

public class PointerBorder : MonoBehaviour
{
    public Color GizmoColor = Color.red;
    private Pointer _pointer = null;
    private PointerBorder _anotherBorder;

    private void Awake()
    {
        _pointer = FindObjectOfType<Pointer>();
        _anotherBorder = FindObjectOfType<PointerBorder>();
    }

    private void OnDrawGizmos()
    {
        if (_anotherBorder == null || _pointer == null)
        {
            _anotherBorder = FindObjectOfType<PointerBorder>();
            _pointer = FindObjectOfType<Pointer>();
            return;
        }    

        Gizmos.color = GizmoColor;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(1, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-1, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 1, 0));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -1, 0));
        Gizmos.DrawLine(transform.position, _anotherBorder.transform.position);

    }
}