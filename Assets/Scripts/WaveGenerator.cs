using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] private List<GenerationAlgorithm> _algorithms = new List<GenerationAlgorithm>();

    private GarbageGenerator _generator;

    private void Awake()
    {
        _generator = FindObjectOfType<GarbageGenerator>();
    }

    private void Start()
    {
        if(_generator == null)
        {
            throw new UnityException("No GarbageGenerator on scene");
        }
    }

    public void StartGenerate()
    {
        _generator.ChangeAlgorithm(typeof(GridRandomAlg));
        _generator.GenerateWave(60);
    }
}
