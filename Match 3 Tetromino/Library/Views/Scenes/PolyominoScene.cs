using Match_3_Tetromino.Components;
using Match_3_Tetromino.Library.Core;
using Match_3_Tetromino.Library.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Views.Scenes
{
    internal class PolyominoScene : Scene
    {
        static int BlockSize = 40;

        private List<BlockScene> _blockScenes;
        private TransformComponent _transformComponent;
        private GridComponent _gridComponent;

        public PolyominoScene(int x, int y, Polyomino polyomino)
        {
            _blockScenes = new List<BlockScene>(polyomino.Blocks.Count);
            _transformComponent = new TransformComponent(x, y);
            int[,] shape = polyomino.getCurrentShape();
            _gridComponent = new GridComponent(shape.GetLength(0), shape.GetLength(1), BlockSize);

            UpdateBlockScene(polyomino);
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in _blockScenes)
            {
                block.Draw(spriteBatch);
            }
        }


        private void UpdateBlockScene(Polyomino polyomino)
        {
            int[,] shape = polyomino.getCurrentShape();
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    int blockIndex = shape[i, j];
                    if (blockIndex != -1)
                    {
                        Block block = polyomino.Blocks[blockIndex];
                        Point cellCenter = _gridComponent.GetCellLocationAt(i, j);
                        Point coord = _transformComponent.GetCenter() + cellCenter;
                        _blockScenes[blockIndex] = new BlockScene(coord.X, coord.Y, block);
                    }
                }
            }
        }
    }
}
