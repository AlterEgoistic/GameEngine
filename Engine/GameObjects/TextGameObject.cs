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
        /// <summary>
        /// The string to draw
        /// </summary>
        protected String text;

        /// <summary>
        /// The color of the text
        /// </summary>
        protected Color textColor;

        /// <summary>
        /// The font to use for drawing the text
        /// </summary>
        protected SpriteFont font;

        /// <summary>
        /// Creates a new Game Object that draws text on the screen
        /// </summary>
        /// <param name="fontName">The name of the font asset to load</param>
        /// <param name="text">The text to draw</param>
        /// <param name="textColor">The color of the text</param>
        /// <param name="id">The name the object can be identified with</param>
        /// <param name="layer">The layer to draw the object on</param>
        public TextGameObject(String fontName, String text, Color textColor, String id = "", int layer = 0)
            : base(id, layer)
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

        /// <summary>
        /// Draws the text on the screen
        /// </summary>
        /// <param name="gameTime">Contains information about the time in the game</param>
        /// <param name="spriteBatch">The spritebatch to draw it on/with</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.isVisible && this.isInView)
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

        public override Vector2 Center
        {
            get { return new Vector2(this.position.X + this.Width / 2, this.position.Y + this.Height / 2); }
        }
    }
}
