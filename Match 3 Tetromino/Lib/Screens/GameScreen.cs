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
            _curPolyomino.Transform.Center = new Vector2(200, 200);
            _nextPolyomino.Transform.Center = new Vector2(200, 500);
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
                _curPolyomino = null;
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
            };
            _inputTracker.RotateAntiClockwisePressed += () =>
            {
                _curPolyomino.Rotate(clockwise: false);
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
            Shape shape = Shape.AllShapes[_gameState.Rng.Next(Shape.AllShapes.Count)];
            List<BlockType> blocks = new List<BlockType> { };
            for (int i = 0; i < shape.CountNonEmpty(); i++)
            {
                Array allBlocks = Enum.GetValues(typeof(BlockType));
                BlockType randomBlock = (BlockType)allBlocks.GetValue(_gameState.Rng.Next(allBlocks.Length));
                blocks.Add(randomBlock);
            }
            Polyomino newPolyomino = new Polyomino(shape, blocks);

            if (_curPolyomino != null)
            {
                _nextPolyomino.Transform = _curPolyomino.Transform;
            }
            _curPolyomino = _nextPolyomino;
            _nextPolyomino = newPolyomino;
        }
    }
}
