using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private List<Modifier> _modifiers = new List<Modifier>();

    public int Rating { get; }
    public int TotalScore { get; }

    public List<Modifier> Modifiers => new List<Modifier>(_modifiers);

    public Score(int rating, int totalScore, List<Modifier> modifiers)
    {
        Rating = rating;
        TotalScore = totalScore;
        _modifiers = modifiers;
    }
}
