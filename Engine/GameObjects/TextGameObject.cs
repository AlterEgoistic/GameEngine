#region Using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Engine
{
    public class TextGameObject : GameObject
    {
        protected String text;

        protected Color textColor;

        protected SpriteFont font;

        public TextGameObject(String fontName, String text, Color textColor, String id = "", int layer = 0)
            : base()
        {
            this.textColor = textColor;
            if(text != null)
            {
                this.text = text;
            }
            else
            {
                throw new NullReferenceException("String 'Text' of a TextGameObject was assigned null");
            }

            this.font = GameEnvironment.AssetManager.GetAsset<SpriteFont>(fontName);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.isVisible && this.text != null && this.isInView)
            {
                spriteBatch.DrawString(this.font, this.text, this.position, this.textColor);
                base.Draw(gameTime, spriteBatch);
            }
        }

        public String Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public Color TextColor
        {
            get { return this.textColor; }
            set { this.textColor = value; }
        }

        public override int Width
        {
            get { return (int) this.font.MeasureString(this.text).X; }
        }

        public override int Height
        {
            get { return (int) this.font.MeasureString(this.text).Y; }
        }
    }
}
