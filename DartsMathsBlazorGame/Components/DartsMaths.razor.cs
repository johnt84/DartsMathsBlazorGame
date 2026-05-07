using DartsMathsGameEngine.Logic;
using DartsMathsGameEngine.Models;
using DartsMathsGameEngine.Models.Enums;
using DartsMathsGameEngine.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DartsMathsBlazorGame.Components;

public partial class DartsMaths
{
    [Inject]
    private IDartsGameEngine DartsGameEngine { get; set; } = null!;

    [Inject]
    private IDartsMathsService DartsMathsService { get; set; } = null!;

    private GameState GameState { get; set; } = null!;

    private ScoreForMathsGuess? ScoreForMathsGuess { get; set; }

    private ScoreArea SelectedScoreArea { get; set; }

    private int ScoreValue { get; set; }

    private bool? IsGuessCorrect { get; set; }

    private bool NewGame { get; set; }

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

    protected override void OnInitialized() =>
        StartNewGame();

    private void OnGuessClick()
    {
        var score = new Score(SelectedScoreArea, scoreValue: ScoreValue);

        IsGuessCorrect = DartsMathsService.CompleteFinisherGuess(score);

        DartsGameEngine.QuestionAnswered(IsGuessCorrect.Value);
    }

    private void OnNextClick()
    {
        SetUpGuess();

        IsGuessCorrect = null;
    }

    private void OnNewGameButtonClick() =>
        StartNewGame();

    private void SetUpGuess()
    {
        ScoreForMathsGuess = DartsMathsService.SetUpFinisher(true);
        ClearGuess();
    }

    private void StartNewGame()
    {
        GameState = DartsGameEngine.StartNewGame();
        SetUpGuess();
        NewGame = true;
    }

    private void ClearGuess()
    {
        SelectedScoreArea = ScoreArea.Double;
        ScoreValue = 1;
        IsGuessCorrect = null;
    }
}
