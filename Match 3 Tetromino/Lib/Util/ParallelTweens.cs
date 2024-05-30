using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Lib.Util
{
    internal class ParallelTweens<T> : Tween<float>
    {
        private List<Tween<T>> _tweens;
        public ParallelTweens(List<Tween<T>> tweens)
            : base(0, 1, tweens.MaxBy(e => e.Duration).Duration)
        {
            Debug.Assert(tweens.Count > 0);
            _tweens = tweens;
        }

        override public void Update(TimeSpan timeSpan)
        {
            base.Update(timeSpan);
            foreach (var t in _tweens)
            {
                t.Update(timeSpan);
            }
        }
    }
}
