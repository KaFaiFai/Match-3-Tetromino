using Match_3_Tetromino.Codes.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Properties
{
    internal record Transform : Property
    {
        public Vector2 Center { get; set; } = Vector2.Zero;
        public Vector2 Size { get; set; } = Vector2.Zero; // Scale not taken into account
        public Vector2 Scale { get; set; } = Vector2.Zero;
    }
}
