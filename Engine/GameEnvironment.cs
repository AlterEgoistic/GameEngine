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
        /// <summary>
        /// The graphics device manager the game should use for handling the display
        /// </summary>
        protected GraphicsDeviceManager graphics;
        /// <summary>
        /// The spritebatch to draw on/with
        /// </summary>
        protected SpriteBatch spriteBatch;

        /// <summary>
        /// The inputhandler that needs to check all the input within the game
        /// </summary>
        protected InputHelper inputHelper;
        
        /// <summary>
        /// The background color of the spritebatch
        /// </summary>
        protected Color backgroundColor;

        /// <summary>
        /// The dimensions of the screen
        /// </summary>
        protected static Point screen;
        /// <summary>
        /// The assetmanager to load all types of content and play sounds and music in the game
        /// </summary>
        private static AssetManager assetManager;

        /// <summary>
        /// The GameStateManager that handles all the switching between different menus and levels in the game
        /// </summary>
        private static GameStateManager gameStateManager;

        /// <summary>
        /// The camera to use for displaying
        /// </summary>
        private static Camera camera;

        /// <summary>
        /// Makes the game ignore input
        /// </summary>
        private static bool pauseInput;

        /// <summary>
        /// The amount of frames between calls to fixed update
        /// </summary>
        private static int fixedUpdateInterval;

        /// <summary>
        /// Determines how many frames have passed since the last fixed update
        /// </summary>
        private float frameCounter;

        /// <summary>
        /// Creates a new Game environment
        /// </summary>
        /// <param name="fixedUpdateInterval">The amount of frames between calls to fixed update </param>
        public GameEnvironment(int fixedUpdateInterval = 1)
        {
            this.graphics = new GraphicsDeviceManager(this);
            GameEnvironment.fixedUpdateInterval = fixedUpdateInterval;
            GameEnvironment.pauseInput = false;
            this.Content.RootDirectory = "Content";
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
            GameEnvironment.screen = new Point(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);

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
            this.frameCounter++;
            this.HandleInput();

            while(this.frameCounter> GameEnvironment.fixedUpdateInterval)
            {
                this.frameCounter -= GameEnvironment.fixedUpdateInterval;
                this.FixedUpdate(gameTime);
            }

            GameEnvironment.gameStateManager.Update(gameTime);
            if(!GameEnvironment.camera.IsDisabled)
            {
                GameEnvironment.camera.Update(gameTime);
            }
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

        /// <summary>
        /// Update that is being called every given interval. Called before regular Update
        /// Uses fixedUpdateInterval to determine call frequency
        /// </summary>
        protected virtual void FixedUpdate(GameTime gameTime)
        {
            GameEnvironment.gameStateManager.FixedUpdate(gameTime);
        }

        /// <summary>
        /// Detects input to keyboard and mouse
        /// Called before update and fixedupdate
        /// </summary>
        protected virtual void HandleInput()
        {
            if(!GameEnvironment.pauseInput)
            {
                this.inputHelper.Update();
                GameEnvironment.gameStateManager.HandleInput(this.inputHelper);
            }
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

        public static int FixedUpdateInterval
        {
            get { return GameEnvironment.fixedUpdateInterval; }
            set { GameEnvironment.fixedUpdateInterval = value; }
        }
    }
}
