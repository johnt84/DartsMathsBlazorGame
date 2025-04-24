using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;

namespace DartsMathsGameEngine.Services;

public class DartsMathsService : IDartsMathsService
{
    public ScoreForMathsGuess? ScoreForMathsGuess { get; set; }

    private const int BullseyeScore = 50;

    private const int OuterBullScore = 25;

    private const int MinimumFinalScore = 41;

    private const int MaximumFinalScore = 155;

    private const int NumberOfScoresInFinisher = 3;

    private const int TrebleMultiplier = 3;

    private const int DoubleMultiplier = 2;

    private const int MaxScoreValue = 20;

    public DartsMathsService()
    {
        ScoreForMathsGuess = null;
    }

    public ScoreForMathsGuess SetUpFinisher(bool completeFinisher)
    {
        int leftToScore = GenerateFinisherScore();

        if (completeFinisher)
        {
            ScoreForMathsGuess = GenerateScores(leftToScore);

            return ScoreForMathsGuess;
        }

        ScoreForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = leftToScore
        };

        return ScoreForMathsGuess;
    }

    public bool FinisherGuess(IEnumerable<Score> scores)
    {
        if (ScoreForMathsGuess == null || ScoreForMathsGuess.Scores is null)
        {
            return false;
        }

        if (scores.Count() != NumberOfScoresInFinisher)
        {
            return false;
        }

        int scoreValue = 0;

        foreach (var score in scores)
        {
            scoreValue += GetScoreValue(score);
        }

        if (scoreValue == ScoreForMathsGuess.LeftToScore)
        {
            var lastDart = scores.Last().ScoreArea;

            bool lastDartWasBullseye = lastDart == ScoreArea.Bullseye;
            bool lastDartWasDouble = lastDart == ScoreArea.Double;

            if (lastDartWasBullseye || lastDartWasDouble)
            {
                ScoreForMathsGuess = null;

                return true;
            }

            return false;
        }

        return false;
    }

    public bool CompleteFinisherGuess(Score score)
    {
        if (ScoreForMathsGuess is null || ScoreForMathsGuess.Scores is null)
        {
            return false;
        }

        if (ScoreForMathsGuess.Scores.Count() != NumberOfScoresInFinisher - 1)
        {
            return false;
        }

        if (score.ScoreArea == ScoreArea.Bullseye && score.ScoreValue == BullseyeScore)
        {
            return true;
        }

        int scoreValue = 0;

        var completeScores = new List<Score>();

        completeScores.AddRange(ScoreForMathsGuess.Scores);
        completeScores.Add(score);

        bool lastScoreIsADoubleOut = score.ScoreArea == ScoreArea.Double || score.ScoreArea == ScoreArea.Bullseye;

        if (!lastScoreIsADoubleOut)
        {
            return false;
        }

        foreach (var completeScore in completeScores)
        {
            scoreValue += GetScoreValue(completeScore);
        }

        if (scoreValue == ScoreForMathsGuess.LeftToScore)
        {
            ScoreForMathsGuess = null;

            return true;
        }

        return false;
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
            ScoreArea.Double => score.ScoreValue!.Value * DoubleMultiplier,
            ScoreArea.Treble => score.ScoreValue!.Value * TrebleMultiplier,
            ScoreArea.OuterBull => OuterBullScore,
            ScoreArea.Bullseye => BullseyeScore,
            _ => throw new ArgumentException("Invalid score area")
        };
    }

    private int GenerateFinisherScore()
    {
        var random = new Random();

    int finisherScore = 169;

    var nonFinishers = new List<int> { 159, 162, 163, 165, 166, 168, 169 };

        while (nonFinishers.Contains(finisherScore))
        {
            finisherScore = random.Next(MinimumFinalScore, MaximumFinalScore + 1);
        };

        return finisherScore;
    }

    private ScoreForMathsGuess GenerateScores(int finisherScore)
    {
        int scoreSum = finisherScore;

        var generatedScores = new List<Score>();

        Score? finishingScore = null;

        while (scoreSum >= finisherScore || finishingScore is null)
        {
            generatedScores.Clear();
            generatedScores.Add(GenerateScore());
            generatedScores.Add(GenerateScore());

            int currentScoreIndex = 0;

            foreach (var score in generatedScores)
            {
                if (currentScoreIndex == 0)
                {
                    scoreSum = 0;
                }
                
                scoreSum += GetScoreValue(score);

                currentScoreIndex++;
            }

            int leftToScore = finisherScore - scoreSum;

            finishingScore = CanFinish(leftToScore);
        }

        return new ScoreForMathsGuess
        {
            LeftToScore = finisherScore,
            Scores = generatedScores,
            ScoreToFinish = finishingScore!
        };
    }

    private Score GenerateScore()
    {
        var random = new Random();

        int scoreArea = random.Next(0, 5);

        var bullseyes = new List<ScoreArea> { ScoreArea.Bullseye, ScoreArea.OuterBull };

        if (bullseyes.Contains((ScoreArea)scoreArea))
        {
            return new Score((ScoreArea)scoreArea);
        }

        int scoreValue = random.Next(15, 21);

        return new Score((ScoreArea)scoreArea, scoreValue: scoreValue);
    }

    private Score? CanFinish(int leftToScore)
    {
        bool isFinishable(int value) => value > 0 && value <= MaxScoreValue;

        if (leftToScore == BullseyeScore)
        {
            return new Score(ScoreArea.Bullseye);
        }

        bool isDouble = leftToScore % DoubleMultiplier == 0;
        int divideByTwo = leftToScore / DoubleMultiplier;

        bool isDoubleOut = isDouble && isFinishable(divideByTwo);

        if (isDoubleOut)
        {
            return new Score(ScoreArea.Double, divideByTwo);
        }


        return null;
    }
}
