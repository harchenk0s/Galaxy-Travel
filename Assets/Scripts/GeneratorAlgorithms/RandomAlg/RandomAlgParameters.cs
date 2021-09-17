using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRandomParam", menuName = "AlgorithmParameters/Random", order = 51)]
public class RandomAlgParameters : AlgorithmParameters
{
    [SerializeField] private Vector2 _delayRange = new Vector2();

    public Vector2 DelayRange => _delayRange;
}

