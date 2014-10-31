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
        protected Point world;

        public GameState(Point world)
            : base()
        {
            this.world = world; 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public Point World
        {
            get { return this.world; }
        }

    }
}
