# Darts Maths Blazor Game

Darts Maths Blazor Game where the player guesses the correct dart that would complete the game.

The game provides maths questions in the form of darts scores.

The player is given a score that is left to finish with 2 darts already thrown and the player has to select which dart would succesfully complete the scoring (i.e there would be no score left after the last dart was thrown).  The final dart has to provide a successful checkout of either a double (1 - 20) or a bullseye (50).

For example if the left to score is 69 and the first dart thrown is a 16 and the second dart thrown is an Outer Bull (25) then the correct finishing dart is Double 14 as:-

* 16 + 25 = 41
* Need to score 69 - 41 = 28 so should throw a double 14 to successfully checkout

**Correct Guess**

<img width="660" height="564" alt="image" src="https://github.com/user-attachments/assets/fe69ed55-1432-4da7-8a8a-7b4fead16197" />

**Incorrect Guess**

<img width="587" height="530" alt="image" src="https://github.com/user-attachments/assets/ac513372-3055-4d9f-9977-406b2af9716d" />

[Dart scoring rules are described further here](https://www.dartscorner.co.uk/blogs/how-to/the-rules-of-darts-explained)

* The game is Developed using Blazor Server/.Net 10 and utilises MudBlazor for the GUI Component framework
* There is a unit test app which tests the GameEngine service and uses the XUnit .Net 10 Test Framework
