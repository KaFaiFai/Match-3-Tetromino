using Match_3_Tetromino.Lib.Components;
using Match_3_Tetromino.Lib.Models;
using Match_3_Tetromino.Lib.Services;
using Match_3_Tetromino.Library.Models;
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
        public int[,] Shape { get; set; }
        public List<BlockType> BlockTypes { get; set; }

        public Polyomino(int[,] shape, List<BlockType> blockTypes)
        {
            Transform = new Transform();
            int numRow = shape.GetLength(0);
            int numCol = shape.GetLength(1);
            GridLayout = new GridLayout() { CellSize = 50, RowCol = new Point(numRow, numCol) };
            Shape = shape;
            BlockTypes = blockTypes;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int numRow = Shape.GetLength(0);
            int numCol = Shape.GetLength(1);
            for (int i = 0; i < numRow; i++)
            {
                for (int j = 0; j < numCol; j++)
                {
                    int typeIndex = Shape[i, j];
                    if (typeIndex != -1)
                    {
                        BlockType type = BlockTypes[typeIndex];
                        Block block = new Block(type);
                        block.Transform.Center = Transform.Center + GridLayout.At(new Point(i, j));
                        block.Draw(spriteBatch);
                    }
                }
            }

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

        private void RotateClockwise()
        {
            int rows = Shape.GetLength(0);
            int cols = Shape.GetLength(1);
            int[,] rotatedMatrix = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotatedMatrix[j, rows - 1 - i] = Shape[i, j];
                }
            }
            Shape = rotatedMatrix;
        }

        public void Rotate(bool clockwise)
        {
            if (clockwise)
            {
                RotateClockwise();
            }
            else
            {
                // Rotate 3 times clockwise for anti-clockwise
                Enumerable.Range(0, 3).ToList().ForEach(_ => RotateClockwise());
            }
            int numRow = Shape.GetLength(0);
            int numCol = Shape.GetLength(1);
            GridLayout.RowCol = new Point(numRow, numCol);
        }
    }
}
