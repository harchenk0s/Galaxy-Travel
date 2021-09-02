using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRandomParam", menuName = "AlgorithmParameters/GridRandom", order = 51)]
public class GridRandomParameters : AlgorithmParameters
{
    [SerializeField] [Min(1)] private int _Columns = 1;
    [SerializeField] [Min(1)] private int _Rows = 1;
    [SerializeField] private Vector2 _delayRange = new Vector2();
    
    public int Columns => _Columns;
    public int Rows => _Rows;
    public Vector2 DelayRange => _delayRange;
}
