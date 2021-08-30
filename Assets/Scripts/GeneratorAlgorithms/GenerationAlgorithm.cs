using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Generator))]
public abstract class GenerationAlgorithm : MonoBehaviour
{
    protected List<GameObject> _prefabs = new List<GameObject>();
    protected Generator _generator;
    protected Vector2 _minBorders = Vector2.zero;
    protected Vector2 _maxBorders = Vector2.zero;
    protected bool isBusy = false;

    private IEnumerator _generationCorutine;

    public void StartGenerate()
    {
        isBusy = true;
        StartCoroutine(_generationCorutine);
    }

    public void StopGenerate()
    {
        isBusy = false;
        StopCoroutine(_generationCorutine);
        _generationCorutine = null;
        _generator.SpawnWaveEnder();
    }

    public List<GameObject> GetPrefabsList()
    {
        return _prefabs;
    }

    protected void Awake()
    {
        _generator = GetComponent<Generator>();
        _generator.GetBorders(out _minBorders, out _maxBorders);
        _generationCorutine = GenerationCorutine();
    }

    protected abstract IEnumerator GenerationCorutine();
}
