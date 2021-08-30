using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterAlg : GenerationAlgorithm
{
    [SerializeField] private CenterAlgParameters _parameters;

    private string _parametersPath;

    protected override IEnumerator GenerationCorutine()
    {
        Vector2 center = new Vector2((_maxBorders.x + _minBorders.x) / 2, (_maxBorders.y + _minBorders.y) / 2);

        _generator.Spawn(center);
        yield return new WaitForSecondsRealtime(1);
        _generator.SpawnWaveEnder();
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
        _parametersPath = PlayerPrefs.GetString("CenterAlg", "CenterAlgDefault");
        _parameters = Resources.Load(_parametersPath) as CenterAlgParameters;
        _prefabs = _parameters.Prefabs;
    }
}
