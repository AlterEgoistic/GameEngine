using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine
{
    public sealed class Camera
    {
        /// <summary>
        /// The object the camera is currently following
        /// </summary>
        private GameObject focussedObject;

        /// <summary>
        /// The matrix used to calculate the camera viewport
        /// </summary>
        private Matrix view;

        /// <summary>
        /// The displacement of the camera per second
        /// </summary>
        private Vector3 velocity;

        /// <summary>
        /// Whether the camera is disabled 
        /// Automatically set when World is set to the same size as the Screen
        /// </summary>
        private bool isDisabled;

        /// <summary>
        /// The current instance
        /// Only one camera can exist at a time
        /// </summary>
        private static Camera instance;

        /// <summary>
        /// Creates a new camera
        /// </summary>
        internal Camera()
        {
            this.velocity = Vector3.Zero;
            this.view = Matrix.CreateFromYawPitchRoll(0f, 0f, 0f);
        }

        /// <summary>
        /// Sets the camera position to that of an object in the game
        /// </summary>
        /// <param name="gameObj">The object to focus on</param>
        public void SetFocusOn(GameObject gameObj)
        {
            this.focussedObject = gameObj;
        }

        public void Update(GameTime gameTime)
        {
            this.view.Translation += -this.velocity * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public Matrix View
        {
            get {  return this.view;  }
        }

        /// <summary>
        /// Whether the object is displayed in the current camera view
        /// </summary>
        /// <param name="gameObj">The object to check visibility on</param>
        /// <returns>In view or not</returns>
        public bool IsInView(GameObject gameObj)
        {
            if(this.isDisabled)
            {
                return true; 
            }
            Rectangle currentView = new Rectangle((int) -this.view.Translation.X, (int) -this.view.Translation.Y, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
            return gameObj.DrawingBox.Intersects(currentView);           
        }

        /// <summary>
        /// Converts a position relative to the camera to a world position, 
        /// </summary>
        /// <param name="viewPosition">The position on the camera</param>
        /// <returns>The position on the world</returns>
        public Vector2 ViewToWorld(Vector2 viewPosition)
        {
            return viewPosition + new Vector2(this.view.Translation.X, this.view.Translation.Y);
        }

        /// <summary>
        /// Converts a worldposition to a position relative to the camera
        /// </summary>
        /// <param name="worldPosition">The position in the world</param>
        /// <returns>The position on the camera</returns>
        public Vector2 WorldToView(Vector2 worldPosition)
        {
            return worldPosition - new Vector2(this.view.Translation.X, this.view.Translation.Y);
        }

        public Vector2 CameraPosition
        {
            get { return new Vector2(-this.view.Translation.X, -this.view.Translation.Y); }
            set 
            {
                this.view.Translation = new Vector3(-value.X, -value.Y, 0);
            }
        }

        public Vector2 Velocity
        {
            get { return new Vector2(this.velocity.X, this.velocity.Y); }
            set 
            { 
                this.velocity.X = value.X;
                this.velocity.Y = value.Y; 
            }
        }

        public bool IsDisabled
        {
            get { return this.isDisabled; }
            set { this.isDisabled = value; }
        }

    }
}
