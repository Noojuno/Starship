using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Starship.Event.Input;

namespace Starship.Input
{
    public static class InputManager
    {
        private static KeyboardState oldKeyboardState;
        private static GamePadState oldGamePadState;

        private static bool MouseLocked;

        public static void Update(GameTime gameTime)
        {
            //Keyboard State
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (!keyboardState.IsKeyDown(key) && oldKeyboardState.IsKeyDown(key))
                {
                    StarshipEngine.EVENT_BUS.Publish(new KeyReleaseEvent(key));
                }
            }

            //Gamepad State
            GamePadState gamePadState = GamePad.GetState(1);
            /* foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (!gamePadState(button) && oldGamePadState.IsButtonDown(button))
                {
                    StarshipEngine.EVENT_BUS.Publish(new ButtonReleaseEvent(button));
                }
            } */

            // Update saved state.
            oldKeyboardState = keyboardState;
            oldGamePadState = gamePadState;

            if (MouseLocked)
            {
                int x = StarshipEngine.INSTANCE.Size.X + (StarshipEngine.INSTANCE.Size.Width / 2);
                int y = StarshipEngine.INSTANCE.Size.Y + (StarshipEngine.INSTANCE.Size.Height / 2);
                Mouse.SetPosition(x, y);
            }
        }

        public static void SetMouseLocked(bool locked = true)
        {
            MouseLocked = locked;
        }

        public static void ToggleMouseLocked()
        {
            MouseLocked = !MouseLocked;
        }
    }
}
