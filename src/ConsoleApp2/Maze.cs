using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public enum MazeSize
    {
        Small = 8,
        Medium = 16,
        Large = 32
    }

    public struct MazeCell
    {
        public int X;
        public int Y;
    }

    public class Maze
    {
        public int Size { get; set; }

        private int[,] maze;

        public Stack<MazeCell> MazeStack { get; set; }

        public Maze() : this(MazeSize.Small)
        {

        }

        public Maze(MazeSize mazeSize)
        {
            this.Size = (int)mazeSize;
            maze = new int[this.Size, this.Size];
        }

        public void GenerateMaze()
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    maze[i, j] = 0;
                }
            }

            MazeStack = new Stack<MazeCell>();
            MazeCell current = new MazeCell() { X = 0, Y = 0 };
            int step = 1;
            this[current] = step++;
            MazeStack.Push(current);
            while (MazeStack.Count > 0)
            {
                var neighbors = GetNeighbors(current);
                if (neighbors != null && neighbors.Count > 0)
                {
                    MazeStack.Push(current);
                    step = this[current];
                    current = neighbors[GetRandom(neighbors.Count, current.X * current.X + current.Y * current.Y)];
                    this[current] = ++step;
                    MazeStack.Push(current);
                }
                else
                {
                    current = MazeStack.Pop();
                }
            }
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    Console.Write(maze[i, j]);
                    Console.Write(" ");
                }
                Console.Write("\r\n");
            }

        }

        public int this[MazeCell cell]
        {
            get
            {
                if (IsCellInRange(cell))
                {
                    return maze[cell.X, cell.Y];
                }
                throw new IndexOutOfRangeException("Cell is out of maze range!");
            }
            set
            {
                if (IsCellInRange(cell))
                {
                    maze[cell.X, cell.Y] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Cell is out of maze range!");
                }
            }
        }

        private bool IsCellInRange(MazeCell cell)
        {
            if (cell.X >= 0 && cell.X < this.Size && cell.Y >= 0 && cell.Y < this.Size)
            {
                return true;
            }

            return false;
        }

        private int GetRandom(int maxnumber, int seed)
        {
            Random random = new Random(seed);
            return random.Next(maxnumber);
        }

        private List<MazeCell> GetNeighbors(MazeCell point)
        {
            var neighbors = new List<MazeCell>();
            //1. left
            if (point.X > 0)
            {
                var leftCell = new MazeCell() { X = point.X - 1, Y = point.Y };
                if (this[leftCell] == 0)
                {
                    neighbors.Add(leftCell);
                }
            }
            //2. up
            if (point.Y > 0)
            {
                var upCell = new MazeCell() { X = point.X, Y = point.Y - 1 };
                if (this[upCell] == 0)
                {
                    neighbors.Add(upCell);
                }
            }
            //3. right
            if (point.X < Size - 1)
            {
                var rightCell = new MazeCell() { X = point.X + 1, Y = point.Y };
                if (this[rightCell] == 0)
                {
                    neighbors.Add(rightCell);
                }
            }
            //4. down
            if (point.Y < Size - 1)
            {
                var downCell = new MazeCell() { X = point.X, Y = point.Y + 1 };
                if (this[downCell] == 0)
                {
                    neighbors.Add(downCell);
                }
            }
            return neighbors;
        }
    }
}
