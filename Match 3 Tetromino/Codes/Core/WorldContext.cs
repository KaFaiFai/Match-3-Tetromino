﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Core
{
    internal record WorldContext
    {
        public List<Entity> Entities { get; init; }
    }
}
