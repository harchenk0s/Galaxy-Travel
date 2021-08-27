using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRandomParam", menuName = "AlgorithmParameters/ExactPoints")]
public class ExactPointsParameters : AlgorithmParameters
{
    [SerializeField] private List<Vector2> _points = new List<Vector2>(4);
    [SerializeField] [Min(2)] private int _columns = 2;
    [SerializeField] [Min(2)] private int _rows = 2;

    public int Columns => _columns;
    public int Rows => _rows;
    public List<Vector2> Points => _points;
}
