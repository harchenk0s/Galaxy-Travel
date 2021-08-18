using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GizmoBorderDrawer : MonoBehaviour
{
    public bool DrawGizmos = true;
    public GameObject Point1, Point2;
    public int VerticalCount = 3;
    public int HorisontalCount = 3;

    public Color PointsColor = Color.red;
    public Color LinesColor = Color.green;
    
    public float[] HOSpoints;
    public float[] VERpoints;

    private Vector2 _point1, _point2;
    private float _height;
    private float _width;

    public void Refresh()
    {
        _point1 = Point1.transform.position;
        _point2 = Point2.transform.position;
        HOSpoints = new float[HorisontalCount];
        VERpoints = new float[VerticalCount];
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void Start()
    {
        Refresh();
    }

    private void DrawCross(Vector3 point)
    {
        Gizmos.color = PointsColor;
        Gizmos.DrawLine(point, point + new Vector3(1, 0, 0));
        Gizmos.DrawLine(point, point + new Vector3(-1, 0, 0));
        Gizmos.DrawLine(point, point + new Vector3(0, 1, 0));
        Gizmos.DrawLine(point, point + new Vector3(0, -1, 0));
    }

    private void DrawLines(Vector3 point1, Vector3 point2)
    {
        Gizmos.color = LinesColor;
        Gizmos.DrawLine(point1,
            new Vector3(point2.x, point1.y, 0));
        Gizmos.DrawLine(point1,
            new Vector3(point1.x, point2.y, 0));
    }

    private void DrawSpawnPoints(Vector3 point1, Vector3 point2)
    {
        Gizmos.color = PointsColor;
        _height = Mathf.Abs(point1.y - point2.y);
        _width = Mathf.Abs(point1.x - point2.x);

        HOSpoints[0] = Mathf.Min(point1.x, point2.x) + _width / (HOSpoints.Length * 2);
        VERpoints[0] = Mathf.Min(point1.y, point2.y) + _height / (VERpoints.Length * 2);

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

    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            DrawCross(_point1);
            DrawCross(_point2);
            DrawLines(_point1, _point2);
            DrawLines(_point2, _point1);
            DrawSpawnPoints(_point1, _point2);
        }
    }
}
