using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounterJohnMode : ScoreCounter
{
    [SerializeField] private int _hitBonus = 200;

    private int _countExtraBonus = 0;

    public override Score GetScore()
    {
        int score = (int)ScoreProperty;
        int hitModifireValue = _countExtraBonus * 200;
        int coolBouns = Random.Range(100, 500);
        int totalScore = score + hitModifireValue + coolBouns;

        Modifier hitModifier = new Modifier($"{_countExtraBonus} Decade Hits * {_hitBonus} = ", hitModifireValue);
        Modifier randomModifire = new Modifier("Cool bonus = ", coolBouns);

        int rating = 0;

        if (NumberCollisions > 10)
        {
            rating++;
            if (NumberCollisions > 50)
            {
                rating++;
                if (NumberCollisions > 80)
                    rating++;
            }
        }

        IsWin = rating == 0 ? false : true;

        return new Score(IsWin, rating, score, totalScore, new List<Modifier> { hitModifier, randomModifire });
    }

    protected override void StartCounting()
    {
        ScoreProperty = StartScore;
    }

    protected override void ShipHit()
    {
        base.ShipHit();
        ScoreProperty += _hitBonus;

        if (NumberCollisions % 10 == 0)
            _countExtraBonus++;
    }

    protected override void EndCounting() { }

    protected override void Defeat() { }
}
