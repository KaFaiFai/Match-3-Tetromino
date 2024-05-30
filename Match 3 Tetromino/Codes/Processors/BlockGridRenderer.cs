using Match_3_Tetromino.Codes.Properties;
using Match_3_Tetromino.Codes.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Match_3_Tetromino.Codes.Models;
using Microsoft.Xna.Framework.Graphics;
using Match_3_Tetromino.Codes.Services;

namespace Match_3_Tetromino.Codes.Processors
{
    internal class BlockGridRenderer : Processor
    {
        static private float BlockSize = 30;

        private List<(Rectangle, Block)> _toBeDrawn; // cache the results of blocks and their positions

        override public void Update(GameTime gameTime, WorldContext context)
        {
            base.Update(gameTime, context);

            _toBeDrawn.Clear();
            foreach (Entity entity in context.Entities)
            {
                if (entity.HasProperties(new List<Type> { typeof(BlockGrid), typeof(GridLayout), typeof(Transform) }))
                {
                    BlockGrid blockBoard = entity.GetProperty<BlockGrid>();
                    GridLayout gridLayout = entity.GetProperty<GridLayout>();
                    Transform transform = entity.GetProperty<Transform>();
                    Vector2 center = transform.Center;
                    Vector2 gridCenter = gridLayout.RowCol.ToVector2() * gridLayout.CellSize / 2;

                    for (int i = 0; i < gridLayout.RowCol.X; i++)
                    {
                        for (int j = 0; j < gridLayout.RowCol.Y; j++)
                        {
                            Block? block = blockBoard.Data[i, j];
                            if (block != null)
                            {
                                Vector2 cellCenter = GetCellPositionAt(new Point(i, j), gridLayout.CellSize);
                                Point size = new Point((int)gridLayout.CellSize);
                                Point topLeft = (center - gridCenter + cellCenter - size.ToVector2() / 2).ToPoint();
                                Rectangle rectangle = new Rectangle(topLeft, size);
                                _toBeDrawn.Add((rectangle, (Block)block));
                            }
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, WorldContext context)
        {
            base.Draw(spriteBatch, context);

            foreach (var (rect, block) in _toBeDrawn)
            {
                Color color = block switch
                {
                    Block.a => Color.Blue,
                    Block.b => Color.Purple,
                    Block.c => Color.Yellow,
                    Block.d => Color.Green,
                    _ => Color.Red,
                };
                spriteBatch.Draw(Contents.Pixel, rect, color);
            }
        }

        /// <summary>
        /// Get the relative position of the center of the cell from top left
        /// </summary>
        public Vector2 GetCellPositionAt(Point rowCol, float cellSize)
        {
            Vector2 cellCenter = rowCol.ToVector2() * cellSize + new Vector2(cellSize) / 2;
            return cellCenter;
        }
    }
}
