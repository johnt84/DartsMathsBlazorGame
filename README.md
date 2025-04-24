# Darts Maths Blazor Game

Darts Maths game developed in Blazor which provides maths questions in the form of darts scores.

The player is given a score that is left to finish with 2 darts already thrown and the player has to select which dart would succesfully complete the scoring (i.e there would be no score left after the last dart was thrown).  The final dart has to provide a successful checkout of either a double (1 - 20) or a bullseye (50).

For example if the left to score is 69 and the first dart thrown is a 16 and the second dart throws is an Outer Bull (25) then the correct finishing dart is Double 14 as:-

* 16 + 25 = 41
* Need to score 69 - 41 = 28 so should throw a double 14

![image](https://github.com/user-attachments/assets/5b7b68dc-124d-4d75-8da4-4c6b7fd00d91)

* The game is Developed using Blazor Server/.Net 8 and utlises MudBlazor for the GUI Component framework
* There is a unit test app which uses MS Test .Net 8 and utilises the FluentAssertions library for the test assertions on the GameEngine Service
