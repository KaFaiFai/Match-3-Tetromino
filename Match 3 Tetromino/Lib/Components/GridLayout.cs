﻿using Microsoft.Xna.Framework;
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

        /// <summary>
        /// Return offset from the grid center to the center of this cell
        /// </summary>
        public Vector2 At(Point rowCol)
        {
            // (row, col) is mapped to (y, x)
            Vector2 cellPos = CellSize * rowCol.ToVector2() + Vector2.One * CellSize / 2;
            Vector2 offset = cellPos - Center();
            Vector2 swapped = new Vector2(offset.Y, offset.X);
            return swapped;
        }

        public Vector2 Center()
        {
            return RowCol.ToVector2() * CellSize / 2;
        }
    }
}
