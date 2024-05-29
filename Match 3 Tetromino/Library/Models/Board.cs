using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Models
{
    internal class Board
    {
        public RowCol Size { get; set; }
        public Block?[,] Data { get; set; }

        public Board(RowCol size)
        {
            Size = size;
            Data = new Block?[size.Row, size.Col];
        }

        public bool CanPlace(Polyomino polyomino, int leftIndex)
        {
            int width = polyomino.getWidth();
            int rightIndex = leftIndex + width - 1;
            return (leftIndex >= 0) && (rightIndex < Size.Col);
        }

        public int WallKick(Polyomino polyomino, int leftIndex)
        {
            // push polyomino inside the board, return a new leftIndex
            if (leftIndex < 0) return 0;
            else if (polyomino.getWidth() + leftIndex > Size.Col) return Size.Col - polyomino.getWidth();
            else return leftIndex;
        }

        public List<(RowCol, Block)> WillDropTo(Polyomino polyomino, int leftIndex)
        {
            // Top row is at 0 and bottom row is at Size.Row - 1
            List<int> lowestEmptySpace = Enumerable.Repeat(0, Size.Col).ToList();

            for (int col = 0; col < Size.Col; col++)
            {
                for (int row = Size.Row - 1; row > -1; row--)
                {
                    if (Data[row, col] == null)
                    {
                        lowestEmptySpace[col] = row;
                        break;
                    }
                }
            }

            List<(RowCol, Block)> dropTo = new List<(RowCol, Block)>();
            int[,] shape = polyomino.getCurrentShape();
            // bottom-up search
            for (int i = shape.GetLength(0) - 1; i > -1; i--)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    int blockIndex = shape[i, j];
                    if (blockIndex != -1)
                    {
                        Block block = polyomino.Blocks[blockIndex];
                        int col = leftIndex + j;
                        dropTo.Add((new RowCol(lowestEmptySpace[col], col), block));
                        lowestEmptySpace[col] -= 1;
                    }
                }
            }

            return dropTo;
        }

        public void PlaceBlocks(List<(RowCol, Block)> cells)
        {
            foreach (var (rowCol, block) in cells)
            {
                Data[rowCol.Row, rowCol.Col] = block;
            }
        }

        public void RemoveBlocks(List<RowCol> rowCols)
        {
            foreach (var rowCol in rowCols)
            {
                Data[rowCol.Row, rowCol.Col] = null;
            }
        }

        public List<(RowCol, Block)> FindMatch3()
        {
            List<(RowCol, Block)> results = new List<(RowCol, Block)>();

            // scan horizontal lines
            for (int i = 0; i < Size.Row; i++)
            {
                List<(RowCol, Block)> curResults = new List<(RowCol, Block)>();
                for (int j = 0; j < Size.Col; j++)
                {
                    Block? block = Data[i, j];
                    if (block == null)
                    {
                        if (curResults.Count >= 3)
                        {
                            results.AddRange(curResults);
                        }
                        curResults = new List<(RowCol, Block)>();
                    }
                    else if (curResults.Count == 0)
                    {
                        curResults.Add((new RowCol(i, j), (Block)block));
                    }
                    else
                    {
                        if (block == curResults.Last().Item2)
                        {
                            curResults.Add((new RowCol(i, j), (Block)block));
                        }
                        else
                        {
                            if (curResults.Count >= 3)
                            {
                                results.AddRange(curResults);
                            }
                            curResults = new List<(RowCol, Block)> { (new RowCol(i, j), (Block)block) };
                        }
                    }
                }
                if (curResults.Count >= 3)
                {
                    results.AddRange(curResults);
                }
            }


            // scan vertical lines
            for (int j = 0; j < Size.Col; j++)
            {
                List<(RowCol, Block)> curResults = new List<(RowCol, Block)>();
                for (int i = 0; i < Size.Row; i++)
                {
                    Block? block = Data[i, j];
                    if (block == null)
                    {
                        if (curResults.Count >= 3)
                        {
                            results.AddRange(curResults);
                        }
                        curResults = new List<(RowCol, Block)>();
                    }
                    else if (curResults.Count == 0)
                    {
                        curResults.Add((new RowCol(i, j), (Block)block));
                    }
                    else
                    {
                        if (block == curResults.Last().Item2)
                        {
                            curResults.Add((new RowCol(i, j), (Block)block));
                        }
                        else
                        {
                            if (curResults.Count >= 3)
                            {
                                results.AddRange(curResults);
                            }
                            curResults = new List<(RowCol, Block)> { (new RowCol(i, j), (Block)block) };
                        }
                    }
                }
                if (curResults.Count >= 3)
                {
                    results.AddRange(curResults);
                }
            }
            return results;
        }

        /// <summary>
        /// Return the start and end position of flying blocks
        /// </summary>
        public List<(RowCol, RowCol, Block)> FindFlyingBlocks()
        {
            List<(RowCol, RowCol, Block)> flyingBlocks = new List<(RowCol, RowCol, Block)>();
            for (int j = 0; j < Size.Col; j++)
            {
                int numEmpty = 0;
                for (int i = Size.Row - 1; i > -1; i--)
                {
                    if (Data[i, j] == null)
                    {
                        numEmpty++;
                    }
                    else if (numEmpty != 0)
                    {
                        RowCol start = new RowCol(i, j);
                        RowCol end = new RowCol(i + numEmpty, j);
                        Block block = (Block)Data[i, j];
                        flyingBlocks.Add((start, end, block));
                    }
                }
            }
            return flyingBlocks;
        }
    }
}
