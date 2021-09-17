using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Generator))]
public abstract class GenerationAlgorithm : MonoBehaviour
{
    protected List<GameObject> Prefabs = new List<GameObject>();
    protected Generator Generator;
    protected Vector2 MinBorders = Vector2.zero;
    protected Vector2 MaxBorders = Vector2.zero;
    protected bool IsBusy = false;

    private IEnumerator _generationCorutine;

    public void StartGenerate()
    {
        _generationCorutine = GenerationCorutine();
        IsBusy = true;
        StartCoroutine(_generationCorutine);
    }

    public void StopGenerate()
    {
        IsBusy = false;
        StopCoroutine(_generationCorutine);
        Generator.SpawnWaveEnder();
    }

    public List<GameObject> GetPrefabsList()
    {
        return Prefabs;
    }

    protected void Awake()
    {
        Generator = GetComponent<Generator>();
        Generator.GetBorders(out MinBorders, out MaxBorders);
        _generationCorutine = GenerationCorutine();
    }

    protected abstract IEnumerator GenerationCorutine();
}