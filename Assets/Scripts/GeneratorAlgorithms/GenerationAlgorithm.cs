using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenerationAlgorithm : MonoBehaviour
{
    [SerializeField] protected List<GameObject> _prefabs = new List<GameObject>();

    protected GarbageGenerator _generator;
    protected Vector2 _minBorders = Vector2.zero;
    protected Vector2 _maxBorders = Vector2.zero;
    private IEnumerator _generationCorutine;

    public void StartGenerate()
    {
        _generator.GetBorders(out _minBorders, out _maxBorders);
        _generationCorutine = GenerationCorutine();
        StartCoroutine(_generationCorutine);
    }

    public void StopGenerate()
    {
        if(_generationCorutine != null)
            StopCoroutine(_generationCorutine);
    }

    public List<GameObject> GetPrefabsList()
    {
        return _prefabs;
    }

    protected void Awake()
    {
        _generator = GetComponent<GarbageGenerator>();
        
    }

    protected abstract IEnumerator GenerationCorutine();
}
