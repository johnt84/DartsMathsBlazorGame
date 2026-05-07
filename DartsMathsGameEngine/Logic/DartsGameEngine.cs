using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Values;
using Microsoft.Extensions.Configuration;

namespace DartsMathsGameEngine.Logic;

public class DartsGameEngine : IDartsGameEngine
{
    private GameState GameState { get; set; } = null!;

    public DartsGameEngine(IConfiguration Configuration)
    {
        int numberOfQuestions = 0;

        Int32.TryParse(Configuration[Values.NumberOfQuestions], out numberOfQuestions);

        GameState = new GameState
        {
            NumberOfQuestions = numberOfQuestions
        };

        StartNewGame();
    }

    public GameState StartNewGame()
    {
        GameState.GameComplete = false;
        GameState.NumberOfCorrectAnswers = 0;
        GameState.NumberOfIncorrectAnswers = 0;
        GameState.HighestScoreBeaten = false;

        return GameState;
    }

    public GameState QuestionAnswered(bool correctAnswer)
    {
        if (GameState.GameComplete)
        {
            return GameState;
        }
        
        if (correctAnswer)
        {
            GameState.NumberOfCorrectAnswers++;
        }
        else
        {
            GameState.NumberOfIncorrectAnswers++;
        }

        if (IsGameComplete(GameState))
        {
            CompleteGame(GameState);
        }

        return GameState;
    }

    private bool IsGameComplete(GameState gameState) =>
        gameState.TotalQuestionsAnswered >= gameState.NumberOfQuestions;

    private void CompleteGame(GameState gameState)
    {
        gameState.GameComplete = true;

        if (gameState.NumberOfCorrectAnswers > gameState.HighestScore)
        {
            gameState.HighestScoreBeaten = true;
            gameState.HighestScore = gameState.NumberOfCorrectAnswers;
        }
    }
}
