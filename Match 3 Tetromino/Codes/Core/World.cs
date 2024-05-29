using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Core
{
    internal abstract class World
    {
        public List<Entity> Entities { get; private set; } = new List<Entity>();
        public List<Processor> Processors { get; private set; } = new List<Processor>();

        virtual public void Initialize() { }

        virtual public void Update(GameTime gameTime)
        {
            foreach (Processor processor in Processors)
            {
                processor.Update(gameTime, getContext());
            }
        }

        virtual public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Processor processor in Processors)
            {
                processor.Draw(spriteBatch, getContext());
            }
        }

        private WorldContext getContext()
        {
            return new WorldContext()
            {
                Entities = Entities,
            };
        }
    }
}
