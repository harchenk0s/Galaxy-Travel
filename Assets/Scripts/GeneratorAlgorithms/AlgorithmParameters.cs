using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlgorithmParameters : ScriptableObject
{
    [SerializeField] protected List<GameObject> _prefabs = new List<GameObject>();

    public List<GameObject> Prefabs => _prefabs;
}
