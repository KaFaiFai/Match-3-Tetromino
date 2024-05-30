using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Lib.Components
{
    internal record GridLayout
    {
        public float CellSize { get; set; } = 0;
        public Point RowCol { get; set; } = Point.Zero;
    }
}
