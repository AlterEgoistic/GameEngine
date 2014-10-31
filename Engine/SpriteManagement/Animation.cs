#region Using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
#endregion

namespace Engine
{
    public class Animation : SpriteSheet
    {
        private bool isLooping;

        private float frametime;

        private float elapsedTime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetName">Name of the spritesheet to use</param>
        /// <param name="sheetRows">The amount of rows the spritesheet has</param>
        /// <param name="sheetColumns">The amount of columns the spritesheet has</param>
        /// <param name="frametime">The speed of the animation/time between two frames</param>
        /// <param name="isLooping">Whether the animation is repeated constantly or not</param>
        public Animation(String assetName, int sheetRows, int sheetColumns, float frametime = 0.1f, bool isLooping = true) 
            : base(assetName, sheetRows, sheetColumns)
        {
            this.frametime = frametime;
            this.isLooping = isLooping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetName">Name of the spritesheet to use</param>
        /// <param name="sheetRows">The amount of rows the spritesheet has</param>
        /// <param name="sheetColumns">The amount of columns the spritesheet has</param>
        /// <param name="startingSheetIndex">The starting index of the part of the spritesheet that should be used for the spritesheet object</param>
        /// <param name="endingSheetIndex">The ending index of the part of the spriteshe tthat should be used for the spritesheet object </param>
        /// <param name="frametime">The speed of the animation/time between two frames</param>
        /// <param name="isLooping">Whether the animation is repeated constantly or not</param>
        public Animation(String assetName, int sheetRows, int sheetColumns, int startingSheetIndex, int endingSheetIndex, float frametime = 0.1f, bool isLooping = true)
            : base(assetName, sheetRows, sheetColumns, startingSheetIndex, endingSheetIndex)
        {
            this.frametime = frametime;
            this.isLooping = isLooping;
        }

        /// <summary>
        /// Start the animation from the beginning
        /// </summary>
        public void Play()
        {
            this.SheetIndex = 0;
            this.elapsedTime = 0.0f;
        }

        public override void Update(GameTime gameTime)
        {
            if(this.SheetElements != 0)
            {
                this.elapsedTime += (float) gameTime.ElapsedGameTime.TotalSeconds;
                while(this.elapsedTime >= this.frametime)
                {
                    this.elapsedTime -= this.frametime;
                    if(this.isLooping)
                    {
                        this.SheetIndex = (this.SheetIndex + 1) % this.SheetElements;
                    }
                    else
                    {
                        this.SheetIndex = Math.Min(this.SheetIndex + 1, this.SheetElements - 1);
                    }
                }
            }
            base.Update(gameTime);
        }

        public bool AnimationEnded
        {
            get
            {
                if(!this.isLooping)
                {
                    return this.SheetIndex >= (this.SheetElements - 1);
                }
                throw new Exception("AnimationEnded is not defined for a looping animation");
            }
        }

        public bool IsLooping
        {
            get { return this.isLooping; }
        }

    }
}
