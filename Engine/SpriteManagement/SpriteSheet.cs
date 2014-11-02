#region Using statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Engine
{
    public class SpriteSheet
    {
        /// <summary>
        /// The amount of rows the spritesheet has
        /// </summary>
        private int sheetRows;

        /// <summary>
        /// The amount of columns the spritesheet has
        /// </summary>
        private int sheetColumns;

        /// <summary>
        /// Where the part of the spritesheet to use starts, if applicable
        /// </summary>
        protected int startingSheetIndex;

        /// <summary>
        /// Where the part of the spritesheet to use starts, if applicable
        /// </summary>
        protected int endingSheetIndex;

        /// <summary>
        /// The column index of the current sprite used
        /// </summary>
        protected int columnIndex;

        /// <summary>
        /// The row index of the current sprite used
        /// </summary>
        protected int rowIndex;

        /// <summary>
        /// The index in the spritesheet of the current sprite used
        /// </summary>
        private int sheetIndex;

        /// <summary>
        /// Whether the sprite(s) used should be flipped horizontally
        /// </summary>
        private bool mirror;

        /// <summary>
        /// The texture that contains the spritesheet
        /// </summary>
        protected Texture2D sprite;

        /// <summary>
        /// The rectangle that resembles the location of the current sheetindex in the spritesheet
        /// </summary>
        private Rectangle spritePart;

        private Vector2 center;

        private Color color; 

        /// <summary>
        /// A SpriteSheet using given amount of sheetrows and sheetcolumns
        /// </summary>
        /// <param name="assetName">Name of the spritesheet to use</param>
        /// <param name="sheetRows">Amount of rows in the spritesheet</param>
        /// <param name="sheetColumns">Amount of columns in the spritesheet</param>
        public SpriteSheet(String assetName, int sheetRows = 1, int sheetColumns = 1)
        {
            this.sprite = GameEnvironment.AssetManager.GetAsset<Texture2D>(assetName);
            this.sheetColumns = sheetColumns;
            this.sheetRows = sheetRows;

            this.sheetIndex = 0;
            this.columnIndex = 0;
            this.rowIndex = 0;
            this.startingSheetIndex = 0;
            this.endingSheetIndex = sheetRows * sheetColumns - 1;
            this.spritePart = new Rectangle(0, 0, this.Width, this.Height);
            this.center = new Vector2(this.Width / 2, this.Height / 2);
            this.color = Color.White;
        }
        /// <summary>
        /// A SpriteSheet using given amount of columns, rows parsing the part starting at startingSheetIndex and ending at endingSheetIndex.
        /// Change to public if necessary.
        /// </summary>
        /// <param name="assetName">Name of the spritesheet to use</param>
        /// <param name="sheetRows">Amount of rows in the spritesheet</param>
        /// <param name="sheetColumns">Amount of columns in the spritesheet</param>
        /// <param name="startingSheetIndex">The starting index of the part of the spritesheet that should be used for the spritesheet object</param>
        /// <param name="endingSheetIndex">The ending index of the part of the spriteshe tthat should be used for the spritesheet object </param>
        protected SpriteSheet(String assetName, int sheetRows, int sheetColumns, int startingSheetIndex, int endingSheetIndex)
        {
            if(endingSheetIndex < 0 || startingSheetIndex < 0 || startingSheetIndex > sheetRows * sheetColumns || endingSheetIndex > sheetRows * sheetColumns)
            {
                throw new ArgumentOutOfRangeException("starting- and endingSheetIndex cannot exceed the maximum sheetIndex value, nor can their value be below 0");
            }
            if(endingSheetIndex < startingSheetIndex)
            {
                throw new Exception("endingSheetIndex cannot be higher than startingSheetIndex");
            }
            this.sprite = GameEnvironment.AssetManager.GetAsset<Texture2D>(assetName);
            this.sheetColumns = sheetColumns;
            this.sheetRows = sheetRows;

            this.sheetIndex = 0;
            this.columnIndex = 0;
            this.rowIndex = 0;
            this.startingSheetIndex = startingSheetIndex;
            this.endingSheetIndex = endingSheetIndex;
            this.spritePart = new Rectangle(0, 0, this.Width, this.Height);
        }

        /// <summary>
        /// Draws the part of the spritesheet matching the current SheetIndex at given position
        /// *Automatically called by the corresponding (Sprite)GameObject*
        /// </summary>
        /// <param name="spriteBatch">Used to store and draw the sprites</param>
        /// <param name="position">Location of the sprite(gameobject) </param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(this.sprite, position, this.spritePart, this.color, 0.0f, Vector2.Zero, 1.0f, this.mirror ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.0f);
        }

        /// <summary>
        /// Draws the part of the spritesheet matching the current SheetIndex at given position and using additional options to modify the sprite drawing
        /// *Note: Override the draw method of the specific (Sprite)GameObject to use this version, since it is not the one automatically called*
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="origin"></param>
        /// <param name="scale"></param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, Vector2 origin, float scale)
        {
            spriteBatch.Draw(this.sprite, position, this.spritePart, this.color, rotation, origin, scale, this.mirror ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.0f);
        }

        /// <summary>
        /// Updates the rectangle of the spritepart drawn on the screen (using the SheetIndex)
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            this.columnIndex = this.sheetIndex % this.sheetColumns;
            this.rowIndex = (int) (this.sheetIndex / this.sheetColumns) % this.sheetRows;
            this.spritePart = new Rectangle((int) this.Width * columnIndex, (int) this.Height * rowIndex, this.Width, this.Height);
        }

        /// <summary>
        /// Get the color of a certain pixel on the current SheetIndex (not the entire spritesheet!) on given location
        /// </summary>
        /// <param name="x">The X location</param>
        /// <param name="y">The Y location</param>
        /// <returns>The Color at the given pixel</returns>
        public Color GetPixelColor(int x, int y)
        {
            Rectangle sourceRectangle = new Rectangle(this.Width * columnIndex + x, this.Height * rowIndex + y, 1, 1);
            Color[] retrievedColor = new Color[1];
            this.sprite.GetData<Color>(0, sourceRectangle, retrievedColor, 0, 1);
            return retrievedColor[0];
        }
        /// <summary>
        /// Get the color of a certain pixel on the current Sheetindex (not the entire spriteshe!) on given location
        /// </summary>
        /// <param name="location">The location of the pixel</param>
        /// <returns>The Color at the given pixel</returns>
        public Color GetPixelColor(Point location)
        {
            Rectangle sourceRectangle = new Rectangle(this.Width * columnIndex + location.X, this.Height * rowIndex + location.Y, 1, 1);
            Color[] retrievedColor = new Color[1];
            this.sprite.GetData<Color>(0, sourceRectangle, retrievedColor, 0, 1);
            return retrievedColor[0];
        }

        public int SheetIndex
        {
            get
            {
                return this.sheetIndex - this.startingSheetIndex;
            }
            set
            {
                if(value <= this.endingSheetIndex - this.startingSheetIndex && value >= 0)
                {
                    this.sheetIndex = value + this.startingSheetIndex;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Specified value for SheetIndex is higher or lower than the specified amount of sheets");
                }
            }
        }

        public int Width
        {
            get { return this.sprite.Width / this.sheetColumns; }
        }

        public int Height
        {
            get { return this.sprite.Height / this.sheetRows; } 
        }

        public Vector2 Center
        {
            get { return this.center; }
        }

        public bool Mirror
        {
            get { return this.mirror; }
            set { this.mirror = value; }
        }

        public int SheetElements
        {
            get { return this.endingSheetIndex - this.startingSheetIndex + 1; }
        }

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
    }
}
