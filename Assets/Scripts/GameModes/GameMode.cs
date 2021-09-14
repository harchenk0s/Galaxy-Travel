using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour
{
    [SerializeField] protected List<Wave> _waves = new List<Wave>();
    [SerializeField] protected bool _addGates = true;
    [SerializeField] protected int _startSpeed;

    public bool AddGates => _addGates;
    public int StartSpeed => _startSpeed;

    public List<Wave> GetWaves()
    {
        return new List<Wave>(_waves);
    }
}
