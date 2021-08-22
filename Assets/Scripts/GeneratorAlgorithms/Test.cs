using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GarbageGenerator generator;
    Ship ship;
    RandomAlgorithm ra;

    private void Start()
    {
        generator = FindObjectOfType<GarbageGenerator>();
        ship = FindObjectOfType<Ship>();
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            generator.ChangeAlgorithm(typeof(RandomAlgorithm));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            generator.GenerateWave(20);
        }


    }
}
