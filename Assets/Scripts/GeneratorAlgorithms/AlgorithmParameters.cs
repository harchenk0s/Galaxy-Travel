using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlgorithmParameters : ScriptableObject
{
    [SerializeField] protected List<GameObject> Prefabs = new List<GameObject>();

    public List<GameObject> PrefabsList => Prefabs;
}
