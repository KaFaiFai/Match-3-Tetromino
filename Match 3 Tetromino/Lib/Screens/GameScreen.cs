using Match_3_Tetromino.Lib.Entities;
using Match_3_Tetromino.Lib.Models;
using Match_3_Tetromino.Lib.Util;
using Match_3_Tetromino.Lib.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private InputTracker _inputTracker;

        public void Initialize()
        {
            _gameState = new GameState(0);
            AdvancePolyomino();
            AdvancePolyomino();
            _mainBoard = new MainBoard(new Point(10, 6));
            _mainBoard.Transform.Center = new Vector2(1280, 720) / 2;
            _mainBoard.BlockTypes[0, 0] = BlockType.a;
            _mainBoard.BlockTypes[9, 0] = BlockType.b;
            _mainBoard.BlockTypes[0, 5] = BlockType.c;
            _mainBoard.BlockTypes[9, 5] = BlockType.d;

            _mainBoard.BoardResolved += () =>
            {
                _gameState.IsInputEnabled = true;
                AdvancePolyomino();
            };

            _inputTracker = new InputTracker();
            _inputTracker.EnterPressed += () =>
            {
                _gameState.IsInputEnabled = false;
                _mainBoard.PlacePolyomino(_curPolyomino, _gameState.LeftIndex);
            };
            _inputTracker.MoveLeftPressed += () =>
            {
                _gameState.LeftIndex--;
            };
            _inputTracker.MoveRightPressed += () =>
            {
                _gameState.LeftIndex++;
            };
            _inputTracker.RotateClockwisePressed += () =>
            {
                _curPolyomino.Rotate(clockwise: true);
                _nextPolyomino.Rotate(clockwise: true);
            };
            _inputTracker.RotateAntiClockwisePressed += () =>
            {
                _curPolyomino.Rotate(clockwise: false);
                _nextPolyomino.Rotate(clockwise: false);
            };
        }

        public void Update(GameTime gameTime)
        {
            _mainBoard.Update(gameTime);
            _inputTracker.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _curPolyomino.Draw(spriteBatch);
            _nextPolyomino.Draw(spriteBatch);
            _mainBoard.Draw(spriteBatch);
            spriteBatch.End();
        }

        private void AdvancePolyomino()
        {
            int[,] shape = Shapes.AllShapes[_gameState.Rng.Next(Shapes.AllShapes.Count)];
            List<BlockType> blocks = new List<BlockType> { };

            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] != -1)
                    {
                        Array allBlocks = Enum.GetValues(typeof(BlockType));
                        BlockType randomBlock = (BlockType)allBlocks.GetValue(_gameState.Rng.Next(allBlocks.Length));
                        blocks.Add(randomBlock);
                    }
                }
            }

            Polyomino newPolyomino = new Polyomino(shape, blocks);
            _curPolyomino = _nextPolyomino;
            _nextPolyomino = newPolyomino;

            if (_curPolyomino != null) _curPolyomino.Transform.Center = new Vector2(200, 200);
            if (_nextPolyomino != null) _nextPolyomino.Transform.Center = new Vector2(200, 500);
        }
    }
}
