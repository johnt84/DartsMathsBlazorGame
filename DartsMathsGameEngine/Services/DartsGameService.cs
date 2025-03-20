using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;

namespace DartsMathsGameEngine.Services;

public class DartsGameService : IDartsGameService
{
    private int ScoreTotal { get; set; }
    private int NumberOfDartsThrown { get; set; }

    private const int GameTotal = 501;

    private const int BullseyeScore = 50;

    private const int OuterBullScore = 25;

    public DartsGameService()
    {
        ScoreTotal = GameTotal;
        NumberOfDartsThrown = 0;
    }

    public ScoreEntry CalculateScore(IEnumerable<Score> scores)
    {
        int scoreEntryValue = 0;
        
        foreach(var score in scores)
        {
            scoreEntryValue += GetScoreValue(score);
            NumberOfDartsThrown++;
        }

        if (scoreEntryValue == ScoreTotal)
        {
            var lastDart = scores.Last().ScoreArea;

            bool lastDartWasBullseye = lastDart == ScoreArea.Bullseye;
            bool lastDartWasDouble = lastDart == ScoreArea.Double;

            if (lastDartWasBullseye || lastDartWasDouble) 
            {
                return new ScoreEntry
                {
                    ScoreValue = scoreEntryValue,
                    LeftToScore = 0,
                    GameWon = true,
                    NumberOfDartsThrown = NumberOfDartsThrown
                };
            }

            return new ScoreEntry
            {
                ScoreValue = scoreEntryValue,
                LeftToScore = ScoreTotal,
                Bust = true,
                NumberOfDartsThrown = NumberOfDartsThrown
            };
        }

        if (scoreEntryValue > ScoreTotal)
        {
            return new ScoreEntry
            {
                ScoreValue = scoreEntryValue,
                LeftToScore = ScoreTotal,
                Bust = true,
                NumberOfDartsThrown = NumberOfDartsThrown
            };
        }

        ScoreTotal -= scoreEntryValue;

        return new ScoreEntry
        {
            ScoreValue = scoreEntryValue,
            LeftToScore = ScoreTotal,
            NumberOfDartsThrown = NumberOfDartsThrown
        };
    }

    private int GetScoreValue(Score score)
    {
        var bullseyes = new List<ScoreArea> { ScoreArea.Bullseye, ScoreArea.OuterBull };

        if (!bullseyes.Contains(score.ScoreArea) && score.ScoreValue is null)
        {
            throw new ArgumentException("Score value must be provided for non-bullseye scores");
        }
        
        return score.ScoreArea switch
        {
            ScoreArea.Single => score.ScoreValue!.Value,
            ScoreArea.Double => score.ScoreValue!.Value * 2,
            ScoreArea.Treble => score.ScoreValue!.Value * 3,
            ScoreArea.OuterBull => OuterBullScore,
            ScoreArea.Bullseye => BullseyeScore,
            _ => throw new ArgumentException("Invalid score area")
        };
    }
}
