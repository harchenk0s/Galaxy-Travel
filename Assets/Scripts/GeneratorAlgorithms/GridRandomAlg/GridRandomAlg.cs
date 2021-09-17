using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRandomAlg : GenerationAlgorithm
{
    [SerializeField] private GridRandomParameters _parameters;

    private string _parametersPathDefault = Strings.AlgorithmsParameters.GridRandomAlg.GridRandomAlgDefault;
    private float[] _columns;
    private float[] _rows;
    private List<Vector2> _spawnPoints = new List<Vector2>();

    protected override IEnumerator GenerationCorutine()
    {
        while (isBusy)
        {
            Vector2 position = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            _generator.Spawn(position);
            yield return new WaitForSeconds(Random.Range(_parameters.DelayRange.x, _parameters.DelayRange.y));
        }
    }

    private void OnDestroy()
    {
        if (_parameters != null)
            Resources.UnloadAsset(_parameters);
    }

    private new void Awake()
    {
        base.Awake();
        string parametersPath = PlayerPrefs.GetString(Strings.Algorithms.GridRandomAlg);
        _parameters = Resources.Load<GridRandomParameters>(parametersPath);

        if(_parameters == null)
        {
            _parameters = Resources.Load<GridRandomParameters>(_parametersPathDefault);
        }
        _prefabs = _parameters.PrefabsList;
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        _columns = new float[_parameters.Columns];
        _rows = new float[_parameters.Rows];

        float height = _maxBorders.y - _minBorders.y;
        float width = _maxBorders.x - _minBorders.x;

        _columns[0] = _minBorders.x + width / (_parameters.Columns * 2);
        _rows[0] = _minBorders.y + height / (_parameters.Rows * 2);

        for (int i = 1; i < _columns.Length; i++)
        {
            _columns[i] = _columns[0] + width / _columns.Length * i;
        }

        for (int i = 1; i < _rows.Length; i++)
        {
            _rows[i] = _rows[0] + height / _rows.Length * i;
        }

        for (int i = 0; i < _columns.Length; i++)
        {
            for (int j = 0; j < _rows.Length; j++)
            {
                _spawnPoints.Add(new Vector2(_columns[i], _rows[j]));
            }
        }
    }
}

