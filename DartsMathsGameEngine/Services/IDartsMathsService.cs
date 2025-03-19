using DartsMathsGameEngine.Models;

namespace DartsMathsGameEngine.Services;

public interface IDartsMathsService
{
    public ScoreForMathsGuess SetUpFinisher(bool completeFinisher);
    public bool FinisherGuess(IEnumerable<Score> scores);
    public bool CompleteFinisherGuess(Score score);
}
