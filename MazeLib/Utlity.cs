using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLib
{
    public class Utlity
    {
        public static int GetRandom(int maxnumber, int seed)
        {
            Random random = new Random(seed + DateTime.Now.Millisecond);
            return random.Next(maxnumber);
        }
    }
}
