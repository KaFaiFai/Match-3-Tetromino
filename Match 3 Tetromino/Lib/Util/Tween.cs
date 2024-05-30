using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Lib.Util
{
    internal class Tween<T>
    {
        public event Action<T> Started;
        public event Action<T> Updating;
        public event Action<T> Ended;

        public T From { get; private set; }
        public T To { get; private set; }
        public TimeSpan Duration { get; private set; }

        public T Current { get; private set; }
        public TimeSpan ElapsedTime { get; private set; }

        public Tween(T from, T to, TimeSpan duration)
        {
            Duration = duration;
            From = from;
            To = to;
        }

        public void Update(TimeSpan timeSpan)
        {
            double t = ElapsedTime.TotalMilliseconds / Duration.TotalMilliseconds;
            Current = Interpolate((float)t);
            if (t <= 0) Started?.Invoke(Current);
            if (t >= 1) Ended?.Invoke(Current);
            Updating?.Invoke(Current);

            ElapsedTime = ElapsedTime.Add(timeSpan);
        }

        private T Interpolate(float t)
        {
            // linear interpolation
            return Op.Add(Op.Multiply(From, 1 - t), Op.Multiply(To, t));
        }

        // from https://github.com/craftworkgames/MonoGame.Extended/blob/develop/source/MonoGame.Extended.Tweening/LinearOperations.cs
        private static class Op
        {
            static Op()
            {
                var a = Expression.Parameter(typeof(T));
                var b = Expression.Parameter(typeof(T));
                var c = Expression.Parameter(typeof(float));
                Add = Expression.Lambda<Func<T, T, T>>(Expression.Add(a, b), a, b).Compile();
                Multiply = Expression.Lambda<Func<T, float, T>>(Expression.Multiply(a, c), a, c).Compile();
            }

            public static Func<T, T, T> Add { get; }
            public static Func<T, float, T> Multiply { get; }
        }
    }
}
