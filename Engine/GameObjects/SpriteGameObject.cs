#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#endregion

namespace Engine
{
    public class SpriteGameObject : GameObject
    {
        protected SpriteSheet sprite; 

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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.isVisible && this.isInView)
            {
                this.sprite.Draw(spriteBatch, this.position);
                base.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(this.isVisible)
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

        public virtual Vector2 Center
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
