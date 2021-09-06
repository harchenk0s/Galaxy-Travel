using System.Collections;
using System.Collections.Generic;

public class Score
{
    private List<Modifier> _modifiers = new List<Modifier>();

    public bool IsWin { get; }
    public int Rating { get; }
    public int CleanScore { get; }
    public int TotalScore { get; }

    public List<Modifier> Modifiers => new List<Modifier>(_modifiers);

    public Score(bool isWin, int rating, int score, int totalScore, List<Modifier> modifiers)
    {
        IsWin = isWin;
        Rating = rating;
        CleanScore = score;
        TotalScore = totalScore;
        _modifiers = modifiers;
    }
}
