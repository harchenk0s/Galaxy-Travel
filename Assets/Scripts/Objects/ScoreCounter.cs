using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private float _pointFactor;
    [SerializeField] private int _startPoints;

    protected float _points;
    protected int _numberCollisions;
    protected ShipEvents _shipEvents;
    protected LevelBuilder _levelBuilder;

    private IEnumerator _countingCoroutine = null;
    private bool _counting = true;

    public FloatEvent PointsChangeEvent;
    
    public float Points
    {
        get { return (int)Mathf.Clamp(_points, 0, float.PositiveInfinity); }
        private set
        {
            _points = value;
            PointsChangeEvent.Invoke(Points);
        }
    }

    protected virtual void Start()
    {
        _levelBuilder = FindObjectOfType<LevelBuilder>();
        _levelBuilder.StartGameEvent.AddListener(StartCounting);
        _levelBuilder.EndLevelEvent.AddListener(EndCounting);
        _levelBuilder.GameOverEvent.AddListener(GameOver);
    }

    protected virtual void StartCounting()
    {
        Points = _startPoints;

        if(_countingCoroutine != null)
        {
            StopCoroutine(_countingCoroutine);
        }

        _countingCoroutine = CountingCoroutine();
        _counting = true;
        StartCoroutine(_countingCoroutine);
    }

    protected virtual void EndCounting()
    {
        _counting = false;
    }

    protected virtual void GameOver()
    {
        Points = 0;
    }

    private IEnumerator CountingCoroutine()
    {
        while (_counting)
        {
            Points -= _pointFactor;
            yield return new WaitForFixedUpdate();
        }
        _countingCoroutine = null;
    }

    private void OnDestroy()
    {
        PointsChangeEvent.RemoveAllListeners();
    }
}
