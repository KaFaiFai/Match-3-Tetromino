using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Lib.Entities
{
    internal class GameState
    {
        public Random Rng { get; private set; }
        public int LeftIndex { get; set; }
        public bool IsInputEnabled { get; set; }

        public GameState(int seed)
        {
            Rng = new Random(seed);
            LeftIndex = 0;
            IsInputEnabled = false;
        }
    }
}
