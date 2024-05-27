using Match_3_Tetromino.Library.Core;
using Match_3_Tetromino.Library.Models;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Views.Scenes
{
    internal class PolyominoScene : Scene
    {
        static int blockSize = 40;

        public int X { get; set; }
        public int Y { get; set; }
        private Polyomino _polyomino;
        private List<BlockScene> _blockScenes;

        public PolyominoScene(int x, int y, Polyomino polyomino)
        {
            X = x;
            Y = y;
            _polyomino = polyomino;

            _blockScenes = new List<BlockScene>();
            int[,] shape = polyomino.getCurrentShape();
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    int blockIndex = shape[i, j];
                    if (blockIndex != -1)
                    {
                        Block block = polyomino.Blocks[blockIndex];
                        int startX = (int)(X - polyomino.getWidth() * blockSize / 2) + blockSize * j;
                        int startY = (int)(Y - polyomino.getHeight() * blockSize / 2) + blockSize * i;
                        _blockScenes.Add(new BlockScene(startX, startY, block));
                    }
                }
            }
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in _blockScenes)
            {
                block.Draw(spriteBatch);
            }
        }

    }
}
