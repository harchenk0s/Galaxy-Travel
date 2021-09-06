using System.Collections;
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
    protected bool _isWin = true;

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
        int score = (int)Score;
        int hitModifireValue = _hitCount == 0 ? _noHitBonus : _hitCount * _hitPenalty * -1;
        int totalScore = score + hitModifireValue;

        Modifier hitModifier = new Modifier($"{_hitCount} Hits: ", hitModifireValue);

        rating = _hitCount == 0 ? 3 : 2;

        if (_score + hitModifireValue < _startScore / 2)
            rating--;

        return new Score(_isWin, rating, score, totalScore, new List<Modifier> { hitModifier });
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
        _hitCount = 0;
        _numberCollisions = 0;

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
        _isWin = true;
    }

    protected virtual void Defeat()
    {
        _isWin = false;
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
        _isWin = true;
    }

    private void OnDestroy()
    {
        ScoreChangeEvent.RemoveAllListeners();
    }
}
