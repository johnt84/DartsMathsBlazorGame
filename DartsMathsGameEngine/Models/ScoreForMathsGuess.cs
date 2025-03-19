namespace DartsMathsGameEngine.Models;

public class ScoreForMathsGuess
{
    public int LeftToScore { get; set; }
    public IEnumerable<Score>? Scores { get; set; }
}
