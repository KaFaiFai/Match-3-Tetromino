using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Views.Animations
{
    internal abstract class Animation
    {
        public event Action Started;
        public event Action Completed;

        public TimeSpan Duration { get; private set; }
        public TimeSpan ElapsedTime { get; private set; }

        public Animation(TimeSpan duration)
        {
            Duration = duration;
            ElapsedTime = TimeSpan.Zero;
        }

        public virtual void Update(GameTime gameTime)
        {
            ElapsedTime = ElapsedTime.Add(gameTime.ElapsedGameTime);
            double t = GetProgress();
            if (t <= 0) Started?.Invoke();
            if (t >= 1) Completed?.Invoke();
        }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public double GetProgress()
        {
            double t = ElapsedTime.TotalMilliseconds / Duration.TotalMilliseconds;
            return Math.Clamp(t, 0, 1);
        }
    }
}
