﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAlgorithm : GenerationAlgorithm
{
    [SerializeField] private RandomAlgParameters _parameters;
    private string _parametersPath = "RandomParameters";

    private new void Awake()
    {
        base.Awake();
        _parameters = Resources.Load(_parametersPath) as RandomAlgParameters;
        _prefabs = _parameters.Prefabs;
    }

    private void OnDestroy()
    {
        if(_parameters != null)
            Resources.UnloadAsset(_parameters);
    }

    protected override IEnumerator GenerationCorutine()
    {
        while (true)
        {
            Vector2 position =
                new Vector2(Random.Range(_minBorders.x, _maxBorders.x), Random.Range(_minBorders.y, _maxBorders.y));
            _generator.Spawn(position);
            yield return new WaitForSeconds(Random.Range(_parameters.DelayRange.x, _parameters.DelayRange.y));
        }
    }
}