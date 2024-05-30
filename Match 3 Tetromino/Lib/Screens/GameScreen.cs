using Match_3_Tetromino.Lib.Entities;
using Match_3_Tetromino.Lib.Models;
using Match_3_Tetromino.Lib.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Match_3_Tetromino.Lib.Screens
{
    internal class GameScreen
    {
        private GameState _gameState;
        private Polyomino _curPolyomino;
        private Polyomino _nextPolyomino;
        private MainBoard _mainBoard;

        private Tween<object> _tween;

        public GameScreen()
        {
            _gameState = new GameState(0);
        }

        void Initialize()
        {
            AdvancePolyomino();
            AdvancePolyomino();
        }

        private void AdvancePolyomino()
        {
            Shape shape = Shape.AllShapes[_gameState.Rng.Next(Shape.AllShapes.Count)];
            List<Block> blocks = new List<Block>(shape.CountNonEmpty());
            for (int i = 0; i < blocks.Count; i++)
            {
                Array allBlocks = Enum.GetValues(typeof(Block));
                Block randomBlock = (Block)allBlocks.GetValue(_gameState.Rng.Next(allBlocks.Length));
                blocks.Add(randomBlock);
            }
            Polyomino newPolyomino = new Polyomino(shape, blocks);

            _nextPolyomino.Transform = _curPolyomino.Transform;
            _curPolyomino = _nextPolyomino;
            _nextPolyomino = newPolyomino;
        }
    }
}
