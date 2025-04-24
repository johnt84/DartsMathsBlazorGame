using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DartsMathsBlazorGame.Components;

public partial class ScoreGuideButton
{
    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    public async Task OnScoreGuideClickAsync()
    {
        var options = new DialogOptions
        {
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
            CloseButton = true
        };

        var dialog = await DialogService.ShowAsync<ScoreGuideDialog>("Score Guide", options);
        await dialog.Result;
    }
}
