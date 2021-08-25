using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] private List<GenerationAlgorithm> _algorithms = new List<GenerationAlgorithm>();

    public void StartGenerate()
    {

    }
}
