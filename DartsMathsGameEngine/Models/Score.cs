using DartsMathsGameEngine.Models.Enums;

namespace DartsMathsGameEngine.Models;

public class Score
{
    public ScoreArea ScoreArea { get; set; }
    public int? ScoreValue { get; set; }

    public string ScoreLabel =>
        ScoreArea == ScoreArea.Bullseye || ScoreArea == ScoreArea.OuterBull ? 
            ScoreArea.ToString() : NonBullseyeLabel;

    private string NonBullseyeLabel =>
        ScoreArea != ScoreArea.Single ? $"{ScoreArea}-{ScoreValue!}" : ScoreValue!.Value.ToString();

    public Score(ScoreArea scoreArea, int? scoreValue = null)
    {
        ScoreArea = scoreArea;
        ScoreValue = scoreValue;
    }
}
