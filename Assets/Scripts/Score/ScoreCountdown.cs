using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCountdown : ScoreCounter
{
    [SerializeField] private float _scoreFactor = 0.5f;
    [SerializeField] private int _hitPenalty = 100;
    [SerializeField] private int _noHitBonus = 500;

    private IEnumerator _countingCoroutine = null;
    private bool _counting = true;

    public override Score GetScore()
    {
        int score = (int)Score;
        int hitModifireValue = _numberCollisions == 0 ? _noHitBonus : _numberCollisions * _hitPenalty * -1;
        int totalScore = score + hitModifireValue;

        Modifier hitModifier = new Modifier($"{_numberCollisions} Hits = ", hitModifireValue);

        int rating = _numberCollisions == 0 ? 3 : 2;

        if (_score + hitModifireValue < _startScore / 2)
            rating--;

        return new Score(_isWin, rating, score, totalScore, new List<Modifier> { hitModifier });
    }

    protected override void StartCounting()
    {
        Score = _startScore;
        _numberCollisions = 0;

        if (_countingCoroutine != null)
        {
            StopCoroutine(_countingCoroutine);
        }

        _countingCoroutine = CountingCoroutine();
        _counting = true;
        StartCoroutine(_countingCoroutine);
    }

    protected override void EndCounting()
    {
        _counting = false;
        _isWin = true;
    }

    protected override void Defeat()
    {
        _isWin = false;
        Score = 0;
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
}
