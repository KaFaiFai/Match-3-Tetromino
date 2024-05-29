using Match_3_Tetromino.Codes.Core;
using Match_3_Tetromino.Codes.Models;
using Match_3_Tetromino.Codes.Properties;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Entities
{
    internal class BoardEntity : Entity
    {
        public BoardEntity()
        {
            AddProperty(new Transform() { Center = new Vector2(1280, 720) / 2 });
            AddProperty(new GridLayout() { CellSize = 30, RowCol = new Point(10, 5) });
            AddProperty(new BlockBoard() { Data = new Block?[10, 5] });
        }
    }
}
