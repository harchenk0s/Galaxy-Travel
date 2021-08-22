using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBorders : MonoBehaviour
{
    [SerializeField] private List<GameObject> _borderPoints  = new List<GameObject>(2);
    private Vector2 _minBorders = new Vector2();
    private Vector2 _maxBorders = new Vector2();
    private bool _isBorderInitialize = false;

    public void GetBorders(out Vector2 minBorders, out Vector2 maxBorders)
    {
        if(!_isBorderInitialize)
            InitializeBorders();

        minBorders = _minBorders;
        maxBorders = _maxBorders;
    }

    private void InitializeBorders()
    {
        if (_borderPoints.Count > 1)
        {
            float leftBorder = float.MaxValue;
            float rightBorder = float.MinValue;
            float downBorder = float.MaxValue;
            float upBorder = float.MinValue;

            foreach (GameObject border in _borderPoints)
            {
                float borderX = border.transform.position.x;
                float borderY = border.transform.position.y;

                if (borderX < leftBorder)
                    leftBorder = borderX;

                if (borderX > rightBorder)
                    rightBorder = borderX;

                if (borderY < downBorder)
                    downBorder = borderY;

                if (borderY > upBorder)
                    upBorder = borderY;
            }

            _minBorders = new Vector2(leftBorder, downBorder);
            _maxBorders = new Vector2(rightBorder, upBorder);
            _isBorderInitialize = true;
        }
        else
        {
            throw new UnityException("Add 2 GameObject in BorderPoints List");
        }
    }

#if UNITY_EDITOR

    public bool DrawGizmos = false;
    public Color LinesColor = Color.green;
    public Color PointsColor = Color.red;

    public bool DrawGrid = false;

    [Min(1)] public int Columns = 3;
    [Min(1)] public int Rows = 3;

    private float _height;
    private float _width;
    private float[] _columns;
    private float[] _rows;

    private void DrawLines(Vector3 point1, Vector3 point2)
    {
        Gizmos.color = LinesColor;
        Gizmos.DrawLine(point1,
            new Vector3(point2.x, point1.y, point2.z));
        Gizmos.DrawLine(point1,
            new Vector3(point1.x, point2.y, point2.z));
    }

    private void DrawGridPoints(Vector3 point1, Vector3 point2)
    {
        Gizmos.color = PointsColor;
        _columns = new float[Columns];
        _rows = new float[Rows];

        _height = Mathf.Abs(point1.y - point2.y);
        _width = Mathf.Abs(point1.x - point2.x);

        _columns[0] = Mathf.Min(point1.x, point2.x) + _width / (_columns.Length * 2);
        _rows[0] = Mathf.Min(point1.y, point2.y) + _height / (_rows.Length * 2);

        for (int i = 1; i < _columns.Length; i++)
        {
            _columns[i] = _columns[0] + _width / _columns.Length * i;
        }

        for (int i = 1; i < _rows.Length; i++)
        {
            _rows[i] = _rows[0] + _height / _rows.Length * i;
        }

        for (int i = 0; i < _columns.Length; i++)
        {
            for (int j = 0; j < _rows.Length; j++)
            {
                Gizmos.DrawSphere(new Vector3(_columns[i], _rows[j]), 1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            try
            {
                DrawLines(_borderPoints[0].transform.position, _borderPoints[1].transform.position);
                DrawLines(_borderPoints[1].transform.position, _borderPoints[0].transform.position);

                if (DrawGrid)
                    DrawGridPoints(_borderPoints[0].transform.position, _borderPoints[1].transform.position);
            }
            catch
            {
                DrawGizmos = false;
                throw new UnityException("Add 2 GameObject in BorderPoints List");
            }
        }
    }
#endif
}
