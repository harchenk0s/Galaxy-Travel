using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMode : GameMode
{
    private void Start()
    {
        LevelBuilder levelBuilder = FindObjectOfType<LevelBuilder>();
        levelBuilder.ChangeStartSpeed(200);
    }

    private void Awake()
    {
        _waves.Add(new Wave(typeof(RandomAlg), "OnlyCowsRandom", 10f));
        _waves.Add(new Wave(typeof(GridRandomAlg), "OnlyCowsGrid", 10f));
        _waves.Add(new Wave(typeof(GridRandomAlg), "OnlyCowsGrid", 15f));
    }
}
