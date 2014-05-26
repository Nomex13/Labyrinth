# Labyrinth #

* [Gameplay](#gameplay)
    * [Field](#field)
    * [Player](#player)
    * [Enemies](#enemies)

<h2 id="gameplay">Gameplay</h2>

It's turn-based base defence.

Two ways to play:
1.	1 player against AI. Player should kill the enemy AI using killing machines.
2.	2 players against each own AI on the same map. Each player should defend base from his/her enemy and at the same time make it harder for the opponent to defend.

At the start there is a base in one corner of the map and AI in the other. Walls on the map make a maze.
Enemy walks through the maze to the players's base. When enemy comes to the base player looses. When enemy steps on a cell with a killing machine on it AI dies.
Player wins when all enemies are dead.

<h3 id="field">Field</h3>

Field consists of N x M cells. In each cell can be presented:
* An empty space
* A wall
* Player 1 base
* Player 2 base
* Killing machine that kills enemies that step in it

<h3 id="player">Player</h3>

Player can place walls. Player has 0-N walls at the beginning. After player's enemy has evaporated a wall that wall will appear in player's inventory in 2 turns.

Placing one wall takes 1 turn.

<h3 id="enemies">Enemies</h3>

Enemies are slugs. Enemies can't see killing machines. Enemies can evaporate walls. Each 3 turns enemy decides what path it will follow. Decision takes 1 turn. Enemy prefers path that has less number of walls to evaporate and is shorter.
After path is selected, the enemy follows it blindly for the next 3 steps. If enemy meets a cell in its path that was changed, it will re-decide what path to take.
Enemies can awake not only on the 1st turn, but on 2nd or 3rd or etc.

Evaporation takes 1 turn.
Decision making takes 1 turn.
Moving one cell takes 1 turn.