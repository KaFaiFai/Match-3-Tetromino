using Match_3_Tetromino.Library.Core;
using Match_3_Tetromino.Library.Models;
using Match_3_Tetromino.Library.Views.Scenes;
using Match_3_Tetromino.Library.Views.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Views.Animations
{
    internal class BlocksDrop : AnimationWithStates<List<BlockScene>>
    {
        public BlocksDrop(TimeSpan duration, List<BlockScene> from, List<BlockScene> to)
            : base(duration, from, to) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in Current)
            {
                block.Draw(spriteBatch);
            }
        }

        protected override List<BlockScene> Interpolation(double t)
        {
            for (int i = 0; i < From.Count; i++)
            {
                int x = Convert.ToInt32(From[i].Transform.Center.X * (1 - t) + To[i].Transform.Center.X * t);
                int y = Convert.ToInt32(From[i].Transform.Center.Y * (1 - t) + To[i].Transform.Center.Y * t);
                Current[i].Transform.UpdateCenter(x: x, y: y);
            }
            return Current;
        }
    }
}
