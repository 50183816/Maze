using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLib
{
   public interface IMazeFactory
    {
        Maze BuildMaze(MazeSize size);
    }
}
