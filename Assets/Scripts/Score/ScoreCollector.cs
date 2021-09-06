using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollector : MonoBehaviour
{
    public void CollectScore()
    {
        ScoreCounter scoreCounter = FindObjectOfType<ScoreCounter>();
        Score score = scoreCounter.GetScore();

        if (score.IsWin)
        {
            Wallet wallet = FindObjectOfType<Wallet>();
            wallet.Add(score.TotalScore);
        }
    }
}
