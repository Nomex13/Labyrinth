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

Сценарий
Племя негров.
Танцуют вокруг жертвенника, на жертвеннике лежит связанный индеец - ГГ. На его лице ужас.
После танца негры хватают ГГ и тащат к пещере.
Внутри пещеры темная комната. Вначале комнаты стоит столб, к которому привязывают ГГ. Человеческое жертвоприношение это круто.
Племя уходит и закрывает за собой дверь. На другом конце карты есть другая дверь.
Включается свет, перед игроком чистая белая светлая комната-лабиринт.

Игрок играет за оператора лабиринта. Слизни - инопланетные существа, вырвавшиеся из лаборатории.
Игроку надо довести ГГ до хз через множество комнат-лабиринтов.

В качестве хода игрок может:
Поставить 1 стену, стена появится в конце текущего хода игрока.
Поставить 2 стены, стены появятся в конце следующего хода игрока.
Поставить 3 стены, стены появятся в конце послеследующего хода игрока.
Поставить N стен,  стены появятся в конце (N-1)го хода игрока.
Отвязать ГГ.
Ничего не сделать.

Слизень
Первым ходом слизень смотрит, где стоит ГГ и строит к нему свой путь.
Далее он слепо идет по построенному пути.
Если в какой-то момент встает на клетку ГГ - гамовер.
Если в какой-то момент он упирается в стену, которой не было, или наоборот - стена перед ним исчезла, то он заново тратит 1 ход для постройки пути до ГГ.
Если слизень дошел до конца пути, то он заново тратит 1 ход для постройки пути до ГГ.

Стрессбар
ГГ при виде слизня начинает орать и визжать.
У ГГ имеется стрессбар. Значения стрессбара [0, 5].
Если в начале и в конце хода слизня не видно, то в конце хода стрессбар уменьшается на 1.
Если в начале и в конце хода слизня видно,    то в конце хода стрессбар увеличивается на 1.
Иначе стрессбар не меняет значениея.
Вначале стрессбар максимальный.
При стрессе 1-4 ГГ бежит в сторону от слизня или того места, где в последний раз он его видел.
При стрессе равным 0 ГГ движется к выходу. Если пути к выходу нет, то стоит на месте.
При стрессе 1 ГГ бежит от слизня если его видит или движется к выходу если не видит. //стоит на месте если не видит.
При стрессе 2 ГГ бежит от слизня если его видит или движется к выходу если не видит. //стоит на месте если не видит.
При стрессе 3 ГГ бежит от слизня или места где последний раз его видел.
При стрессе 4 ГГ бежит от слизня или места где последний раз его видел.
При стрессе 5 ГГ лежит и орет.
Если ГГ бежит от слизня/места где его видел и находится на развилке, то будет выбрана та клетка, которая дальше от слизня/места.
Если расстояния равны, то та, что ближе к выходу (дальность по прямой). Если и эти расстояния равны, то та, что правее.