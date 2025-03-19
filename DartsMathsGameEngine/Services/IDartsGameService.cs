using DartsMathsGameEngine.Models;

namespace DartsMathsGameEngine.Services;

public interface IDartsGameService
{
    ScoreEntry CalculateScore(IEnumerable<Score> scores);
}
