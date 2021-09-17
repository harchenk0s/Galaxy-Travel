using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterAlg : GenerationAlgorithm
{
    [SerializeField] private CenterAlgParameters _parameters;

    private string _parametersPathDefault = Strings.AlgorithmsParameters.CenterAlg.CenterAlgDefault;

    protected override IEnumerator GenerationCorutine()
    {
        Vector2 center = new Vector2((MaxBorders.x + MinBorders.x) / 2, (MaxBorders.y + MinBorders.y) / 2);

        Generator.Spawn(center);
        yield return new WaitForSecondsRealtime(1);
        Generator.SpawnWaveEnder();
        yield return null;
    }

    private void OnDestroy()
    {
        if (_parameters != null)
            Resources.UnloadAsset(_parameters);
    }

    private new void Awake()
    {
        base.Awake();
        string parametersPath = PlayerPrefs.GetString(Strings.Algorithms.CenterAlg);
        _parameters = Resources.Load<CenterAlgParameters>(parametersPath);

        if(_parameters == null)
        {
            Resources.Load<CenterAlgParameters>(_parametersPathDefault);
        }
        Prefabs = _parameters.PrefabsList;
    }
}
