using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Defines the index of blocks in each shapes and their rotations
 * -1 denotes empty block
 */

namespace Match_3_Tetromino.Library.Models
{
    internal class Shape
    {
        private readonly int _NumBlocks;
        private readonly List<int[,]> _Shapes;

        public int NumBlocks { get { return _NumBlocks; } }
        public int NumShapes { get { return _Shapes.Count; } }

        private Shape(int numBlocks, List<int[,]> shapes)
        {
            _NumBlocks = numBlocks;
            _Shapes = shapes;
        }

        public int[,] rotated(int rotationIndex)
        {
            return _Shapes[rotationIndex % NumShapes];
        }

        private static readonly Shape _triminoLShapes = new(
            3,
            new List<int[,]> {
                new int[,] {{ 0, 1 },{ -1, 2 }},
                new int[,] {{ -1, 0 },{ 2, 1 }},
                new int[,] {{ 2, -1 },{ 1, 0 }},
                new int[,] {{ 1, 2 },{ 0, -1 }},
            }
        );

        private static readonly Shape _triminoIShapes = new(
            3,
            new List<int[,]> {
                new int[,] {{ 0, 1, 2 }},
                new int[,] {{ 0 },{ 1 },{ 2 }},
                new int[,] {{ 2, 1, 0 }},
                new int[,] {{ 2 },{ 1 },{ 0 }},
            }
        );

        public static readonly List<Shape> allShapes = new() { _triminoLShapes, _triminoIShapes };
    }
}