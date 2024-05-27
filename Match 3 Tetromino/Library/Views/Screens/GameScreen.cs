using Match_3_Tetromino.Library.Core;
using Match_3_Tetromino.Library.Models;
using Match_3_Tetromino.Library.StateManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Match_3_Tetromino.Library.Views.Screens
{
    internal class GameScreen : IScreen
    {
        private GameState _gameState;
        private SpriteBatch _spriteBatch;
        private Texture2D _rectangle;

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
            if (Input.EnterState == InputState.justPressed)
            {
                List<(RowCol, Block)> willDropTo = _gameState.Board.WillDropTo(_gameState.CurPolyomino, 0);
                _gameState.Board.PlaceBlocks(willDropTo);
                _gameState.AdvancePolyomino();
            };
        }

        void IScreen.Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            DrawGrid();
            DrawPolyomino();

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
                        _spriteBatch.Draw(_rectangle, new Rectangle(i * 30, j * 30, blockSize, blockSize), color);
                    }
                }
            }

        }
    }
}
