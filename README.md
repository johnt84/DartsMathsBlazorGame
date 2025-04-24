# Darts Maths Blazor Game

Darts Maths Blazor Game where the player guesses the correct dart that would complete the game.

The game provides maths questions in the form of darts scores.

The player is given a score that is left to finish with 2 darts already thrown and the player has to select which dart would succesfully complete the scoring (i.e there would be no score left after the last dart was thrown).  The final dart has to provide a successful checkout of either a double (1 - 20) or a bullseye (50).

For example if the left to score is 69 and the first dart thrown is a 16 and the second dart thrown is an Outer Bull (25) then the correct finishing dart is Double 14 as:-

* 16 + 25 = 41
* Need to score 69 - 41 = 28 so should throw a double 14 to successfully checkout

![image](https://github.com/user-attachments/assets/5b7b68dc-124d-4d75-8da4-4c6b7fd00d91)

[Dart scoring rules are described further here](https://www.dartscorner.co.uk/blogs/how-to/the-rules-of-darts-explained)

* The game is Developed using Blazor Server/.Net 8 and utilises MudBlazor for the GUI Component framework
* There is a unit test app which uses MS Test .Net 8 and utilises the FluentAssertions library for the test assertions on the GameEngine Service
