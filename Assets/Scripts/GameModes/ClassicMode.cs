using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMode : GameMode
{
    [SerializeField] private int _waveCount = 3;
    [SerializeField] private float _durationWave = 10f;

    private void Awake()
    {
        for (int i = 0; i < _waveCount; i++)
        {
            Waves.Add(new Wave(typeof(GridRandomAlg), Strings.AlgorithmsParameters.GridRandomAlg.GridRandomAlgDefault, _durationWave));
        }
    }
}
