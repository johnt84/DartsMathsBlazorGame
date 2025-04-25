using DartsMathsGameEngine.Models.Enums;
using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Services;

namespace DartsMathsGameEngineTests;

public class FinisherGuessTests
{
    [Fact]
    public void NoScoreForMatchGuess_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var score = new Score(ScoreArea.Bullseye);

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
            score
        };

        // Act
        bool isCorrectGuess = CallService(null, scores);

        // Assert
        Assert.False(isCorrectGuess);
    }

    [Fact]
    public void NoScores_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var score = new Score(ScoreArea.Bullseye);

        var scores = new List<Score>()
        {
            trebleTwenty,
            trebleTwenty,
            score
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 170
        };

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, scores);

        // Assert
        Assert.False(isCorrectGuess);
    }

    [Fact]
    public void WhenNotCorrectNumberOfScores_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var score = new Score(ScoreArea.Bullseye);

        var scores = new List<Score>()
        {
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
        Assert.False(isCorrectGuess);
    }

    [Fact]
    public void LeftToScoreHasNoFinish_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var score = new Score(ScoreArea.Double, scoreValue: 20);

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
        Assert.False(isCorrectGuess);
    }

    [Fact]
    public void LastThrowIsNotADouble_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

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
        Assert.False(isCorrectGuess);
    }

    [Fact]
    public void IncorrectFinishScoreGuessed_GuessIsIncorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var score = new Score(ScoreArea.OuterBull);

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
        Assert.False(isCorrectGuess);
    }

    [Fact]
    public void CorrectFinishScoreGuessedAsABullesye_GuessIsCorrect()
    {
        // Arrange
        var trebleTwenty = new Score(ScoreArea.Treble, scoreValue: 20);

        var score = new Score(ScoreArea.Bullseye);

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
        Assert.True(isCorrectGuess);
    }


    [Fact]
    public void CorrectFinishScoreGuessed_GuessIsCorrect()
    {
        // Arrange
        var scores = new List<Score>()
        {
            new Score(ScoreArea.Single, scoreValue: 16),
            new Score(ScoreArea.OuterBull),
            new Score(ScoreArea.Double, scoreValue: 14),
        };

        var scoresForMathsGuess = new ScoreForMathsGuess
        {
            LeftToScore = 69,
            Scores = scores
        };

        // Act
        bool isCorrectGuess = CallService(scoresForMathsGuess, scores);

        // Assert
        Assert.True(isCorrectGuess);
    }

    private bool CallService(ScoreForMathsGuess? scoreForMathsGuess, IEnumerable<Score> scores)
    {
        var service = new DartsMathsService();

        service.ScoreForMathsGuess = scoreForMathsGuess;

        return service.FinisherGuess(scores);
    }
}
