using Match_3_Tetromino.Library.Core;
using Match_3_Tetromino.Library.Models;
using Match_3_Tetromino.Library.Views.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Views.Scenes
{
    internal class BlockScene : Scene
    {
        static int BaseSize = 30;

        public int X { get; set; }
        public int Y { get; set; }
        public Block Block_ { get; set; }

        public BlockScene(int x, int y, Block block)
        {
            X = x;
            Y = y;
            Block_ = block;
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Block_ switch
            {
                Block.a => Color.Blue,
                Block.b => Color.Purple,
                Block.c => Color.Yellow,
                Block.d => Color.Green,
                _ => Color.Red,
            };
            spriteBatch.Draw(Contents.Pixel, new Rectangle(X, Y, BaseSize, BaseSize), color);
        }
    }
}
