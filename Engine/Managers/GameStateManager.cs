#region Using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Engine
{
    /// <summary>
    /// Allows switching between different Game States
    /// </summary>
    public sealed class GameStateManager
    {
        private GameState currentGameState;

        internal GameStateManager()
        {
            this.currentGameState = null;
        }

        /// <summary>
        /// A bit of a workaround for getting to load the correct state, but much more simplified and easier to work with in an IDE
        /// </summary>
        /// <typeparam name="T">The gamestate to switch to</typeparam>
        /// <param name="restart">If the gamestate is identical, reset it instead</param>
        public void SwitchTo<T>(bool restart = false) where T:GameState
        {
            if(this.currentGameState != null && typeof(T) == this.currentGameState.GetType() && !restart)
            {
                Console.Error.WriteLine("You are switching to the same state as the current game state. Method will terminate.");
                return;
            }
            GameEnvironment.Camera.CameraPosition = Vector2.Zero;
            this.currentGameState = (GameState) Activator.CreateInstance(typeof(T));
            if(this.CurrentWorld == GameEnvironment.Screen)
            {
                GameEnvironment.Camera.IsDisabled = true;
            }
            else
            {
                GameEnvironment.Camera.IsDisabled = false;
            }
        }

        /// <summary>
        /// The Gamestate currently active
        /// </summary>
        public GameState CurrentGameState
        {
            get 
            {
                if(this.currentGameState != null)
                {
                    return this.currentGameState;
                }
                throw new NullReferenceException("currentGameState has no value");
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.currentGameState != null)
            {
                this.currentGameState.Draw(gameTime, spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            if(this.currentGameState != null)
            {
                this.currentGameState.Update(gameTime);
            }
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if(this.currentGameState != null)
            {
                this.currentGameState.HandleInput(inputHelper);
            }
        }

        public void FixedUpdate(GameTime gameTime)
        {
            if(this.currentGameState != null)
            {
                this.currentGameState.FixedUpdate(gameTime);
            }
        }

        /// <summary>
        /// Resets the current state back to it's original state
        /// </summary>
        public void ResetCurrentState()
        {
            if(this.currentGameState != null)
            {
                this.currentGameState.Reset();
            }
        }

        public Point CurrentWorld
        {
            get { return this.currentGameState.World; }
        }
    }
}
