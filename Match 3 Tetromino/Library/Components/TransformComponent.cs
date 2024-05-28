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
        public Point Size { get; private set; }
        public Rectangle Rect { get; private set; }

        public TransformComponent(int x, int y, int width = 0, int height = 0)
        {
            Center = new Point(x, y);
            Size = new Point(width, height);
            UpdateRectangle();
        }

        public void UpdateCenter(int? x, int? y)
        {
            Center = new Point(x ?? Center.X, y ?? Center.Y);
            UpdateRectangle();
        }

        private void UpdateRectangle()
        {
            // form Rectangle.Center: If Width or Height is an odd number, the center point will be rounded down.
            // https://docs.monogame.net/api/Microsoft.Xna.Framework.Rectangle.html
            int topLeftX = Center.X - (int)Math.Ceiling(Size.X / 2.0);
            int topLeftY = Center.Y - (int)Math.Ceiling(Size.Y / 2.0);
            Rect = new Rectangle(topLeftX, topLeftY, Size.X, Size.Y);
        }
    }
}
