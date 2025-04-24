using Microsoft.AspNetCore.Components;
using MudBlazor;    

namespace DartsMathsBlazorGame.Components;

public partial class ScoreGuideDialog
{
    [CascadingParameter]
    public IMudDialogInstance MudDialog { get; set; } = null!;

    private void Close() => MudDialog.Cancel();

    private string ScoringRules = @"The aim of the game is to guess the correct final dart which will complete the game with a successful checkout (i.e. nothing left in total score).

Darts scoring consists of:

* A single score area (1-20) - score * 1 (i.e. single 16 is 16 points) 
* A double score area (1-20) - score * 2 (i.e. double 16 is 32 points (i.e. 16 * 2 ))
* A treble score area (1-20) - score * 3 (i.e. treble 16 is 48 points (i.e. 16 * 3)) 
* Outer bull is 25 points
* Bullseye (Inner bull) is 50 points

The final dart which gives a successful checkout must be a double (1- 20) or a bullseye.
";
}
