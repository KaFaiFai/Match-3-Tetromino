using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Match_3_Tetromino.Lib.Models
{
    internal class Shape
    {
        public int[,] Matrix { get; private set; }

        private Shape(int[,] matrix)
        {
            Matrix = matrix;
        }

        public void RotateClockwise()
        {
            int rows = Matrix.GetLength(0);
            int cols = Matrix.GetLength(1);
            int[,] rotatedMatrix = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotatedMatrix[j, rows - 1 - i] = Matrix[i, j];
                }
            }
            Matrix = rotatedMatrix;
        }

        public int CountNonEmpty()
        {
            int nonEmptyCount = 0;
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] != -1)
                        nonEmptyCount++;
                }
            }
            return nonEmptyCount;
        }

        public Point GetRowCol()
        {
            int rows = Matrix.GetLength(0);
            int cols = Matrix.GetLength(1);
            return new Point(rows, cols);
        }

        private static readonly Shape _triminoLShapes = new(new int[,] { { 0, 1 }, { -1, 2 } });

        private static readonly Shape _triminoIShapes = new(new int[,] { { 0, 1, 2 } });

        public static readonly List<Shape> AllShapes = new() { _triminoLShapes, _triminoIShapes };
    }
}
