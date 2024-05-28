using Match_3_Tetromino.Library.Models;
using Match_3_Tetromino.Library.Views.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Components
{
    /// <summary>
    /// Represents grid of this shape
    /// 
    ///  (0, 0)    (0, 1)  ...  (0, C-1)
    ///  (1, 0)    (1, 1)  ...  (1, C-1)
    ///    .          .            .
    ///    .          .            .
    ///    .          .            .
    /// (R-1, 0)  (R-1, 1) ... (R-1, C-1)
    /// </summary>
    internal class GridComponent
    {
        public int NumRow { get; private set; }
        public int NumCol { get; private set; }
        public int CellSize { get; private set; }

        private Point _gridCenter;

        public GridComponent(int numRow, int numCol, int cellSize)
        {
            NumRow = numRow;
            NumCol = numCol;
            CellSize = cellSize;

            int X = NumCol * CellSize / 2;
            int Y = NumRow * CellSize / 2;
            _gridCenter = new Point(X, Y);
        }

        /// <summary>
        /// Get the offset from the center of the cell to center of the grid
        /// </summary>
        public Point GetCellLocationAt(int row, int col)
        {
            int X = CellSize * col + CellSize / 2;
            int Y = CellSize * row + CellSize / 2;
            Point cellCenter = new Point(X, Y);
            return cellCenter - _gridCenter;
        }
        public Point GetCellLocationAt(double row, double col)
        {
            int X = (int)(CellSize * col + CellSize / 2);
            int Y = (int)(CellSize * row + CellSize / 2);
            Point cellCenter = new Point(X, Y);
            return cellCenter - _gridCenter;
        }
    }
}
