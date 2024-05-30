using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Lib.Services
{
    internal enum InputState
    {
        justPressed, holding, notPressed
    }

    internal static class InputButton
    {
        static public Dictionary<Keys, InputState> keyStateMap = new Dictionary<Keys, InputState>();

        static InputButton()
        {
            List<Keys> keysToCheck = new List<Keys> { Keys.Enter, Keys.Left, Keys.Right, Keys.Q, Keys.E };
            foreach (Keys key in keysToCheck)
            {
                keyStateMap[key] = InputState.notPressed;
            }
        }

        static public void Update()
        {
            foreach (var (key, state) in keyStateMap)
            {
                if (Keyboard.GetState().IsKeyDown(key))
                {
                    if (state == InputState.notPressed) keyStateMap[key] = InputState.justPressed;
                    else keyStateMap[key] = InputState.holding;
                }
                else
                {
                    keyStateMap[key] = InputState.notPressed;
                }
            }
        }
    }
}
