using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalMode : GameMode
{
    private void Awake()
    {
        _waves.Add(new Wave(typeof(GridRandomAlg), "OnlyAsteroids", 5000f));
    }
}
