#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#endregion

namespace Engine
{
    public class SpriteGameObject : GameObject
    {
        /// <summary>
        /// The spritesheet to draw
        /// </summary>
        protected SpriteSheet sprite; 

        /// <summary>
        /// A GameObject that has a sprite(sheet) that needs to be drawn on the screen
        /// </summary>
        /// <param name="assetName">Name of the file the sprite(sheet) is stored in</param>
        /// <param name="sheetRows">The amount of rows the spritesheet has</param>
        /// <param name="sheetColumns">The amount of columns the spritesheet has</param>
        /// <param name="id">Name of the spritegame object to identify it</param>
        /// <param name="layer">The layer to draw the sprite on</param>
        public SpriteGameObject(String assetName,  int sheetRows = 1, int sheetColumns = 1, String id = "", int layer = 0)
            : base(id, layer)
        {
            if(assetName != null)
            {
                this.sprite = new SpriteSheet(assetName, sheetRows, sheetColumns);
            }
            else
            {
                throw new NullReferenceException("String 'assetName' of SpriteGameObject cannot be null");
            }
        }

        /// <summary>
        /// Draws the sprite on the spritebatch passed
        /// </summary>
        /// <param name="gameTime">Provides info about the current time in the game</param>
        /// <param name="spriteBatch">The spritebatch to draw the sprite on </param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.isInView && this.isVisible)
            {
                this.sprite.Draw(spriteBatch, this.position);
            }
            base.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Updates information on the gameObject, such as the current sprite sheet index or position
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if((this.isInView || this.alwaysAnimate) && this.isVisible)
            {
                this.sprite.Update(gameTime);
            }
            base.Update(gameTime);
        }

        
        public override int Width
        {
            get { return this.sprite.Width;  }
        }
        
        public override int Height
        {
            get { return this.sprite.Height; }
        }

        public override Vector2 Center
        {
            get { return this.sprite.Center; }
        }

        public bool Mirror
        {
            get { return this.sprite.Mirror; }
            set { this.sprite.Mirror = value; }
        }

        public SpriteSheet Sprite
        {
            get { return this.sprite; }
        }
    }
}
