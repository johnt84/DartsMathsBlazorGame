namespace DartsMathsGameEngine.Models;

public class GameState
{
    public int NumberOfQuestions { get; set; }

    public int NumberOfCorrectAnswers { get; set; }
    public int NumberOfIncorrectAnswers { get; set; }

    public bool GameComplete { get; set; }
    public bool HighestScoreBeaten { get; set; }
    public int HighestScore { get; set; }

    public int QuestionsLeftToAnswer => NumberOfQuestions - TotalQuestionsAnswered;
    public int TotalQuestionsAnswered => NumberOfCorrectAnswers + NumberOfIncorrectAnswers;
}
