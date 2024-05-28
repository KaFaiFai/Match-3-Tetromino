using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Components
{
    internal class TransformComponent
    {
        public Point Center { get; private set; }
        public Point Size { get; private set; } // Scale not taken into account
        public Vector2 Scale { get; private set; }
        public Rectangle Rect { get; private set; }

        public TransformComponent(int x, int y, int width = 0, int height = 0, Vector2? scale = null)
        {
            Center = new Point(x, y);
            Size = new Point(width, height);
            Scale = scale ?? Vector2.One;
            UpdateRectangle();
        }

        public void Update(int? x = null, int? y = null, Vector2? scale = null)
        {
            Center = new Point(x ?? Center.X, y ?? Center.Y);
            Scale = scale ?? Scale;
            UpdateRectangle();
        }

        private void UpdateRectangle()
        {
            // form Rectangle.Center: If Width or Height is an odd number, the center point will be rounded down.
            // https://docs.monogame.net/api/Microsoft.Xna.Framework.Rectangle.html
            int sizeX = (int)(Size.X * Scale.X);
            int sizeY = (int)(Size.Y * Scale.Y);
            int topLeftX = Center.X - (int)Math.Ceiling(sizeX / 2.0);
            int topLeftY = Center.Y - (int)Math.Ceiling(sizeY / 2.0);
            Rect = new Rectangle(topLeftX, topLeftY, sizeX, sizeY);
        }
    }
}
