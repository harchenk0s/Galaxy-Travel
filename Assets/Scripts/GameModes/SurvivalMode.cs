using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalMode : GameMode
{
    private void Awake()
    {
        Waves.Add(new Wave(typeof(GridRandomAlg),Strings.AlgorithmsParameters.GridRandomAlg.GridRandomOnlyAsteroids, 5000f));
    }
}
