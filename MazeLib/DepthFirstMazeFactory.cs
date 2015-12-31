using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLib
{
    internal class DepthFirstMazeFactory : IMazeFactory
    {
        public Maze BuildMaze(MazeSize size)
        {
            Maze maze = new Maze(size);

            var mazeStack = new Stack<MazeCell>();
            MazeCell current = new MazeCell() { X = 0, Y = Utlity.GetRandom(maze.Size - 1, DateTime.Now.Millisecond) };
            maze.StartPoint = current;
            int step = 1;
            maze[current] = step++;
            mazeStack.Push(current);
            while (mazeStack.Count > 0)
            {
                var neighbors = maze.GetNeighbors(current);
                if (neighbors != null && neighbors.Count > 0)
                {
                    mazeStack.Push(current);
                    step = maze[current];
                    var start = current;
                    current = neighbors[Utlity.GetRandom(neighbors.Count, current.X * current.X + current.Y * current.Y)];
                    maze[current] = 1;
                    mazeStack.Push(current);
                    maze.AddPassage(start, current);
                }
                else
                {
                    current = mazeStack.Pop();
                }
            }

            //for (int i = 0; i < maze.Size; i++)
            //{
            //    for (int j = 0; j < maze.Size; j++)
            //    {
            //        Console.Write(maze[i, j]);
            //        Console.Write(" ");
            //    }
            //    Console.Write("\r\n");
            //}

            return maze;
        }
    }
}
