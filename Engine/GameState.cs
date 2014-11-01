using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class GameState : LoopingObjectList
    {
        /// <summary>
        /// The GameWorldSize of the current GameState
        /// </summary>
        protected Point world;

        /// <summary>
        /// Creates a new GameState
        /// </summary>
        /// <param name="world">The GameWorldSize of the current GameState</param>
        public GameState(Point world)
            : base()
        {
            this.world = world; 
        }

        /// <summary>
        /// Updates all the GameObjects and GameObjectLists in the GameState
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Resets the gamestate to its starting state including the GameObjects and GameObjectLists it is parrent of
        /// </summary>
        public override void Reset()
        {
            GameEnvironment.Camera.CameraPosition = Vector2.Zero;
            base.Reset();
        }

        public Point World
        {
            get { return this.world; }
        }
    }
}
