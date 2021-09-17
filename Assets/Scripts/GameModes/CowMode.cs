using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMode : GameMode
{
    private void Awake()
    {
        Waves.Add(new Wave(typeof(RandomAlg), Strings.AlgorithmsParameters.RandomAlg.RandomAlgOnlyCows, 10f));
        Waves.Add(new Wave(typeof(GridRandomAlg), Strings.AlgorithmsParameters.GridRandomAlg.GridRandomOnlyCows, 10f));
        Waves.Add(new Wave(typeof(GridRandomAlg), Strings.AlgorithmsParameters.GridRandomAlg.GridRandomOnlyCows, 15f));
    }
}
