using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLib
{
    internal class WidthFirstMazeFactory : IMazeFactory
    {
        public Maze BuildMaze(MazeSize size)
        {
            Maze maze = new Maze(size);
            List<MazeWall> mazeWalls = new List<MazeWall>();
            MazeCell current = new MazeCell() { X = 0, Y = Utlity.GetRandom(maze.Size - 1, DateTime.Now.Millisecond) };
            maze.StartPoint = current;
            maze[current] = 1;
            mazeWalls.AddRange(GetCellWalls(current));
            int i = 0;
            while (mazeWalls.Any())
            {
                int count = mazeWalls.Count;
                int nextIndex = Utlity.GetRandom(count - 1, count * count + i * (i++)); ;
                //if (!mazeWalls.Exists(mw=>mw.Cell.X != current.X || mw.Cell.Y != current.Y))
                //{
                //    nextIndex = Utlity.GetRandom(count - 1, count * count + i * (i++));
                //}else
                //{
                //    nextIndex = mazeWalls.FindIndex(mw => mw.Cell.X != current.X || mw.Cell.Y != current.Y);
                //}
               
                var currentCell = mazeWalls[nextIndex].Cell;
                var neighborCell = GetNeighborCell(mazeWalls[nextIndex], maze.Size);
                if (neighborCell.HasValue)
                {
                    var cell = neighborCell.Value;
                    if (maze[cell] == 0)
                    {
                        mazeWalls.AddRange(GetCellWalls(cell, mazeWalls[nextIndex].Wall));
                        maze[cell] = 1;
                        maze.AddPassage(currentCell, cell);
                        //mazeWalls.RemoveAll(wall => { return wall.Cell.X == currentCell.X && wall.Cell.Y == currentCell.Y; });
                    }
                    mazeWalls.RemoveAt(nextIndex);
                }
                else
                {
                    mazeWalls.RemoveAt(nextIndex);
                }
            }
            //for (i = 0; i < maze.Size; i++)
            //{
            //    for (int j = 0; j < maze.Size; j++)
            //    {
            //        Console.Write(maze[i, j]);
            //        Console.Write("     ");
            //    }
            //    Console.Write("\r\n");
            //}
            return maze;
        }

        private List<MazeWall> GetCellWalls(MazeCell cell,Wall? baseWall = null)
        {
            List<MazeWall> mazeWalls = new List<MazeWall>();
            if (baseWall == null)
            {
                mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Bottom });
                mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Left });
                mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Right });
                mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Top });
            }
            else
            {
                switch (baseWall)
                {
                    case Wall.Bottom:
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Bottom });
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Left });
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Right });
                        break;
                    case Wall.Left:
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Bottom });
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Left });
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Top });
                        break;
                    case Wall.Right:
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Bottom });
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Right });
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Top });
                        break;
                    case Wall.Top:
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Left });
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Right });
                        mazeWalls.Add(new MazeWall() { Cell = cell, Wall = Wall.Top });
                        break;
                    default: break;
                }
            }
            return mazeWalls;
        }

        private MazeCell? GetNeighborCell(MazeWall wall, int mazeSize)
        {
            MazeCell cell = new MazeCell() { X = -1, Y = -1 };
            switch (wall.Wall)
            {
                case Wall.Bottom:
                    if (wall.Cell.X < mazeSize - 1)
                    {
                        cell.X = wall.Cell.X + 1;
                        cell.Y = wall.Cell.Y;
                    }
                    break;
                case Wall.Left:
                    if (wall.Cell.Y > 0)
                    {
                        cell.Y = wall.Cell.Y - 1;
                        cell.X = wall.Cell.X;
                    }
                    break;
                case Wall.Right:
                    if (wall.Cell.Y < mazeSize - 1)
                    {
                        cell.Y = wall.Cell.Y + 1;
                        cell.X = wall.Cell.X;
                    }
                    break;
                case Wall.Top:
                    if (wall.Cell.X > 0)
                    {
                        cell.X = wall.Cell.X - 1;
                        cell.Y = wall.Cell.Y;
                    }
                    break;
                default: break;
            }
            if (cell.X < 0 || cell.Y < 0)
            {
                return null;
            }

            return cell;
        }
    }
}
