using DartsMathsGameEngine.Models;

namespace DartsMathsGameEngine.Logic;

public interface IDartsGameEngine
{
    GameState StartNewGame();
    GameState QuestionAnswered(bool correctAnswer);
}
