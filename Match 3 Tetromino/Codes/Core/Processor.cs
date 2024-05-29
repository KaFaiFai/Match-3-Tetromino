using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Core
{
    internal abstract class Processor // as System in ECS
    {
        virtual public void Update(GameTime gameTime, WorldContext context) { }

        virtual public void Draw(SpriteBatch spriteBatch, WorldContext context) { }
    }
}
