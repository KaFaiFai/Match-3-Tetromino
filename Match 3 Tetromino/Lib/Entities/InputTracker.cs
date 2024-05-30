
using Match_3_Tetromino.Lib.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Lib.Entities
{
    internal class InputTracker
    {
        public event Action EnterPressed;
        public event Action MoveLeftPressed;
        public event Action MoveRightPressed;
        public event Action RotateClockwisePressed;
        public event Action RotateAntiClockwisePressed;

        public void Update(GameTime gameTime)
        {
            if (InputButton.keyStateMap[Keys.Enter] == InputState.justPressed)
            {
                EnterPressed?.Invoke();
            }
            if (InputButton.keyStateMap[Keys.Left] == InputState.justPressed)
            {
                MoveLeftPressed?.Invoke();
            }
            if (InputButton.keyStateMap[Keys.Right] == InputState.justPressed)
            {
                MoveRightPressed?.Invoke();
            }
            if (InputButton.keyStateMap[Keys.Q] == InputState.justPressed)
            {
                RotateClockwisePressed?.Invoke();
            }
            if (InputButton.keyStateMap[Keys.E] == InputState.justPressed)
            {
                RotateAntiClockwisePressed?.Invoke();
            }
        }
    }
}
