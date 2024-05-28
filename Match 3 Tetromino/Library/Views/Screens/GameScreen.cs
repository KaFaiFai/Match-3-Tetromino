using Match_3_Tetromino.Components;
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
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Match_3_Tetromino.Library.Views.Screens
{
    internal class GameScreen : IScreen
    {
        private GameState _gameState;
        private SpriteBatch _spriteBatch;

        private BlocksDrop? _blocksDrop;
        private GridComponent _gridComponent;
        private BlockScene?[,] _blockScenes;
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
            //UpdateScenes();
        }

        void IScreen.LoadContent(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        void IScreen.Update(GameTime gameTime)
        {
            _blocksDrop?.Update(gameTime);
            if (Input.EnterState == InputState.justPressed)
            {

                List<(RowCol, Block)> willDropTo = _gameState.Board.WillDropTo(_gameState.CurPolyomino, 0);
                List<BlockScene> startFrom = new List<BlockScene> { };
                List<BlockScene> dropTo = new List<BlockScene> { };
                foreach (var (rowCol, block) in willDropTo)
                {
                    startFrom.Add(new BlockScene(rowCol.Col * 10, rowCol.Row * 10, block));
                    dropTo.Add(new BlockScene(rowCol.Col * 20, rowCol.Row * 20, block));
                }
                _blocksDrop = new BlocksDrop(new TimeSpan(0, 0, 1), startFrom, dropTo);
                _blocksDrop.Started += () =>
                {
                    Debug.Print("Event received!");
                };
                _blocksDrop.Completed += () =>
                {
                    _gameState.Board.PlaceBlocks(willDropTo);
                    _gameState.AdvancePolyomino();
                    _blocksDrop = null;
                    UpdateScenes();
                };
            };
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


        void IScreen.Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawGrid();
            _curPolyominoScene.Draw(_spriteBatch);
            _nextPolyominoScene.Draw(_spriteBatch);
            _blocksDrop?.Draw(_spriteBatch);

            _spriteBatch.End();
        }

        private void DrawGrid()
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
                _spriteBatch.Draw(Contents.Pixel, new Rectangle(start + center, size), Color.Black);
            }
            // Draw vertical lines
            for (int i = 0; i < board.Size.Col + 1; i++)
            {
                int lineHeight = board.Size.Row * cellSize;
                Point start = _gridComponent.GetCellLocationAt(-0.5, i - 0.5);
                start.X -= width / 2;
                Point size = new Point(width, lineHeight);
                _spriteBatch.Draw(Contents.Pixel, new Rectangle(start + center, size), Color.Black);
            }

            // Draw blocks in the grid
            for (int i = 0; i < _blockScenes.GetLength(0); i++)
            {
                for (int j = 0; j < _blockScenes.GetLength(1); j++)
                {
                    _blockScenes[i, j]?.Draw(_spriteBatch);
                }
            }
        }

    }
}
