#region Using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Engine
{
    public class GameEnvironment : Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        protected InputHelper inputHelper;
        protected Color backgroundColor;

        protected static Point screen;
        protected static AssetManager assetManager;
        protected static GameStateManager gameStateManager;

        protected static Camera camera;

        private static bool pauseInput;

        protected GameEnvironment()
        {
            this.graphics = new GraphicsDeviceManager(this);
            GameEnvironment.pauseInput = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            this.inputHelper = new InputHelper();
            this.backgroundColor = Color.CornflowerBlue;

            GameEnvironment.assetManager = new AssetManager(this.Content);
            GameEnvironment.gameStateManager = new GameStateManager();
            GameEnvironment.screen = new Point(0, 0);
            GameEnvironment.camera = new Camera();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            this.HandleInput();
            gameStateManager.Update(gameTime);
            camera.Update(gameTime); 
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(this.backgroundColor);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.View);
                GameEnvironment.gameStateManager.Draw(gameTime, this.spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected virtual void HandleInput()
        {
             this.inputHelper.Update();
             GameEnvironment.gameStateManager.HandleInput(this.inputHelper);
            
        }

        public static AssetManager AssetManager
        {
            get { return GameEnvironment.assetManager; }
        }

        public static GameStateManager GameStateManager
        {
            get { return GameEnvironment.gameStateManager; }
        }

        /// <summary>
        /// Set the size of the screen *NOTE* Only works on startup for now
        /// </summary>
        /// <param name="screenSize">The (new) size of the screen in pixels</param>
        protected void SetScreenSize(Point screenSize)
        {
            this.graphics.PreferredBackBufferWidth = screenSize.X;
            this.graphics.PreferredBackBufferHeight = screenSize.Y;
            this.graphics.ApplyChanges();
            GameEnvironment.screen = screenSize;
        }

        public static Point Screen
        {
            get { return GameEnvironment.screen; }
        }

        public static Camera Camera
        {
            get { return GameEnvironment.camera; }
        }

        public static bool PauseInput
        {
            get { return GameEnvironment.pauseInput; }
            set { GameEnvironment.pauseInput = value; }
        }
    }
}
