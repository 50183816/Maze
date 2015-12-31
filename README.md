# Maze
C# version maze generator. And a WPF client for showing the maze on screen.
This solution is developed in VS 2015.
It have two projects:
1. MazeLib， the library which implements the maze generating algorithms. There are currently two, one is depth first algorithm and another is width first (Prim) algorithm.
The class Maze represents the maze object. It has propertiese like size, start point , passages etc. And a factory pattern is used to create maze using different algorithm.
2. MazeApp, the wpf client which get call the factory to generate a maze and show it on UI.

Currently it support generating maze with size 8X8,16X16,32X32 and 64X64, but is can be extended if changing the MazeSize enum from the MazeLib project.

After building and runing the application, from the UI by click the maze size buttons the maze will be generated and shown.

Known Issues:
1. the performance for displaying maze in a WPF application is not good.
2. to be found.
