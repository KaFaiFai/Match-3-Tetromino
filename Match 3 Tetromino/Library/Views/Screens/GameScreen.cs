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
        private Texture2D _rectangle;

        private BlocksDrop? _blocksDrop;

        public GameScreen()
        {
            _gameState = new GameState(seed: 1);
        }

        void IScreen.LoadContent(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            // ref: https://stackoverflow.com/questions/5751732/draw-rectangle-in-xna-using-spritebatch
            _rectangle = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _rectangle.SetData(new[] { Color.White });
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
                    Console.WriteLine("Event received!");
                };
                _blocksDrop.Completed += () =>
                {
                    _gameState.Board.PlaceBlocks(willDropTo);
                    _gameState.AdvancePolyomino();
                    _blocksDrop = null;
                };
            };
        }


        void IScreen.Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawGrid();
            DrawPolyomino();
            _blocksDrop?.Draw(_spriteBatch);

            _spriteBatch.End();
        }

        private void DrawGrid()
        {
            int cellSize = 50;
            int width = 2;
            Vector2 center = new Vector2(1280 / 2, 720 / 2);

            Board board = _gameState.Board;
            // Draw horizontal lines
            for (int i = 0; i < board.Size.Row + 1; i++)
            {
                int lineWidth = board.Size.Col * cellSize;
                int startX = (int)(center.X - board.Size.Col / 2 * cellSize);
                int startY = (int)(center.Y - board.Size.Row / 2 * cellSize) + cellSize * i;
                _spriteBatch.Draw(_rectangle, new Rectangle(startX, startY, lineWidth, width), Color.Black);
            }
            // Draw vertical lines
            for (int i = 0; i < board.Size.Col + 1; i++)
            {
                int lineHeight = board.Size.Row * cellSize;
                int startX = (int)(center.X - board.Size.Col / 2 * cellSize) + cellSize * i;
                int startY = (int)(center.Y - board.Size.Row / 2 * cellSize);
                _spriteBatch.Draw(_rectangle, new Rectangle(startX, startY, width, lineHeight), Color.Black);
            }
            // Draw blocks in the grid
            for (int i = 0; i < board.Size.Row; i++)
            {
                for (int j = 0; j < board.Size.Col; j++)
                {
                    Block? block = board.Data[i, j];
                    if (block != null)
                    {
                        Color color = block switch
                        {
                            Block.a => Color.Blue,
                            Block.b => Color.Purple,
                            Block.c => Color.Yellow,
                            Block.d => Color.Green,
                            _ => Color.Red,
                        };
                        int startX = (int)(center.X - board.Size.Col / 2 * cellSize) + cellSize * j;
                        int startY = (int)(center.Y - board.Size.Row / 2 * cellSize) + cellSize * i;
                        _spriteBatch.Draw(_rectangle, new Rectangle(startX, startY, 30, 30), color);
                    }
                }
            }
        }

        private void DrawPolyomino()
        {
            Polyomino polyomino = _gameState.CurPolyomino;
            int[,] shape = polyomino.getCurrentShape();

            int blockSize = 30;
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    int blockIndex = shape[i, j];
                    if (blockIndex != -1)
                    {
                        Block block = polyomino.Blocks[blockIndex];
                        Color color = block switch
                        {
                            Block.a => Color.Blue,
                            Block.b => Color.Purple,
                            Block.c => Color.Yellow,
                            Block.d => Color.Green,
                            _ => Color.Red,
                        };
                        _spriteBatch.Draw(_rectangle, new Rectangle(j * 30, i * 30, blockSize, blockSize), color);
                    }
                }
            }

        }
    }
}
