using DartsMathsGameEngine.Services;
using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;

var service = new DartsMathsService();

bool playAgain = true;

while (playAgain)
{
    var scoresForCompleteFinisher = service.SetUpFinisher(true);

    Console.WriteLine($"\nLeft to score: {scoresForCompleteFinisher.LeftToScore}");

    foreach (var score in scoresForCompleteFinisher.Scores)
    {
        Console.WriteLine($"\nScore: {score.ScoreLabel}");
    }

    Console.WriteLine("\nEnter a Score Area (0 - 2)?  Single-0, Double-1, Treble-2, OuterBull-3, Bullseye-4");
    int scoreArea = Convert.ToInt32(Console.ReadLine());

    var scoringArea = (ScoreArea)scoreArea;

    int? scoreValue = null;

    if (scoringArea != ScoreArea.Bullseye && scoringArea != ScoreArea.OuterBull)
    {
        Console.WriteLine("\nEnter a Score value?");
        scoreValue = Convert.ToInt32(Console.ReadLine());
    }

    var playerGuess = new Score(scoringArea, scoreValue: scoreValue);

    bool isCorrect = service.CompleteFinisherGuess(playerGuess);

    if (isCorrect)
    {
        Console.WriteLine("\nCorrect!");
    }
    else
    {
        Console.WriteLine("\nIncorrect!");

        Console.WriteLine($"\nCorrect Score is {scoresForCompleteFinisher.ScoreToFinish.ScoreLabel}");
    }

    Console.WriteLine("\nPlay again (y/n)?");
    string playAgainInput = Console.ReadLine();

    playAgain = playAgainInput == "y";
}

var playerGuess2 = new Score(ScoreArea.Single, scoreValue: 16);

bool isCorrect2 = service.CompleteFinisherGuess(playerGuess2);

var service2 = new DartsMathsService();

var scoresForFinisher = service2.SetUpFinisher(false);

var playerGuess3 = new List<Score>
{
    new Score(ScoreArea.Treble, scoreValue: 19),
    new Score(ScoreArea.Single, scoreValue: 16),
    new Score(ScoreArea.Bullseye)
};

bool isCorrect3 = service2.FinisherGuess(playerGuess3);

var playerGuess4 = new List<Score>
{
    new Score(ScoreArea.Treble, scoreValue: 19),
    new Score(ScoreArea.Single, scoreValue: 16),
    new Score(ScoreArea.OuterBull)
};

bool isCorrect4 = service.FinisherGuess(playerGuess4);

/*var dartsScores = new List<Score> 
{
    new Score { ScoreArea = ScoreArea.Treble, ScoreValue = 20 },
    new Score { ScoreArea = ScoreArea.Treble, ScoreValue = 20 },
    new Score { ScoreArea = ScoreArea.Treble, ScoreValue = 20 }
};

var service  = new DartsGameService();

var scoreEntry = service.CalculateScore(dartsScores);

var secondDartsScores = new List<Score>
{
    new Score { ScoreArea = ScoreArea.Treble, ScoreValue = 20 },
    new Score { ScoreArea = ScoreArea.Treble, ScoreValue = 20 },
    new Score { ScoreArea = ScoreArea.Treble, ScoreValue = 20 }
};

var secondScoreEntry = service.CalculateScore(secondDartsScores);

var thirdDartsScores = new List<Score>
{
    new Score { ScoreArea = ScoreArea.Double, ScoreValue = 10 },
    new Score { ScoreArea = ScoreArea.Double, ScoreValue = 10 },
    new Score { ScoreArea = ScoreArea.Single, ScoreValue = 5 }
};

var thirdScoreEntry = service.CalculateScore(thirdDartsScores);

var fourthDartsScores = new List<Score>
{
    new Score { ScoreArea = ScoreArea.Bullseye },
    new Score { ScoreArea = ScoreArea.Single, ScoreValue = 6 },
    new Score { ScoreArea = ScoreArea.Double, ScoreValue = 20 }
};

var fourthScoreEntry = service.CalculateScore(fourthDartsScores);*/

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
