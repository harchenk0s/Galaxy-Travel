using UnityEngine;
using System.Collections.Generic;

public class PointerBorder : MonoBehaviour
{
    public int VerticalCount = 3;
    public int HorisontalCount = 3;
    public PointerBorder _anotherBorder = null;
    public List<PointerBorder> _borders = new List<PointerBorder>();

    public Color PointsColor = Color.red;
    public Color LinesColor = Color.green;

    private float _height;
    private float _width;
    public float[] HOSpoints;
    public float[] VERpoints;

    public void Refresh()
    {
        _borders.AddRange(FindObjectsOfType<PointerBorder>());
        _borders.Remove(this);
        _anotherBorder = _borders[0];
        HOSpoints = new float[HorisontalCount];
        VERpoints = new float[VerticalCount];
        _anotherBorder.HorisontalCount = HorisontalCount;
        _anotherBorder.VerticalCount = VerticalCount;
        _anotherBorder.Refresh(false);
    }

    public void Refresh(bool f)
    {
        _borders.AddRange(FindObjectsOfType<PointerBorder>());
        _borders.Remove(this);
        _anotherBorder = _borders[0];
        HOSpoints = new float[HorisontalCount];
        VERpoints = new float[VerticalCount];
    }

    private void Start()
    {
        Refresh();
    }

    private void OnDrawGizmos()
    {
        if (_anotherBorder == null || _anotherBorder == this)
        {
            _borders.Clear();
            _borders.AddRange(FindObjectsOfType<PointerBorder>());

            if (_borders.Count >= 2)
            {
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

        Gizmos.color = PointsColor;
        _height = Mathf.Abs(transform.position.y - _anotherBorder.transform.position.y);
        _width = Mathf.Abs(transform.position.x - _anotherBorder.transform.position.x);

        HOSpoints[0] = Mathf.Min(transform.position.x, _anotherBorder.transform.position.x) + _width / (HOSpoints.Length * 2);
        VERpoints[0] = Mathf.Min(transform.position.y, _anotherBorder.transform.position.y) + _height / (VERpoints.Length * 2);

        for (int i = 1; i < HOSpoints.Length; i++)
        {
            HOSpoints[i] = HOSpoints[0] + _width / HOSpoints.Length * i;
        }

        for (int i = 1; i < VERpoints.Length; i++)
        {
            VERpoints[i] = VERpoints[0] + _height / VERpoints.Length * i;
        }

        for (int i = 0; i < HOSpoints.Length; i++)
        {
            for (int j = 0; j < VERpoints.Length; j++)
            {
                Gizmos.DrawSphere(new Vector3(HOSpoints[i], VERpoints[j]), 1);
            }
        }
    }
}