using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounterJohnMode : ScoreCounter
{
    [SerializeField] private int _hitBonus = 200;

    private int _countExtraBonus = 0;

    protected override void StartCounting()
    {
        Score = _startScore;
    }

    protected override void ShipHit()
    {
        base.ShipHit();
        Score += _hitBonus;

        if (_numberCollisions % 10 == 0)
            _countExtraBonus++;
    }

    public override Score GetScore()
    {
        int score = (int)Score;
        int hitModifireValue = _countExtraBonus * 200;
        int coolBouns = Random.Range(100, 500);
        int totalScore = score + hitModifireValue + coolBouns;

        Modifier hitModifier = new Modifier($"{_countExtraBonus} Decade Hits * {_hitBonus} = ", hitModifireValue);
        Modifier randomModifire = new Modifier("Cool bonus = ", coolBouns);

        int rating = 0;

        if(_numberCollisions > 10)
        {
            rating++;
            if(_numberCollisions > 50)
            {
                rating++;
                if (_numberCollisions > 80)
                    rating++;
            }
        }

        _isWin = rating == 0 ? false : true;

        return new Score(_isWin, rating, score, totalScore, new List<Modifier> { hitModifier, randomModifire });
    }

    protected override void EndCounting() { }

    protected override void Defeat() { }
}
