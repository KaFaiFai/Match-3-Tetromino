﻿using Match_3_Tetromino.Library.Core;
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
    internal class BlocksDrop
    {
        public event Action Started;
        public event Action Completed;

        private TimeSpan _duration;
        private List<BlockScene> _startFrom;
        private List<BlockScene> _dropTo;

        private TimeSpan _elapsedTime;
        private List<BlockScene> _current;

        public BlocksDrop(TimeSpan duration, List<BlockScene> startFrom, List<BlockScene> dropTo)
        {
            _duration = duration;
            _startFrom = startFrom;
            _dropTo = dropTo;
            _elapsedTime = new TimeSpan(0, 0, 0);
            _current = new List<BlockScene>(_startFrom);
        }

        public void Update(GameTime gameTime)
        {
            _elapsedTime = _elapsedTime.Add(gameTime.ElapsedGameTime);
            double t = _elapsedTime.TotalMilliseconds / _duration.TotalMilliseconds;
            _current = Interpolate(_startFrom, _dropTo, t);
            if (t <= 0) Started?.Invoke();
            if (t >= 1) Completed?.Invoke();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in _current)
            {
                block.Draw(spriteBatch);
            }
        }

        private List<BlockScene> Interpolate(List<BlockScene> from, List<BlockScene> to, double t)
        {
            // t will be clamped in [0, 1]
            // when t = 0, return from
            // when t = 1, return to

            double t_ = Math.Clamp(t, 0, 1);
            for (int i = 0; i < from.Count; i++)
            {
                int x = Convert.ToInt32(from[i].Transform.Center.X * (1 - t_) + to[i].Transform.Center.X * t_);
                int y = Convert.ToInt32(from[i].Transform.Center.Y * (1 - t_) + to[i].Transform.Center.Y * t_);
                _current[i].Transform.UpdateCenter(x: x, y: y);
            }
            return _current;
        }
    }
}
