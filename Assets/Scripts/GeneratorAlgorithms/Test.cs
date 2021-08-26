using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    GarbageGenerator generator;
    Ship ship;
    RandomAlgorithm ra;
    public GameObject panel;

    private void Start()
    {
        generator = FindObjectOfType<GarbageGenerator>();
    }

    public void Starttt()
    {
        generator.GenerateWave(30);
        panel.SetActive(false);
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
