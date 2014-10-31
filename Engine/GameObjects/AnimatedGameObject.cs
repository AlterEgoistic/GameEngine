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
    public class AnimatedGameObject : GameObject
    {
        private Animation currentAnimation;

        private Dictionary<String, Animation> animations;

        private bool mirror; 

        /// <summary>
        /// A GameObject using one or several animation(s) 
        /// </summary>
        /// <param name="layer">The layer the object should be drawn on</param>
        /// <param name="id">The identifying ID of the object</param>
        public AnimatedGameObject(String id = "", int layer = 0)
            : base(id, layer)
        {
            this.currentAnimation = null;
            this.animations = new Dictionary<String, Animation>();
            this.mirror = false; 
        }

        /// <summary>
        /// Creates an Animation object using the given parameters and adds it to the dictionary of Animation objects
        /// </summary>
        /// <param name="idInDictionary">The associated String for the animation in the Dictionary</param>
        /// <param name="assetName">The texture to load the parts of the Animation from</param>
        /// <param name="spriteSheetRows">The amount of rows the texture has</param>
        /// <param name="spriteSheetColumns">The amount of columns the texture has</param>
        /// <param name="frameTime">The time 1 frame should be displayed</param>
        /// <param name="isLooping">Whether the animation should repeat itself or not</param>
        public void LoadAnimation(String idInDictionary, String assetName, int spriteSheetRows, int spriteSheetColumns, float frameTime = 0.1f, bool isLooping = true)
        {
            Animation animationToAdd = new Animation(assetName, spriteSheetRows, spriteSheetColumns, frameTime, isLooping);
            this.animations[idInDictionary] = animationToAdd;
        }

        /// <summary>
        /// Creates an Animation object using the given parameters and adds it to the dictionary of Animation objects
        /// </summary>
        /// <param name="idInDictionary">The associated String for the animation in the Dictionary</param>
        /// <param name="assetName">The texture to load the parts of the Animation from</param>
        /// <param name="spriteSheetRows">The amount of rows the texture has</param>
        /// <param name="spriteSheetColumns">The amount of columns the texture has</param>
        /// <param name="startingSheetIndex">From which SheetIndex the animation should be starting</param>
        /// <param name="endingSheetIndex">At which SheetIndex the animation should end</param>
        /// <param name="frameTime">The time 1 frame should be displayed</param>
        /// <param name="isLooping">Whether the animation should repeat itself or not</param>
        public void LoadAnimation(String idInDictionary, String assetName, int spriteSheetRows, int spriteSheetColumns, int startingSheetIndex, int endingSheetIndex, float frameTime = 0.1f, bool isLooping = true)
        {
            Animation animationToAdd = new Animation(assetName, spriteSheetRows, spriteSheetColumns, startingSheetIndex, endingSheetIndex, frameTime, isLooping);
            this.animations[idInDictionary] = animationToAdd;
        }

        /// <summary>
        /// Adds an Animation object to the dictionary of Animation objects that can be played
        /// </summary>
        /// <param name="idInDictionary">The associated String for the animation in the Dictionary</param>
        /// <param name="animationToAdd">The animation object to add to the dictionary</param>
        /// <param name="frameTime">The time 1 frame should be displayed</param>
        /// <param name="isLooping">Whether the animation should repeat itself or not</param>
        public void LoadAnimation(String idInDictionary, Animation animationToAdd, float frameTime = 0.1f, bool isLooping = true)
        {
            if (animationToAdd == null)
            {
                throw new ArgumentNullException("Animation passed to LoadAnimation cannot be null");
            }
            this.animations[idInDictionary] = animationToAdd;
        }

        /// <summary>
        /// Plays an animation that is already loaded
        /// </summary>
        /// <param name="idInDictionary"></param>
        public void PlayAnimation(String idInDictionary)
        {
            if(animations[idInDictionary] == this.currentAnimation)
            {
                return;
            }
            if (!this.animations.ContainsKey(idInDictionary))
            {
                throw new KeyNotFoundException("The give key could not be found in the animations dictionary");
            }
            this.currentAnimation = animations[idInDictionary];
            this.currentAnimation.Play();
        }

        public override void Update(GameTime gameTime)
        {
            if(this.currentAnimation != null && this.isVisible)
            {
                this.currentAnimation.Update(gameTime);
                this.currentAnimation.Mirror = mirror; 
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.currentAnimation != null && this.isVisible)
            {
                this.currentAnimation.Draw(spriteBatch, this.position);
            }
            base.Draw(gameTime, spriteBatch);
        }

        public override int Width
        {
            get 
            {
                if(this.currentAnimation != null)
                {
                    return this.currentAnimation.Width;
                }
                else
                {
                    return base.Width;
                }
            }
        }

        public override int Height
        {
            get 
            {
                if(this.currentAnimation != null)
                {
                    return this.currentAnimation.Height;
                }
                else
                {
                    return base.Height;
                }
            }
        }

        public bool Mirror
        {
            get { return this.mirror; }
            set { this.mirror = value; }
        }

        
    }
}
