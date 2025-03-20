using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;
using DartsMathsGameEngine.Services;
using FluentAssertions;

namespace DartsMathsEngineTests;

[TestClass]
public sealed class SetUpFinishTests
{
    [TestMethod]
    public void FinishAndScoresGeneratedSeveralTime_AllCanFinish()
    {
        // Arrange
        int numberOfExecutions = 1000;

        // Act
        var scoresForMathsGuesses = new List<ScoreForMathsGuess>();

        for (int i = 0; i < numberOfExecutions; i++)
        {
            scoresForMathsGuesses.Add(CallService());
        }

        // Assert
        var totalScores = new List<int>();
        var leftToScoreAfterFinishes = new List<int>();
        var canFinishes = new List<bool>();

        foreach (var scoreForMathsGuess in scoresForMathsGuesses)
        {
            int totalScore = GetTotalScore(scoreForMathsGuess.Scores!);
            totalScores.Add(totalScore);

            int leftToScoreAfterFinish = scoreForMathsGuess.LeftToScore - totalScore;
            leftToScoreAfterFinishes.Add(leftToScoreAfterFinish);

            bool canFinish = CanFinish(leftToScoreAfterFinish);
            canFinishes.Add(canFinish);
        }

        bool allScoresArePositive = totalScores.All(totalScore => totalScore > 0);
        bool allLeftToScoreArePositive = leftToScoreAfterFinishes.All(leftToScore => leftToScore > 0);
        bool allCanFinish = canFinishes.All(canFinish => canFinish);

        allScoresArePositive.Should().BeTrue();
        allLeftToScoreArePositive.Should().BeTrue();
        allCanFinish.Should().BeTrue();
    }

    private ScoreForMathsGuess CallService(bool completeFinisher = true)
    {
        var service = new DartsMathsService();

        return service.SetUpFinisher(completeFinisher);
    }

    private int GetTotalScore(IEnumerable<Score> scores)
    {
        int totalScore = 0;

        foreach (var score in scores)
        {
            totalScore += GetScoreValue(score);
        }

        return totalScore;
    }

    private int GetScoreValue(Score score) => 
        score.ScoreArea switch
        {
            ScoreArea.Single => score.ScoreValue!.Value,
            ScoreArea.Double => score.ScoreValue!.Value * 2,
            ScoreArea.Treble => score.ScoreValue!.Value * 3,
            ScoreArea.OuterBull => 25,
            ScoreArea.Bullseye => 50,
            _ => throw new ArgumentException("Invalid score area")
        };

    private bool CanFinish(int leftToScore)
    {
        bool CanFinishScore(int value) => value <= 20;

        if (CanFinishScore(leftToScore))
        {
            return true;
        }

        bool canTreble = leftToScore % 3 == 0;
        bool canDouble = leftToScore % 2 == 0;

        if (!canTreble && !canDouble)
        {
            return false;
        }

        int divideByThree = leftToScore / 3;

        if (canTreble && CanFinishScore(divideByThree))
        {
            return true;
        }

        int divideByTwo = leftToScore / 2;

        if (canDouble && CanFinishScore(divideByTwo))
        {
            return true;
        }

        return false;
    }
}
