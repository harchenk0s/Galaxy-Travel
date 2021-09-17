using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingCounter : ScoreCounter
{
    [SerializeField] private float _scoreFactor;
    [SerializeField] private int _noHitBonus;
    [SerializeField] private int _oneStarRange;
    [SerializeField] private int _twoStarRange;
    [SerializeField] private int _threeStarRange;

    private bool _counting = true;
    private IEnumerator _countingCoroutine = null;

    public override Score GetScore()
    {
        int score = (int)ScoreProperty;
        int bonus = 0;
        int rating = 0;

        if (score > _oneStarRange)
        {
            rating = 1;
            bonus = 500;

            if(score > _twoStarRange)
            {
                rating = 2;
                bonus = 1000;
            }
            if(score > _threeStarRange)
            {
                rating = 3;
                bonus = 3000;
            }
        }

        Modifier rangeModifier = new Modifier("Bonus ", bonus);

        return new Score(IsWin, rating, score, bonus + score, new List<Modifier> { rangeModifier });
    }

    protected override void Defeat()
    {
        EndCounting();
    }

    protected override void EndCounting()
    {
        _counting = false;
        IsWin = true;
    }

    protected override void StartCounting()
    {
        _counting = true;
        ScoreProperty = StartScore;

        if (_countingCoroutine != null)
        {
            StopCoroutine(_countingCoroutine);
        }

        _countingCoroutine = CountingCoroutine();
        StartCoroutine(_countingCoroutine);
    }

    private IEnumerator CountingCoroutine()
    {
        while (_counting)
        {
            ScoreProperty += _scoreFactor;
            yield return new WaitForFixedUpdate();
        }
        _countingCoroutine = null;
    }
}
