using Match_3_Tetromino.Codes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Properties
{
    internal record MainBoard : Property
    {
        public int LeftIndex { get; set; }
    }
}
