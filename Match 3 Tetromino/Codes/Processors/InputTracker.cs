using Match_3_Tetromino.Codes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Processors
{
    internal class InputTracker : Processor
    {
        public event Action<WorldContext> PressedConfirm;
        public event Action<WorldContext> PressedMoveLeft;
        public event Action<WorldContext> PressedMoveRight;
    }
}
