# Labyrinth #

* [Gameplay](#gameplay)
    * [Field](#field)
    * [Player](#player)
    * [Enemies](#enemies)

<h2 id="scenarop">Scenario</h2>

Племя негров.
Танцуют вокруг жертвенника, на жертвеннике лежит связанный индеец - ГГ. На его лице ужас.
После танца негры хватают ГГ и тащат к пещере.
Внутри пещеры темная комната. Вначале комнаты стоит столб, к которому привязывают ГГ. Человеческое жертвоприношение это круто.
Племя уходит и закрывает за собой дверь. На другом конце карты есть другая дверь.
Включается свет, перед игроком чистая белая светлая комната-лабиринт.

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
Untieing the hero takes 1 turn.

<h4 id="hero">Hero</h4>

Hero is an Indian man.
He is tied to a stake at the very beginning.
Anytime he sees an enemy he screams like a bitch.

<h4 id="stressbar">Stressbar</h4>

Hero has a stress bar which has values [0, 1, 2, 3, 4, 5].
If at the beginning and at the end of player's turn the hero doesn't see any enemy then stressbar is decreased by 1.
If at the beginning and at the end of player's turn the hero sees same enemy then stressbar is increased by 1 for each such enemy.
Otherwise, it is stress bar doesn't change.

At the beginning stressbar is maximum - 5.

<h3 id="enemies">Enemies</h3>

Enemies are slugs with an eye on the top. Totaly alien creatures, maybe created in hidden genetic laboratories.
Enemies can see walls and the player. Enemies can't see killing machines.
Enemies can evaporate walls.
At start enemy decides what path it will follow. That decision takes 1 turn. Prefered path:
1. has less number of walls to evaporate,
2. is shorter.
After path is selected, the enemy follows it blindly until an unexpected wall or player himself is met or destination cell is reached. If enemy meets a wall that it didn't expect, it will re-decide what path to take.
Enemies can awake not only on the 1st turn, but on 2nd or 3rd or etc.

Evaporation takes 1 turn.
Decision making takes 1 turn.
Moving one cell takes 1 turn.


Стрессбар
При стрессе 1-4 ГГ бежит в сторону от слизня или того места, где в последний раз он его видел.
При стрессе равным 0 ГГ движется к выходу. Если пути к выходу нет, то стоит на месте.
При стрессе 1 ГГ бежит от слизня если его видит или движется к выходу если не видит. //стоит на месте если не видит.
При стрессе 2 ГГ бежит от слизня если его видит или движется к выходу если не видит. //стоит на месте если не видит.
При стрессе 3 ГГ бежит от слизня или места где последний раз его видел.
При стрессе 4 ГГ бежит от слизня или места где последний раз его видел.
При стрессе 5 ГГ лежит и орет.
Если ГГ бежит от слизня/места где его видел и находится на развилке, то будет выбрана та клетка, которая дальше от слизня/места.
Если расстояния равны, то та, что ближе к выходу (дальность по прямой). Если и эти расстояния равны, то та, что правее.