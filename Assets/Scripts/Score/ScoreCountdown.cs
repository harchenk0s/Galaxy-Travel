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
        int score = (int)ScoreProperty;
        int hitModifireValue = NumberCollisions == 0 ? _noHitBonus : NumberCollisions * _hitPenalty * -1;
        int totalScore = score + hitModifireValue;

        Modifier hitModifier = new Modifier($"{NumberCollisions} Hits = ", hitModifireValue);

        int rating = NumberCollisions == 0 ? 3 : 2;

        if (Score + hitModifireValue < StartScore / 2)
            rating--;

        return new Score(IsWin, rating, score, totalScore, new List<Modifier> { hitModifier });
    }

    protected override void StartCounting()
    {
        ScoreProperty = StartScore;
        NumberCollisions = 0;

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
        IsWin = true;
    }

    protected override void Defeat()
    {
        IsWin = false;
        ScoreProperty = 0;
    }

    private IEnumerator CountingCoroutine()
    {
        while (_counting)
        {
            ScoreProperty -= _scoreFactor;
            yield return new WaitForFixedUpdate();
        }
        _countingCoroutine = null;
        IsWin = true;
    }
}
