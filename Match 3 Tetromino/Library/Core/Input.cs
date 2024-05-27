using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Library.Core
{
    internal enum InputState
    {
        justPressed, holding, notPressed
    }

    internal static class Input
    {
        static private InputState _enterState = InputState.notPressed;
        static public InputState EnterState { get { return _enterState; } }

        static public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (_enterState == InputState.notPressed) _enterState = InputState.justPressed;
                else _enterState = InputState.holding;
            }
            else
            {
                _enterState = InputState.notPressed;
            }
        }
    }
}
