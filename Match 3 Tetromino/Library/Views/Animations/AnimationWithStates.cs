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
    internal abstract class AnimationWithStates<T> : Animation
    {
        public T From { get; private set; }
        public T To { get; private set; }
        protected T Current { get; set; }

        public AnimationWithStates(TimeSpan duration, T from, T to) : base(duration)
        {
            From = from;
            To = to;
            Current = Interpolate(0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Current = Interpolate(GetProgress());
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
