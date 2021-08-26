using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    private string _algorithm;
    private string _parameters;
    private float _duration;
    private float _secondsLeft;

    public Wave(string algorithm, string parameters, float duration)
    {
        _algorithm = algorithm;
        _parameters = parameters;
        _duration = duration;
    }
}
