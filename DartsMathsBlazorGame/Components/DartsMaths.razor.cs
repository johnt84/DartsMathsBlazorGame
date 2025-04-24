using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;
using DartsMathsGameEngine.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

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

    private string? GuessStatusIcon(bool? isGuessCorrect) => 
        isGuessCorrect switch
        {
            true => Icons.Material.Filled.CheckCircle,
            false => Icons.Material.Filled.Error,
            _ => null
        };

    private Color GuessStatusColor(bool? isGuessCorrect) => 
        isGuessCorrect switch
        {
            true => Color.Success,
            false => Color.Error,
            _ => Color.Default
        };

    protected override void OnInitialized() => SetUpGuess();

    private void OnGuessClick()
    {
        var score = new Score(SelectedScoreArea, scoreValue: ScoreValue);

        IsGuessCorrect = DartsMathsService.CompleteFinisherGuess(score);
    }

    private void OnNextClick() => SetUpGuess();

    private void SetUpGuess()
    {
        ScoreForMathsGuess = DartsMathsService.SetUpFinisher(true);
        ClearGuess();
    }

    private void ClearGuess()
    {
        SelectedScoreArea = ScoreArea.Double;
        ScoreValue = 1;
        IsGuessCorrect = null;
    }
}
