using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Core
{
    internal static class Contents
    {
        public static Texture2D Pixel { get; private set; }

        public static void Load(ContentManager content, SpriteBatch spriteBatch)
        {
            // ref: https://stackoverflow.com/questions/5751732/draw-rectangle-in-xna-using-spritebatch
            Pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
        }
    }
}
