using DartsMathsGameEngine.Models.Enums;
using Microsoft.AspNetCore.Components;

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

    private List<ScoreArea> ScoreAreas { get; set; } = new List<ScoreArea>();

    protected override void OnInitialized()
    {
        ScoreAreas = Enum.GetValues<ScoreArea>().ToList();
    }
}
