using Match_3_Tetromino.Lib.Components;
using Match_3_Tetromino.Lib.Models;
using Match_3_Tetromino.Lib.Services;
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
                        block.Transform.Center = Transform.Center + GridLayout.At(new Point(i, j));
                        block.Draw(spriteBatch);
                    }
                }
            }

            int numRow = rowCol.X;
            int numCol = rowCol.Y;

            int lineWidth = 1;

            // horizontal lines
            for (int i = 0; i < numRow + 1; i++)
            {
                Vector2 topLeft = (GridLayout.At(new Point(i - 1, -1)) + GridLayout.At(new Point(i, 0))) / 2;
                topLeft.Y -= lineWidth;
                Vector2 bottomRight = (GridLayout.At(new Point(i - 1, numCol - 1)) + GridLayout.At(new Point(i, numCol))) / 2;
                bottomRight.Y += lineWidth;
                Vector2 size = bottomRight - topLeft;
                Rectangle rectangle = new Rectangle((topLeft + Transform.Center).ToPoint(), size.ToPoint());
                spriteBatch.Draw(Contents.Pixel, rectangle, Color.Black);
            }

            // vertical lines
            for (int i = 0; i < numCol + 1; i++)
            {
                Vector2 topLeft = (GridLayout.At(new Point(-1, i - 1)) + GridLayout.At(new Point(0, i))) / 2;
                topLeft.X -= lineWidth;
                Vector2 bottomRight = (GridLayout.At(new Point(numRow - 1, i - 1)) + GridLayout.At(new Point(numRow, i))) / 2;
                bottomRight.X += lineWidth;
                Vector2 size = bottomRight - topLeft;
                Rectangle rectangle = new Rectangle((topLeft + Transform.Center).ToPoint(), size.ToPoint());
                spriteBatch.Draw(Contents.Pixel, rectangle, Color.Black);
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
