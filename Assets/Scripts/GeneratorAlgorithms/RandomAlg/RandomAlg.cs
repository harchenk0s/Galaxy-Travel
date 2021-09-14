using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAlg : GenerationAlgorithm
{
    [SerializeField] private RandomAlgParameters _parameters;
    private string _parametersPathDefault = "RandomAlgDefault";

    protected override IEnumerator GenerationCorutine()
    {
        while (true)
        {
            Vector2 position =
                new Vector2(Random.Range(_minBorders.x, _maxBorders.x), Random.Range(_minBorders.y, _maxBorders.y));
            _generator.Spawn(position);
            yield return new WaitForSecondsRealtime(Random.Range(_parameters.DelayRange.x, _parameters.DelayRange.y));
        }
    }

    private new void Awake()
    {
        base.Awake();
        string parametersPath = PlayerPrefs.GetString("RandomAlg");
        _parameters = Resources.Load<RandomAlgParameters>(parametersPath);

        if(_parameters == null)
        {
            _parameters = Resources.Load<RandomAlgParameters>(_parametersPathDefault);
        }

        _prefabs = _parameters.Prefabs;
    }

    private void OnDestroy()
    {
        if(_parameters != null)
            Resources.UnloadAsset(_parameters);
    }
}