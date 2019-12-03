# AStar-Pathfinding
A small demo project using a custom A* Pathfinding library.

Written in C# in the Unity Engine by Kevin O'Neil.

# A* Pathfinding
AstarPathfinding.cs contains the code to do the actual pathfinding, as well as some common functions.  The FindPath method takes in an implementation of the custom class IGrid that allows the pathfinding code to be general, leaving different node map implementations up to the interface.

# IGrid
An interface that contains all the pathfinding methods that can change based on what type of navigational map is implemented. This includes things such as:
* Calculating H cost
* Distance between nodes
* Determining neighbor nodes of the current evaluated node
* Clearing / resetting navigation values
* Constructing the navigation model

Demo project includes 3 implementations of the IGrid:
* 4 Directional / Manhatten pathfinding
* 8 Directional / Diagonal pathfinding
* Node and edge based pathfinding.

# Future Plans
Implement a hexagonal grid model.
