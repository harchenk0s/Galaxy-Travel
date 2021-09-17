using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour
{
    [SerializeField] protected List<Wave> Waves = new List<Wave>();
    [SerializeField] protected bool AddGates = true;
    [SerializeField] protected int StartSpeed;

    public bool AddGatesProperty => AddGates;
    public int StartSpeedProperty => StartSpeed;

    public List<Wave> GetWaves()
    {
        return new List<Wave>(Waves);
    }
}
