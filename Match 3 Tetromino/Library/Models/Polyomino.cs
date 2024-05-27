using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Models
{
    internal class Polyomino
    {
        private Shape _shape;
        private int _rotationIndex;
        private List<Block> _blocks;

        public List<Block> Blocks { get { return _blocks; } }

        public Polyomino(Shape shape, int rotationIndex, List<Block> blocks)
        {
            _shape = shape;
            _rotationIndex = rotationIndex;
            _blocks = blocks;
        }

        public int[,] getCurrentShape()
        {
            return _shape.rotated(_rotationIndex);
        }

        public int getWidth()
        {
            return getCurrentShape().GetLength(1);
        }

        public int getHeight()
        {
            return getCurrentShape().GetLength(0);
        }

        public void rotate(int clockwise = 1)
        {
            _rotationIndex += clockwise;
        }

        public static Polyomino random(Random rng)
        {
            int shapeIndex = rng.Next(Shape.allShapes.Count);
            Shape shape = Shape.allShapes[shapeIndex];
            int rotationIndex = rng.Next(shape.NumShapes);
            int numBlocks = shape.NumBlocks;

            List<Block> blocks = new List<Block>(numBlocks);
            for (int i = 0; i < numBlocks; i++)
            {
                Array allBlocks = Enum.GetValues(typeof(Block));
                Block randomBlock = (Block)allBlocks.GetValue(rng.Next(allBlocks.Length));
                blocks.Add(randomBlock);
            }

            return new Polyomino(shape, rotationIndex, blocks);
        }
    }
}
