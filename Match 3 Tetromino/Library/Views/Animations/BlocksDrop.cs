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
    internal class BlocksDropController : AnimationController<List<BlockScene>>
    {
        public BlocksDropController(TimeSpan duration, List<BlockScene> startFrom, List<BlockScene> dropTo)
            : base(duration, startFrom, dropTo)
        {
            Current = new List<BlockScene>(startFrom);
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

    internal class BlocksDrop : Scene
    {
        public AnimationController<List<BlockScene>> Controller { get; private set; }
        public BlocksDrop(TimeSpan duration, List<BlockScene> startFrom, List<BlockScene> dropTo)
        {
            Controller = new BlocksDropController(duration, startFrom, dropTo);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Controller.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in Controller.Current)
            {
                block.Draw(spriteBatch);
            }
        }
    }
}
