﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private float _scoreFactor = 0.5f;
    [SerializeField] private int _startScore = 5000;
    [SerializeField] private int _hitPenalty = 100;
    [SerializeField] private int _noHitBonus = 500;
    protected float _score;
    protected int _numberCollisions;
    protected ShipEvents _shipEvents;
    protected LevelBuilder _levelBuilder;

    private IEnumerator _countingCoroutine = null;
    private bool _counting = true;
    private int _hitCount = 0;

    public FloatEvent ScoreChangeEvent;
    
    public float Score
    {
        get { return Mathf.Clamp(_score, 0, float.PositiveInfinity); }
        private set
        {
            _score = value;
            ScoreChangeEvent.Invoke(Score);
        }
    }

    public virtual Score GetScore()
    {
        int rating = 0;
        int totalScore = (int)Score;
        int hitModifireValue = _hitCount == 0 ? _noHitBonus : _hitCount * _hitPenalty * -1;

        Modifier hitModifier = new Modifier($"Hits count: {_hitCount}", hitModifireValue);

        rating = _hitCount == 0 ? 3 : 2;

        if (_score + hitModifireValue < _startScore / 2)
            rating--;

        return new Score(rating, totalScore, new List<Modifier> { hitModifier });
    }

    protected virtual void Start()
    {
        _levelBuilder = FindObjectOfType<LevelBuilder>();
        _shipEvents = FindObjectOfType<ShipEvents>();
        _levelBuilder.StartGameEvent.AddListener(StartCounting);
        _levelBuilder.EndLevelEvent.AddListener(EndCounting);
        _levelBuilder.DefeatEvent.AddListener(Defeat);
        _shipEvents.ShipHitEvent.AddListener(ShipHit);
    }

    protected virtual void StartCounting()
    {
        Score = _startScore;

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

    protected virtual void Defeat()
    {
        Score = 0;
    }

    private void ShipHit()
    {
        _hitCount++;
    }

    private IEnumerator CountingCoroutine()
    {
        while (_counting)
        {
            Score -= _scoreFactor;
            yield return new WaitForFixedUpdate();
        }
        _countingCoroutine = null;
    }

    private void OnDestroy()
    {
        ScoreChangeEvent.RemoveAllListeners();
    }
}