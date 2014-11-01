#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Engine
{
    public sealed class InputHelper
    {
        private KeyboardState currentKeyboardstate;

        private KeyboardState previousKeyboardstate;

        private MouseState currentMouseState;

        private MouseState previousMouseState;

        private Vector2 scale;

        /// <summary>
        /// Checks every update for keypresses, mousepresses and mouse movement
        /// </summary>
        public InputHelper()
        {
            this.scale = new Vector2(1,1);
        }

        /// <summary>
        /// Update the current and previous state of the keyboard and mouse to detect changes in user input
        /// </summary>
        public void Update()
        {
            this.previousKeyboardstate = this.currentKeyboardstate;
            this.previousMouseState = this.currentMouseState;

            this.currentKeyboardstate = Keyboard.GetState();
            this.currentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Whether the given key was pressed once (returns true once if the user is holding the key)
        /// </summary>
        /// <param name="k">The key to check</param>
        /// <returns>Whether the key was pressed or not</returns>
        public bool IsKeyPressed(Keys k)
        {
            return this.currentKeyboardstate.IsKeyDown(k) && this.previousKeyboardstate.IsKeyUp(k);
        }

        /// <summary>
        /// Whether the given key is being held down by the user
        /// </summary>
        /// <param name="k">The key to check</param>
        /// <returns>Whether given is held down</returns>
        public bool IsHoldingKey(Keys k)
        {
            return this.currentKeyboardstate.IsKeyDown(k) && this.previousKeyboardstate.IsKeyDown(k);
        }

        /// <summary>
        /// Whether the given key was released since last time pressed
        /// </summary>
        /// <param name="k">The key to check</param>
        /// <returns>Whether the key was released since the last update</returns>
        public bool IsKeyReleased(Keys k)
        {
            return this.currentKeyboardstate.IsKeyUp(k) && this.previousKeyboardstate.IsKeyDown(k); 
        }

        /// <summary>
        /// Whether the left mouse button is/was pressed
        /// </summary>
        /// <returns>Whether left mouse button was pressed or not</returns>
        public bool LeftMousePressed()
        {
            return this.currentMouseState.LeftButton == ButtonState.Pressed && this.previousMouseState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Whether the left mouse button is being held
        /// </summary>
        /// <returns>If the user is holding it or not</returns>
        public bool LeftMouseHold()
        {
            return this.currentMouseState.LeftButton == ButtonState.Pressed && this.previousMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Whether the right mouse button is/was pressed
        /// </summary>
        /// <returns>If the right button was pressed or not</returns>
        public bool RightMousePressed()
        {
            return this.currentMouseState.RightButton == ButtonState.Pressed && this.previousMouseState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// Whether the right mouse button is being held
        /// </summary>
        /// <returns>Whether the user is holding the right mouse button or not</returns>
        public bool RightMouseHold()
        {
            return this.currentMouseState.RightButton == ButtonState.Pressed && this.previousMouseState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// If the user has scrolled up using the mouse wheel
        /// </summary>
        /// <returns>Scrolled up or not</returns>
        public bool ScrolledUp()
        {
            return this.currentMouseState.ScrollWheelValue > this.previousMouseState.ScrollWheelValue;
        }

        /// <summary>
        /// If the user has scrolled down using the mouse wheel
        /// </summary>
        /// <returns>Scrolled down or not</returns>
        public bool ScrolledDown()
        {
            return this.currentMouseState.ScrollWheelValue < this.previousMouseState.ScrollWheelValue;
        }

        /// <summary>
        /// The current position on the screen of the mouse
        /// </summary>
        public Vector2 MousePosition
        {
            get { return new Vector2(this.currentMouseState.Position.X, this.currentMouseState.Position.Y); }
            set { Mouse.SetPosition((int) value.X, (int) value.Y); }
        }

        public Keys[] GetPressedKeys()
        {
            List<Keys> pressed = new List<Keys>();
            for(int i = 0; i < this.currentKeyboardstate.GetPressedKeys().Length; i++)
            {
                if(this.IsKeyPressed(this.currentKeyboardstate.GetPressedKeys()[i]))
                {
                    pressed.Add(this.currentKeyboardstate.GetPressedKeys()[i]);
                }
            }
            return pressed.ToArray();
        }

        public Keys[] GetHeldKeys()
        {
            return this.currentKeyboardstate.GetPressedKeys();
        }

        /// <summary>
        /// Gets the value of the scrollwhee
        /// Positive value means more scrolled up than down
        /// </summary>
        public int ScrollWheelValue
        {
            get { return this.currentMouseState.ScrollWheelValue; }
        }
    }
}
