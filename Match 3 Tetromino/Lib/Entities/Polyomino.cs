using Match_3_Tetromino.Lib.Components;
using Match_3_Tetromino.Lib.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Lib.Entities
{
    internal class Polyomino
    {
        public Transform Transform { get; set; }
        public GridLayout GridLayout { get; set; }
        public Shape Shape { get; set; }
        public List<Block> Blocks { get; set; }

        public Polyomino(Shape shape, List<Block> blocks)
        {
            Transform = new Transform();
            GridLayout = new GridLayout() { CellSize = 50, RowCol = shape.GetRowCol() };
            Shape = shape;
            Blocks = blocks;
        }

        public void Rotate(bool clockwise)
        {
            if (clockwise)
            {
                Shape.RotateClockwise();
            }
            else
            {
                // Rotate 3 times clockwise for anti-clockwise
                Enumerable.Range(0, 3).ToList().ForEach(_ => Shape.RotateClockwise());
            }
            GridLayout.RowCol = Shape.GetRowCol();
        }
    }
}
