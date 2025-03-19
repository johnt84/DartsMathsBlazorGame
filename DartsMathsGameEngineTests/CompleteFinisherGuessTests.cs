using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;
using DartsMathsGameEngine.Services;
using FluentAssertions;

namespace DartsMathsEngineTests;

[TestClass]
public sealed class CompleteFinisherGuessTests
{
    [TestMethod]
    public void NoScoreForMathsGuess_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
        };

        var score = new Score(ScoreArea.InnerBull);

        // Act
        bool isCorrectGuess = CallService(null, score);

        // Assert
        isCorrectGuess.Should().BeFalse();
    }

    [TestMethod]
    public void NoScoresOnScoreForMathsGuess_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 170
        };

        var score = new Score(ScoreArea.InnerBull);

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, score);

        // Assert
        isCorrectGuess.Should().BeFalse();
    }

    [TestMethod]
    public void LeftToScoreHasNoFinish_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 159,
            Scores = scores
        };

        var score = new Score(ScoreArea.Double, scoreValue: 20);

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, score);

        // Assert
        isCorrectGuess.Should().BeFalse();
    }

    [TestMethod]
    public void LastThrowIsNotADouble_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 180,
            Scores = scores
        };

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, trebleTwenty);

        // Assert
        isCorrectGuess.Should().BeFalse();
    }

    [TestMethod]
    public void IncorrectFinishScoreGuessed_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 170,
            Scores = scores
        };

        var score = new Score(ScoreArea.OuterBull);

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, score);

        // Assert
        isCorrectGuess.Should().BeFalse();
    }

    [TestMethod]
    public void CorrectFinishScoreGuessed_GuessIsCorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 170,
            Scores = scores
        };

        var score = new Score(ScoreArea.InnerBull);

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, score);

        // Assert
        isCorrectGuess.Should().BeTrue();
    }

    private bool CallService(ScoreForMathsGuess? scoreForMathsGuess, Score score)
    {
        var service = new DartsMathsService();

        service.ScoreForMathsGuess = scoreForMathsGuess;

        return service.CompleteFinisherGuess(score);
    }
}
