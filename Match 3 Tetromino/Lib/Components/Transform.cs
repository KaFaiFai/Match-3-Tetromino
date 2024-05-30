using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Lib.Components
{
    internal record Transform
    {
        public Vector2 Center { get; set; } = Vector2.Zero;
        public Vector2 Size { get; set; } = Vector2.Zero; // Scale not taken into account
        public Vector2 Scale { get; set; } = Vector2.One;

        public Rectangle GetRectangle()
        {
            Vector2 scaledSize = Size * Scale;
            Vector2 topLeft = Center - scaledSize / 2;
            return new Rectangle(topLeft.ToPoint(), scaledSize.ToPoint());
        }
    }
}
