using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine
{
    public class Camera
    {
        private GameObject focussedObject;

        private Vector3 focusPoint;

        private Matrix view;

        private Vector3 velocity;

        private bool isDisabled;

        public Camera()
        {
            this.focusPoint = Vector3.Zero;
            this.velocity = Vector3.Zero;
            this.view = Matrix.CreateFromYawPitchRoll(0f, 0f, 0f);
        }

        public void SetFocusOn(GameObject gameObj)
        {
            this.focussedObject = gameObj;
        }

        public void Update(GameTime gameTime)
        {
            this.view.Translation += -this.velocity * (float) gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public Vector2 FocusPoint
        {
            get { return new Vector2(this.focusPoint.X, this.focusPoint.Y); }
            set { this.focusPoint = new Vector3(value, 0); }
        }

        public Matrix View
        {
            get {  return this.view;  }
        }

        public bool IsInView(GameObject gameObj)
        {
            if(this.isDisabled)
            {
                return true; 
            }
            Rectangle currentView = new Rectangle((int) -this.view.Translation.X, (int) -this.view.Translation.Y, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
            return gameObj.DrawingBox.Intersects(currentView);           
        }

        public Vector2 ViewToWorld(Vector2 viewPosition)
        {
            return viewPosition + new Vector2(this.view.Translation.X, this.view.Translation.Y);
        }

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
