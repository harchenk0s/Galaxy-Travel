using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    //TODO: DELETE THIS CLASS
    [SerializeField] Generator generator;
    WaveGenerator waveGen;
    Ship ship;
    RandomAlgorithm ra;
    public GameObject panel;

    private void Start()
    {
        waveGen = FindObjectOfType<WaveGenerator>();
    }

    public void Starttt()
    {
        waveGen.StartGenerate();
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
