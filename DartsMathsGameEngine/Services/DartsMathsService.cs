using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;

namespace DartsMathsGameEngine.Services;

public class DartsMathsService : IDartsMathsService
{
    public ScoreForMathsGuess? ScoreForMathsGuess { get; set; }

    private const int BullseyeScore = 50;

    private const int OuterBullScore = 25;

    private const int MinimumFinalScore = 41;

    private const int MaximumFinalScore = 120;

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
        if (ScoreForMathsGuess == null)
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

        int scoreValue = 0;

        var completeScores = new List<Score>();

        completeScores.AddRange(ScoreForMathsGuess.Scores);
        completeScores.Add(score);

        bool scoreContainsValidFinishingThrow = 
            completeScores.Any(score => score.ScoreArea == ScoreArea.Bullseye || score.ScoreArea == ScoreArea.Double);

        if (!scoreContainsValidFinishingThrow)
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
            ScoreArea.Double => score.ScoreValue!.Value * 2,
            ScoreArea.Treble => score.ScoreValue!.Value * 3,
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
        bool containsFinisher = false;

        var generatedScores = new List<Score>();

        Score? finishingScore = null;

        while (scoreSum >= finisherScore || finishingScore is null)
        {
            generatedScores.Clear();
            containsFinisher = false;
            generatedScores.Add(GenerateScore());
            generatedScores.Add(GenerateScore());

            if (generatedScores.Any(score => score.ScoreArea == ScoreArea.Bullseye || score.ScoreArea == ScoreArea.Double))
            {
                containsFinisher = true;
            }

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

            finishingScore = CanFinish(containsFinisher, leftToScore);
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

    private Score? CanFinish(bool scoresContainsFinisher, int leftToScore)
    {
        bool isFinishable(int value) => value > 0 && value <= 20;

        if (scoresContainsFinisher && isFinishable(leftToScore))
        {
            return new Score(ScoreArea.Single, leftToScore);
        }

        bool canTreble = leftToScore % 3 == 0;
        bool canDouble = leftToScore % 2 == 0;

        if (canTreble && canDouble)
        {
            return null;
        }

        int trebleValue = leftToScore / 3;

        if (scoresContainsFinisher && canTreble && isFinishable(trebleValue))
        {
            return new Score(ScoreArea.Treble, trebleValue);
        }

        int doubleValue = leftToScore / 2;

        if (canDouble && isFinishable(doubleValue))
        {
            return new Score(ScoreArea.Double, doubleValue);
        }

        return null;
    }
}
