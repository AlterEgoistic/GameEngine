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
        /// The current position on the screen of the mouse
        /// </summary>
        public Vector2 MousePosition
        {
            get { return new Vector2(this.currentMouseState.Position.X, this.currentMouseState.Position.Y); }
            set { Mouse.SetPosition((int) Math.Floor(value.X), (int) Math.Floor(value.Y)); }
        }

        public Vector2 Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }
    }
}
