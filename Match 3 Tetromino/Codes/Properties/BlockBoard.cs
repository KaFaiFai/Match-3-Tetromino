using Match_3_Tetromino.Codes.Core;
using Match_3_Tetromino.Codes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Properties
{
    internal record BlockBoard : Property
    {
        public Block?[,] Data { get; set; } = new Block?[,] { };
    }
}
