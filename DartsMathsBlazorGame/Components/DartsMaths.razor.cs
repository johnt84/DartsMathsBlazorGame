using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;
using DartsMathsGameEngine.Services;
using Microsoft.AspNetCore.Components;

namespace DartsMathsBlazorGame.Components;

public partial class DartsMaths
{
    [Inject]
    private IDartsMathsService DartsMathsService { get; set; } = null!;

    private ScoreForMathsGuess? ScoreForMathsGuess { get; set; }

    private ScoreArea SelectedScoreArea { get; set; }

    private int ScoreValue { get; set; }

    private bool? IsGuessCorrect { get; set; }

    private bool ContainsABull (ScoreArea scoreArea) =>
        scoreArea == ScoreArea.Bullseye || scoreArea == ScoreArea.OuterBull;

    protected override void OnInitialized()
    {
        SetUpGuess();
    }

    private void Guess()
    {
        var score = new Score(SelectedScoreArea, scoreValue: ScoreValue);

        IsGuessCorrect = DartsMathsService.CompleteFinisherGuess(score);
    }

    private void Next()
    {
        SetUpGuess();
    }

    private void SetUpGuess()
    {
        ScoreForMathsGuess = DartsMathsService.SetUpFinisher(true);
        ClearGuess();
    }

    private void ClearGuess()
    {
        SelectedScoreArea = ScoreArea.Single;
        ScoreValue = 1;
        IsGuessCorrect = null;
    }
}
