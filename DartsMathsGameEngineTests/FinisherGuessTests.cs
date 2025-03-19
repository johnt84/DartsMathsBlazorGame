using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;
using DartsMathsGameEngine.Services;
using FluentAssertions;

namespace DartsMathsEngineTests;

[TestClass]
public sealed class FinisherGuessTests
{
    [TestMethod]
    public void NoScoreForMatchGuess_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score
        {
            ScoreArea = ScoreArea.Treble,
            ScoreValue = 20
        };

        var score = new Score
        {
            ScoreArea = ScoreArea.InnerBull
        };

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
            score
        };

        // Act
        bool isCorrectGuess = CallService(null, scores);

        // Assert
        isCorrectGuess.Should().BeFalse();
    }


    [TestMethod]
    public void LeftToScoreHasNoFinish_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score
        {
            ScoreArea = ScoreArea.Treble,
            ScoreValue = 20
        };

        var score = new Score
        {
            ScoreArea = ScoreArea.Double,
            ScoreValue = 20
        };

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
            score
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 159
        };

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, scores);

        // Assert
        isCorrectGuess.Should().BeFalse();
    }

    [TestMethod]
    public void LastThrowIsNotADouble_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score
        {
            ScoreArea = ScoreArea.Treble,
            ScoreValue = 20
        };

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
            trebleTwenty
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 180,
            Scores = scores
        };

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, scores);

        // Assert
        isCorrectGuess.Should().BeFalse();
    }

    [TestMethod]
    public void IncorrectFinishScoreGuessed_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score
        {
            ScoreArea = ScoreArea.Treble,
            ScoreValue = 20
        };

        var score = new Score
        {
            ScoreArea = ScoreArea.OuterBull
        };

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
            score
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 170,
            Scores = scores
        };

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, scores);

        // Assert
        isCorrectGuess.Should().BeFalse();
    }

    [TestMethod]
    public void CorrectFinishScoreGuessed_GuessIsCorrect()
    {
        // Arrange
        var trebleTwenty = new Score
        {
            ScoreArea = ScoreArea.Treble,
            ScoreValue = 20
        };

        var score = new Score
        {
            ScoreArea = ScoreArea.InnerBull
        };

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
            score
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 170,
            Scores = scores
        };

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, scores);

        // Assert
        isCorrectGuess.Should().BeTrue();
    }

    private bool CallService(ScoreForMathsGuess? scoreForMathsGuess, IEnumerable<Score> scores)
    {
        var service = new DartsMathsService();

        service.ScoreForMathsGuess = scoreForMathsGuess;

        return service.FinisherGuess(scores);
    }
}
