using Match_3_Tetromino.Lib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Match_3_Tetromino.Lib.Models;

namespace Match_3_Tetromino.Lib.Entities
{
    internal class MainBoard
    {
        public Transform Transform { get; set; }
        public GridLayout GridLayout { get; set; }
        public Block?[,] Blocks { get; set; }

        public MainBoard(Point rowCol)
        {
            Transform = new Transform();
            GridLayout = new GridLayout() { CellSize = 50, RowCol = rowCol };
            Blocks = new Block?[rowCol.X, rowCol.Y];
        }
    }
}
