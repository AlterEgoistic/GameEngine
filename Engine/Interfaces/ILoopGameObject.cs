#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#endregion

namespace Engine
{
    public interface ILoopGameObject
    {
        /// <summary>
        /// Draws this object or the objects in this list on the screen
        /// *Automatically called by its corresponding GameState*
        /// </summary>
        /// <param name="gameTime">Contains information about how long the game has been running and time between updates</param>
        /// <param name="spriteBatch">Used to store and draw the sprites</param>
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// Updates information such as the position and velocity of this object or the objects in this list
        /// *Automatically called by its corresponding GameState*
        /// </summary>
        /// <param name="gameTime">Contains information about how long the game has been running and time between updates</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Updates the input checking on this object or the objects in this list
        /// *Automatically called by its corresponding GameState*
        /// </summary>
        /// <param name="inputHelper"></param>
        void HandleInput(InputHelper inputHelper);

        /// <summary>
        /// Sets the object or the objects in this list back to its original state
        /// </summary>
        void Reset();

        int Layer
        {
            get;
        }

        String ID
        {
            get;
        }

        LoopingObjectList Parent
        {
            get;
            set;
        }

        bool IsVisible
        {
            get;
            set;
        }
    }
}
