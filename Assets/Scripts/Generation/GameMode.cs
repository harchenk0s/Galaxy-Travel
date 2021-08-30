using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] protected List<Wave> _waves = new List<Wave>();

    public List<Wave> GetWaves()
    {
        return new List<Wave>(_waves);
    }
}
