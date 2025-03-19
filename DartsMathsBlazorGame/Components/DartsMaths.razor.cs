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

    private bool ContainsABull (ScoreArea scoreArea) =>
        scoreArea == ScoreArea.InnerBull || scoreArea == ScoreArea.OuterBull;

    protected override void OnInitialized()
    {
        ScoreForMathsGuess = DartsMathsService.SetUpFinisher(true);
    }
}
