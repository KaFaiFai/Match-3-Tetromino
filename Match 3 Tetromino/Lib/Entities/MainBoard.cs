using Match_3_Tetromino.Lib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Match_3_Tetromino.Lib.Models;
using Match_3_Tetromino.Lib.Util;
using Match_3_Tetromino.Library.Models;
using Microsoft.Xna.Framework.Graphics;

namespace Match_3_Tetromino.Lib.Entities
{
    internal class MainBoard
    {
        public event Action BoardResolved;

        public Transform Transform { get; set; }
        public GridLayout GridLayout { get; set; }
        public BlockType?[,] BlockTypes { get; set; }

        // For animation
        private List<Block> _blocks = null;
        private ParallelTweens<Vector2> _tweens = null;

        public MainBoard(Point rowCol)
        {
            Transform = new Transform();
            GridLayout = new GridLayout() { CellSize = 50, RowCol = rowCol };
            BlockTypes = new BlockType?[rowCol.X, rowCol.Y];
        }

        public void Update(GameTime gameTime)
        {
            _tweens?.Update(gameTime.ElapsedGameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < BlockTypes.GetLength(0); i++)
            {
                for (int j = 0; j < BlockTypes.GetLength(1); j++)
                {
                    BlockType? type = BlockTypes[i, j];
                    if (type != null)
                    {
                        Block block = new Block((BlockType)type);
                        block.Transform = new Transform()
                        {
                            Center = Transform.Center + GridLayout.GetCellPosition(new Point(i, j)),
                            Size = new Vector2(40, 40),
                        };

                        block.Draw(spriteBatch);
                    }
                }
            }

            if (_blocks != null)
            {
                foreach (Block block in _blocks)
                {
                    block.Draw(spriteBatch);
                }
            }
        }

        public void PlacePolyomino(Polyomino polyomino, int leftIndex)
        {
            _blocks = new List<Block>();
            List<Tween<Vector2>> tweens = new List<Tween<Vector2>>();

            List<(Point, BlockType)> dropTo = WillDropTo(polyomino, leftIndex);
            foreach (var (rowCol, type) in dropTo)
            {
                Block newBlock = new Block(type);
                Tween<Vector2> newTween = new Tween<Vector2>(
                    GridLayout.GetCellPosition(rowCol - new Point(6, 0)),
                    GridLayout.GetCellPosition(rowCol),
                    TimeSpan.FromMilliseconds(1000)
                );
                newTween.Updating += (value) => newBlock.Transform.Center = value;
                newTween.Ended += (_) => BlockTypes[rowCol.X, rowCol.Y] = type;

                _blocks.Add(newBlock);
                tweens.Add(newTween);
            }
            _tweens = new ParallelTweens<Vector2>(tweens);
            _tweens.Ended += (value) => ResolveBoard();
        }

        private void ResolveBoard()
        {
            List<(Point, Point, BlockType)> flyingBlocks = FindFlyingBlocks();
            List<(Point, BlockType)> match3 = FindMatch3();

            if (flyingBlocks.Count > 0)
            {
                _blocks = new List<Block>();
                List<Tween<Vector2>> tweens = new List<Tween<Vector2>>();

                foreach (var (start, end, type) in flyingBlocks)
                {
                    Block newBlock = new Block(type);
                    Tween<Vector2> newTween = new Tween<Vector2>(
                        GridLayout.GetCellPosition(start),
                        GridLayout.GetCellPosition(end),
                        TimeSpan.FromMilliseconds(1000)
                    );
                    newTween.Started += (_) => BlockTypes[start.X, start.Y] = null;
                    newTween.Updating += (value) => newBlock.Transform.Center = value;
                    newTween.Ended += (_) => BlockTypes[end.X, end.Y] = type;

                    _blocks.Add(newBlock);
                    tweens.Add(newTween);
                }
                _tweens = new ParallelTweens<Vector2>(tweens);
                _tweens.Ended += (value) => ResolveBoard();
            }
            else if (match3.Count > 0)
            {
                _blocks = new List<Block>();
                List<Tween<Vector2>> tweens = new List<Tween<Vector2>>();

                foreach (var (rowCol, type) in match3)
                {
                    Block newBlock = new Block(type);
                    Tween<Vector2> newTween = new Tween<Vector2>(
                        Vector2.One,
                        Vector2.Zero,
                        TimeSpan.FromMilliseconds(1000)
                    );
                    newTween.Started += (_) => BlockTypes[rowCol.X, rowCol.Y] = null;
                    newTween.Updating += (value) => newBlock.Transform.Scale = value;

                    _blocks.Add(newBlock);
                    tweens.Add(newTween);
                }
                _tweens = new ParallelTweens<Vector2>(tweens);
                _tweens.Ended += (value) => ResolveBoard();

                ResolveBoard();
            }
            else
            {
                _blocks = null;
                _tweens = null;
                BoardResolved?.Invoke();
            }
        }

        private List<(Point, BlockType)> WillDropTo(Polyomino polyomino, int leftIndex)
        {
            return new();
        }

        private List<(Point, BlockType)> FindMatch3()
        {
            return new();
        }


        private List<(Point, Point, BlockType)> FindFlyingBlocks()
        {
            return new();
        }
    }
}
