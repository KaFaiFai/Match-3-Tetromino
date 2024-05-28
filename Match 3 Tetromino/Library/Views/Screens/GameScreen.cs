using Match_3_Tetromino.Library.Components;
using Match_3_Tetromino.Library.Core;
using Match_3_Tetromino.Library.Models;
using Match_3_Tetromino.Library.StateManagers;
using Match_3_Tetromino.Library.Views.Animations;
using Match_3_Tetromino.Library.Views.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Match_3_Tetromino.Library.Views.Screens
{
    internal class GameScreen : IScreen
    {
        private GameState _gameState;

        private Animation _animation;
        private GridComponent _gridComponent;
        private BlockScene[,] _blockScenes;
        private PolyominoScene _curPolyominoScene;
        private PolyominoScene _nextPolyominoScene;


        public GameScreen()
        {
            _gameState = new GameState(seed: 1);
            RowCol size = _gameState.Board.Size;
            _gridComponent = new GridComponent(size.Row, size.Col, 50);
            _blockScenes = new BlockScene[size.Row, size.Col];
            _curPolyominoScene = new PolyominoScene(100, 100, _gameState.CurPolyomino);
            _nextPolyominoScene = new PolyominoScene(100, 400, _gameState.NextPolyomino);
            UpdateScenes();
        }

        void IScreen.Update(GameTime gameTime)
        {
            _animation?.Update(gameTime);
            if (Input.EnterState == InputState.justPressed)
            {
                List<(RowCol, Block)> willDropTo = _gameState.Board.WillDropTo(_gameState.CurPolyomino, 0);
                List<BlockScene> startFrom = new List<BlockScene> { };
                List<BlockScene> dropTo = new List<BlockScene> { };
                int lowestRow = willDropTo.Max(e => e.Item1.Row);
                foreach (var (rowCol, block) in willDropTo)
                {
                    Point center = new Point(1280 / 2, 720 / 2);
                    Point startPoint = _gridComponent.GetCellLocationAt(rowCol.Row - lowestRow, rowCol.Col) + center;
                    Point endPoint = _gridComponent.GetCellLocationAt(rowCol.Row, rowCol.Col) + center;
                    startFrom.Add(new BlockScene(startPoint.X, startPoint.Y, block));
                    dropTo.Add(new BlockScene(endPoint.X, endPoint.Y, block));
                }
                _animation = new BlocksTween(TimeSpan.FromMilliseconds(1000), startFrom, dropTo);
                _animation.Completed += () =>
                {
                    _gameState.Board.PlaceBlocks(willDropTo);
                    _gameState.AdvancePolyomino();
                    _animation = null;
                    UpdateScenes();
                    SettleMove();
                };
            };
        }

        private void SettleMove()
        {
            List<(RowCol, Block)> match3Blocks = _gameState.Board.FindMatch3();
            Debug.Print(String.Format("SettleMove {0}", match3Blocks.Count));
            if (match3Blocks.Count > 0)
            {
                List<BlockScene> from = new List<BlockScene> { };
                List<BlockScene> to = new List<BlockScene> { };
                foreach (var (rowCol, block) in match3Blocks)
                {
                    Point center = new Point(1280 / 2, 720 / 2);
                    Point blockCenter = _gridComponent.GetCellLocationAt(rowCol.Row, rowCol.Col) + center;
                    from.Add(new BlockScene(blockCenter.X, blockCenter.Y, block));
                    to.Add(new BlockScene(blockCenter.X, blockCenter.Y, block));
                    to.Last().Transform.Update(scale: Vector2.Zero);
                }
                _animation = new BlocksTween(TimeSpan.FromMilliseconds(1000), from, to);
                _animation.Started += () =>
                {
                    _gameState.Board.RemoveBlocks(match3Blocks.Select((e, i) => e.Item1).ToList());
                    UpdateScenes();
                };
                _animation.Completed += () =>
                {
                    _animation = null;
                    UpdateScenes();
                };
            }
        }

        private void UpdateScenes()
        {
            Point center = new Point(1280 / 2, 720 / 2);
            for (int i = 0; i < _gameState.Board.Size.Row; i++)
            {
                for (int j = 0; j < _gameState.Board.Size.Col; j++)
                {
                    Block? block = _gameState.Board.Data[i, j];
                    if (block == null)
                    {
                        _blockScenes[i, j] = null;
                    }
                    else
                    {
                        Point blockCenter = center + _gridComponent.GetCellLocationAt(i, j);
                        _blockScenes[i, j] = new BlockScene(blockCenter.X, blockCenter.Y, (Block)block);
                    }
                }
            }
            _curPolyominoScene = new PolyominoScene(100, 100, _gameState.CurPolyomino);
            _nextPolyominoScene = new PolyominoScene(100, 400, _gameState.NextPolyomino);
        }


        void IScreen.Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            DrawGrid(spriteBatch);
            _curPolyominoScene.Draw(spriteBatch);
            _nextPolyominoScene.Draw(spriteBatch);
            _animation?.Draw(spriteBatch);

            spriteBatch.End();
        }

        private void DrawGrid(SpriteBatch spriteBatch)
        {
            int cellSize = 50;
            int width = 2;
            Point center = new Point(1280 / 2, 720 / 2);

            Board board = _gameState.Board;
            // Draw horizontal lines
            for (int i = 0; i < board.Size.Row + 1; i++)
            {
                int lineWidth = board.Size.Col * cellSize;
                Point start = _gridComponent.GetCellLocationAt(i - 0.5, -0.5);
                start.Y -= width / 2;
                Point size = new Point(lineWidth, width);
                spriteBatch.Draw(Contents.Pixel, new Rectangle(start + center, size), Color.Black);
            }
            // Draw vertical lines
            for (int i = 0; i < board.Size.Col + 1; i++)
            {
                int lineHeight = board.Size.Row * cellSize;
                Point start = _gridComponent.GetCellLocationAt(-0.5, i - 0.5);
                start.X -= width / 2;
                Point size = new Point(width, lineHeight);
                spriteBatch.Draw(Contents.Pixel, new Rectangle(start + center, size), Color.Black);
            }

            // Draw blocks in the grid
            for (int i = 0; i < _blockScenes.GetLength(0); i++)
            {
                for (int j = 0; j < _blockScenes.GetLength(1); j++)
                {
                    _blockScenes[i, j]?.Draw(spriteBatch);
                }
            }
        }

    }
}
