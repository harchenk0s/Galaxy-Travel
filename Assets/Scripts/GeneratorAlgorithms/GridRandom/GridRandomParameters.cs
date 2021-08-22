using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRandomParam", menuName = "AlgorithmParameters/GridRandom")]
public class GridRandomParameters : AlgorithmParameters
{
    [SerializeField] private int _Columns;
    [SerializeField] private int _Rows;
    [SerializeField] private Vector2 _delayRange = new Vector2();
    
    public int Columns => _Columns;
    public int Rows => _Rows;
    public Vector2 DelayRange => _delayRange;
}
