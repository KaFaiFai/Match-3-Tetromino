using Match_3_Tetromino.Codes.Core;
using Match_3_Tetromino.Codes.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Properties
{
    internal record GridLayout : Property
    {
        public float CellSize { get; set; } = 0;
        public Point RowCol { get; set; } = Point.Zero;
    }
}
