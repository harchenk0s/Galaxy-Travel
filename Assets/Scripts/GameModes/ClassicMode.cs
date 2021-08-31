using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMode : GameMode
{
    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            _waves.Add(new Wave(typeof(GridRandomAlg), "GridRandomAlgDefault", 10f));
        }
    }
}
