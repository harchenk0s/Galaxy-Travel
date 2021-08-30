using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] protected List<Wave> _waves = new List<Wave>();
    [SerializeField] protected bool _addGates = true;

    public bool AddGates => _addGates;

    public List<Wave> GetWaves()
    {
        return new List<Wave>(_waves);
    }
}
