using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeLib
{
    public enum MazeSize
    {
        Small = 8,
        Medium = 16,
        Large = 32,
        ExtraLarge = 64

    }

    public enum Wall
    {
        Top,
        Right,
        Bottom,
        Left
    }

    public enum MazeStratigy
    {
        DepthFirst,
        WidthFirst
    }

    public struct MazeCell
    {
        public int X;
        public int Y;
        public static bool operator ==(MazeCell a, MazeCell b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(MazeCell a, MazeCell b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is MazeCell && this == (MazeCell)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public struct MazeWall
    {
        public MazeCell Cell;
        public Wall Wall;
    }

    //Maze Passage
    public struct MazePassage
    {
        public MazeCell StartCell;
        public MazeCell EndCell;

    }

    /// <summary>
    /// 
    /// </summary>
    public class Maze
    {
        public int Size { get; set; }

        private int[,] maze;

        public MazeCell StartPoint { get; set; }

        public List<MazePassage> Passages { get; set; }

        internal Maze() : this(MazeSize.Small)
        {

        }

        internal Maze(MazeSize mazeSize)
        {
            this.Size = (int)mazeSize;
            maze = new int[this.Size, this.Size];
            Passages = new List<MazePassage>();
        }



        public int this[MazeCell cell]
        {
            get
            {
                if (IsCellInRange(cell.X, cell.Y))
                {
                    return maze[cell.X, cell.Y];
                }
                throw new IndexOutOfRangeException("Cell is out of maze range!");
            }
            set
            {
                if (IsCellInRange(cell.X, cell.Y))
                {
                    maze[cell.X, cell.Y] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Cell is out of maze range!");
                }
            }
        }

        public int this[int x, int y]
        {
            get
            {
                if (IsCellInRange(x, y))
                {
                    return maze[x, y];
                }
                throw new IndexOutOfRangeException("Cell is out of maze range!");
            }
            set
            {
                if (IsCellInRange(x, y))
                {
                    maze[x, y] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Cell is out of maze range!");
                }
            }
        }

        private bool IsCellInRange(int x, int y)
        {
            if (x >= 0 && x < this.Size && y >= 0 && y < this.Size)
            {
                return true;
            }

            return false;
        }



        public List<MazeCell> GetNeighbors(MazeCell point)
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

        public List<MazeCell> GetPassedNeighbors(MazeCell point)
        {
            var neighbors = new List<MazeCell>();
            ////1. left
            //if (point.X > 0)
            //{
            //    var leftCell = new MazeCell() { X = point.X - 1, Y = point.Y };
            //    if (this[leftCell] == this[point] + 1)
            //    {
            //        neighbors.Add(leftCell);
            //    }
            //}
            ////2. up
            //if (point.Y > 0)
            //{
            //    var upCell = new MazeCell() { X = point.X, Y = point.Y - 1 };
            //    if (this[upCell] == this[point] + 1)
            //    {
            //        neighbors.Add(upCell);
            //    }
            //}
            ////3. right
            //if (point.X < Size - 1)
            //{
            //    var rightCell = new MazeCell() { X = point.X + 1, Y = point.Y };
            //    if (this[rightCell] == this[point] + 1)
            //    {
            //        neighbors.Add(rightCell);
            //    }
            //}
            ////4. down
            //if (point.Y < Size - 1)
            //{
            //    var downCell = new MazeCell() { X = point.X, Y = point.Y + 1 };
            //    if (this[downCell] == this[point] + 1)
            //    {
            //        neighbors.Add(downCell);
            //    }
            //}
            neighbors = Passages.Where(p => { return p.StartCell == point; }).Select(p => p.EndCell).ToList();
            return neighbors;
        }

        public void AddPassage(MazeCell start, MazeCell end)
        {
            //ToDo: validate if they are really passage ends. 
            Passages.Add(new MazePassage() { EndCell = end, StartCell = start });
        }
    }
}
