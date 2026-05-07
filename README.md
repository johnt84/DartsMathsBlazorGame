# Darts Maths Blazor Game

Darts Maths Blazor Game where the player guesses the correct dart that would complete the game.

The game provides maths questions in the form of darts scores.

The player is given a score that is left to finish with 2 darts already thrown and the player has to select which dart would succesfully complete the scoring (i.e there would be no score left after the last dart was thrown).  The final dart has to provide a successful checkout of either a double (1 - 20) or a bullseye (50).

For example if the left to score is 69 and the first dart thrown is a 16 and the second dart thrown is an Outer Bull (25) then the correct finishing dart is Double 14 as:-

* 16 + 25 = 41
* Need to score 69 - 41 = 28 so should throw a double 14 to successfully checkout

**Correct Guess**

<img width="633" height="505" alt="image" src="https://github.com/user-attachments/assets/85f34b2a-d4fc-4db3-9690-942bb2943dbd" /><br>

**Incorrect Guess**

<img width="645" height="521" alt="image" src="https://github.com/user-attachments/assets/cb033094-431a-48d0-8d5d-c1da83d3132c" /><br>

[Dart scoring rules are described further here](https://www.dartscorner.co.uk/blogs/how-to/the-rules-of-darts-explained)

* The game is Developed using Blazor Server/.Net 10 and utilises MudBlazor for the GUI Component framework
* There is a unit test app which tests the GameEngine service and uses the XUnit .Net 10 Test Framework
