using Match_3_Tetromino.Components;
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

        public Block Block_ { get; set; }
        public TransformComponent Transform { get; private set; }

        public BlockScene(int x, int y, Block block)
        {
            Block_ = block;
            Transform = new TransformComponent(x, y, BaseSize, BaseSize);
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
            spriteBatch.Draw(Contents.Pixel, Transform.Rect, color);
        }
    }
}
