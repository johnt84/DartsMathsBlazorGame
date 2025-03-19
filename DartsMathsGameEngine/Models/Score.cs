using DartsMathsGameEngine.Models.Enums;

namespace DartsMathsGameEngine.Models;

public class Score
{
    public ScoreArea ScoreArea { get; set; }
    public int? ScoreValue { get; set; }

    public Score(ScoreArea scoreArea, int? scoreValue = null)
    {
        ScoreArea = scoreArea;
        ScoreValue = scoreValue;
    }
}
