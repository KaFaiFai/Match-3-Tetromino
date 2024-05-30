using Match_3_Tetromino.Lib.Components;
using Match_3_Tetromino.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<BlockType> BlockTypes { get; set; }

        public Polyomino(Shape shape, List<BlockType> blockTypes)
        {
            Transform = new Transform();
            GridLayout = new GridLayout() { CellSize = 50, RowCol = shape.GetRowCol() };
            Shape = shape;
            BlockTypes = blockTypes;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Point rowCol = Shape.GetRowCol();
            for (int i = 0; i < rowCol.X; i++)
            {
                for (int j = 0; j < rowCol.Y; j++)
                {
                    int typeIndex = Shape.Matrix[i, j];
                    if (typeIndex != -1)
                    {
                        BlockType type = BlockTypes[typeIndex];
                        Block block = new Block(type);
                        block.Transform = new Transform()
                        {
                            Center = Transform.Center + GridLayout.At(new Point(i, j)),
                            Size = new Vector2(40, 40),
                        };

                        block.Draw(spriteBatch);
                    }
                }
            }
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
