using UnityEngine;
using System.Collections.Generic;

public class PointerBorder : MonoBehaviour
{
    public Color PointsColor = Color.red;
    public Color LinesColor = Color.green;
    public bool DrawGizmos = true;

    private PointerBorder _anotherBorder = null;
    private List<PointerBorder> _borders = new List<PointerBorder>();

    private void Start()
    {
        _borders.AddRange(FindObjectsOfType<PointerBorder>());
        _borders.Remove(this);
        _anotherBorder = _borders[0];
    }

    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            if (_anotherBorder == null || _anotherBorder == this)
            {
                _borders.Clear();
                _borders.AddRange(FindObjectsOfType<PointerBorder>());

                if (_borders.Count >= 2)
                {
                    _borders.Remove(this);
                    _anotherBorder = _borders[0];
                }
                return;
            }

            Gizmos.color = PointsColor;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(1, 0, 0));
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(-1, 0, 0));
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, 1, 0));
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -1, 0));

            Gizmos.color = LinesColor;
            Gizmos.DrawLine(transform.position,
                new Vector3(_anotherBorder.transform.position.x, transform.position.y, transform.position.z));
            Gizmos.DrawLine(transform.position,
                new Vector3(transform.position.x, _anotherBorder.transform.position.y, transform.position.z));

        }
    }
}