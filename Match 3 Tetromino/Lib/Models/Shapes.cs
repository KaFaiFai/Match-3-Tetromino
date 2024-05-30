using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Match_3_Tetromino.Lib.Models
{
    internal class Shapes
    {
        public static readonly int[,] TriminoLShape = new int[,] { { 0, 1 }, { -1, 2 } };

        public static readonly int[,] TriminoIShape = new int[,] { { 0, 1, 2 } };

        public static readonly List<int[,]> AllShapes = new() { TriminoLShape, TriminoIShape };
    }
}
