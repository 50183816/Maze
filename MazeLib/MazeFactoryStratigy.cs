using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLib
{
   public static class MazeFactoryStratigy
    {
        public static IMazeFactory GetMazeFactory(MazeStratigy stratigy)
        {
            switch(stratigy)
            {
                case MazeStratigy.DepthFirst:
                    return new DepthFirstMazeFactory();
                case MazeStratigy.WidthFirst:
                    return new WidthFirstMazeFactory();
                default:
                    return new DepthFirstMazeFactory();
            }
        }
    }
}
