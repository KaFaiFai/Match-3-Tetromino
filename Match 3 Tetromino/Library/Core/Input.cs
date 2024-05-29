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
        static public InputState MoveLeft { get; private set; } = InputState.notPressed;
        static public InputState MoveRight { get; private set; } = InputState.notPressed;

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

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (MoveLeft == InputState.notPressed) MoveLeft = InputState.justPressed;
                else MoveLeft = InputState.holding;
            }
            else
            {
                MoveLeft = InputState.notPressed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (MoveRight == InputState.notPressed) MoveRight = InputState.justPressed;
                else MoveRight = InputState.holding;
            }
            else
            {
                MoveRight = InputState.notPressed;
            }
        }
    }
}
