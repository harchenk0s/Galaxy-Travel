using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAlg : GenerationAlgorithm
{
    [SerializeField] private RandomAlgParameters _parameters;
    private string _parametersPathDefault = Strings.AlgorithmsParameters.RandomAlg.RandomAlgDefault;

    protected override IEnumerator GenerationCorutine()
    {
        while (true)
        {
            Vector2 position =
                new Vector2(Random.Range(MinBorders.x, MaxBorders.x), Random.Range(MinBorders.y, MaxBorders.y));
            Generator.Spawn(position);
            yield return new WaitForSecondsRealtime(Random.Range(_parameters.DelayRange.x, _parameters.DelayRange.y));
        }
    }

    private new void Awake()
    {
        base.Awake();
        string parametersPath = PlayerPrefs.GetString(Strings.Algorithms.RandomAlg);
        _parameters = Resources.Load<RandomAlgParameters>(parametersPath);

        if(_parameters == null)
        {
            _parameters = Resources.Load<RandomAlgParameters>(_parametersPathDefault);
        }

        Prefabs = _parameters.PrefabsList;
    }

    private void OnDestroy()
    {
        if(_parameters != null)
            Resources.UnloadAsset(_parameters);
    }
}
