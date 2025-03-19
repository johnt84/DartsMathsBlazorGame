namespace DartsMathsGameEngine.Models;

public class ScoreEntry
{
    public int ScoreValue { get; set; }
    public int LeftToScore { get; set; }
    public bool GameWon { get; set; }
    public bool Bust { get; set; }
    public int NumberOfDartsThrown { get; set; }
}
