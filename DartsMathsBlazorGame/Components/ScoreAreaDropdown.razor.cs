using DartsMathsGameEngine.Models.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DartsMathsBlazorGame.Components;

public partial class ScoreAreaDropdown
{
    [Parameter]
    public ScoreArea ScoringArea { get; set; }

    [Parameter]
    public EventCallback<ScoreArea> ScoringAreaChanged { get; set; }

    [Parameter]
    public string Label { get; set; } = "Score Area";

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public bool ReadOnly { get; set; }

    [Parameter]
    public Adornment Adornment { get; set; }

    [Parameter]
    public string? AdornmentIcon { get; set; }

    [Parameter]
    public Color AdornmentColor { get; set; }

    private List<ScoreArea> ScoreAreas { get; set; } = new List<ScoreArea>();

    protected override void OnInitialized()
    {
        var scoreAreas = Enum.GetValues<ScoreArea>().ToList();

        var finishingScoreAreas = new List<ScoreArea>
        {
            ScoreArea.Double,
            ScoreArea.Bullseye
        };

        ScoreAreas = scoreAreas
                        .Where(scoreArea => finishingScoreAreas.Contains(scoreArea))
                        .ToList();
    }
}
