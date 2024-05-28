using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Components
{
    internal class TransformComponent
    {
        // represents the center
        public int X { get; private set; }
        public int Y { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public TransformComponent(int x, int y, int width = 0, int height = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void Update(int? x, int? y)
        {
            X = x ?? X;
            Y = y ?? Y;
        }

        public Point GetCenter()
        {
            return new Point(X, Y);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
    }
}
