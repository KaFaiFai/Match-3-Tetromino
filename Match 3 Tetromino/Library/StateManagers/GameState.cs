﻿using Match_3_Tetromino.Library.Models;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.StateManagers
{
    internal class GameState
    {
        private Random _random;
        private Board _board;
        private Polyomino _curPolyomino;
        private Polyomino _nextPolyomino;

        public Board Board { get { return _board; } }
        public Polyomino CurPolyomino { get { return _curPolyomino; } }
        public Polyomino NextPolyomino { get { return _nextPolyomino; } }
        public int LeftIndex { get; private set; }

        public GameState(int seed = 0)
        {
            _random = new Random(seed);
            _board = new Board(new RowCol(8, 5));
            _curPolyomino = Polyomino.random(_random);
            _nextPolyomino = Polyomino.random(_random);
        }

        public void AdvancePolyomino()
        {
            _curPolyomino = _nextPolyomino;
            _nextPolyomino = Polyomino.random(_random);
        }

        public void Move(bool toLeft)
        {
            LeftIndex += toLeft ? -1 : 1;
            LeftIndex = _board.WallKick(CurPolyomino, LeftIndex);
        }
    }
}
