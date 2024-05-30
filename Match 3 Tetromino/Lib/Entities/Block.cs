using Match_3_Tetromino.Lib.Components;
using Match_3_Tetromino.Lib.Models;
using Match_3_Tetromino.Lib.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Lib.Entities
{
    internal class Block
    {
        public Transform Transform { get; set; }
        public BlockType Type { get; set; }

        public Block(BlockType type)
        {
            Transform = new Transform() { Size = new Vector2(40, 40) };
            Type = type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Type switch
            {
                BlockType.a => Color.Blue,
                BlockType.b => Color.Purple,
                BlockType.c => Color.Yellow,
                BlockType.d => Color.Green,
                _ => Color.Red,
            };
            spriteBatch.Draw(Contents.Pixel, Transform.GetRectangle(), color);
        }
    }
}
