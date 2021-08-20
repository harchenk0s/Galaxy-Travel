﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAlgorithm : GenerationAlgorithm
{
    protected override IEnumerator GenerationCorutine()
    {
        while (true)
        {
            Vector2 position =
                new Vector2(Random.Range(_minBorders.x, _maxBorders.x), Random.Range(_minBorders.y, _maxBorders.y));
            _generator.Spawn(position);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
        }
    }
}
