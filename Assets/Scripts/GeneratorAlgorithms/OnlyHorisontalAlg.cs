using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyHorisontalAlg : GenerationAlgorithm
{
    private float _height;

    public void ChangeHeight(float value)
    {
        _height = Mathf.Clamp(value, _minBorders.y, _maxBorders.y);
    }

    protected override IEnumerator GenerationCorutine()
    {
        _height = _minBorders.y;
        while (true)
        {
            Vector2 position =
                new Vector2(Random.Range(_minBorders.x, _maxBorders.x), Random.Range(_minBorders.y, _maxBorders.y));
            _generator.Spawn(position);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
        }
    }
}
