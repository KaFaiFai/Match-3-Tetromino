using Match_3_Tetromino.Library.Views.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Views.Animations
{
    internal abstract class AnimationController<T>
    {
        public event Action Started;
        public event Action Completed;

        public TimeSpan Duration { get; private set; }
        public T From { get; private set; }
        public T To { get; private set; }

        public TimeSpan ElapsedTime { get; private set; }
        public T Current { get; protected set; }

        public AnimationController(TimeSpan duration, T from, T to)
        {
            Duration = duration;
            From = from;
            To = to;
            ElapsedTime = TimeSpan.Zero;
            Current = Interpolate(0);
        }

        public void Update(GameTime gameTime)
        {
            ElapsedTime = ElapsedTime.Add(gameTime.ElapsedGameTime);
            double t = ElapsedTime.TotalMilliseconds / Duration.TotalMilliseconds;
            Current = Interpolate(t);
            if (t <= 0) Started?.Invoke();
            if (t >= 1) Completed?.Invoke();
        }

        /// <summary>
        /// Return the interpolation of current state with the following conditions:
        /// When t <= 0, return from.
        /// When t >= 1, return to.
        /// </summary>
        public T Interpolate(double t)
        {
            if (t <= 0) { return From; }
            if (t >= 1) { return To; }
            return Interpolation(t);
        }

        protected abstract T Interpolation(double t);
    }
}
