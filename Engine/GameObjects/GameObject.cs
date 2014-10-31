#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#endregion

namespace Engine
{
    public class GameObject : ILoopGameObject
    {
        protected Vector2 position;

        protected Vector2 velocity;

        protected String id;

        protected int layer;

        protected LoopingObjectList parent;

        protected bool isVisible;

        protected bool isInView;

        protected Vector2 displacementPerUpdate;

        public GameObject(String id = "", int layer = 0)
        {
            this.isVisible = true;
            this.id = id;
            this.layer = layer; 
            this.velocity = Vector2.Zero;
            this.position = Vector2.Zero;
            this.isInView = false;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if(GameEnvironment.Camera.IsInView(this))
            {
                this.isInView = true;
            }
            else
            {
                this.isInView = false;
            }
            this.displacementPerUpdate = this.velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            this.position += this.displacementPerUpdate; 
        }

        public virtual void HandleInput(InputHelper inputHelper)
        {

        }

        public virtual void Reset()
        {
            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
            GameEnvironment.Camera.CameraPosition = Vector2.Zero;
        }

        public bool CollidesWith(GameObject gameObj, bool includeInvisibleObjects = false)
        {
            if(!this.isInView || (!this.isVisible && includeInvisibleObjects) || (!gameObj.IsVisible && includeInvisibleObjects))
            {
                return false;
            };
            return this.BoundingBox.Intersects(gameObj.BoundingBox);
            
        }

        public Rectangle GetCollisionRectangle(GameObject gameObj, bool includeInvisibleObjects)
        {
            if(!this.isInView || (!this.isVisible && includeInvisibleObjects) || (!gameObj.IsVisible && includeInvisibleObjects))
            {
                return Rectangle.Empty;
            }
            return Rectangle.Intersect(gameObj.BoundingBox, this.BoundingBox);
        }

        public bool ColidesWithCircle(Vector2 center, int radius)
        {
            // Find the closest point to the circle within the rectangle
            int closestX = MathHelper.Clamp((int) center.X, this.BoundingBox.Left, this.BoundingBox.Right);
            int closestY = MathHelper.Clamp((int) center.Y, this.BoundingBox.Top, this.BoundingBox.Bottom);

            // Calculate the distance between the circle's center and this closest point
            float distanceX = center.X - closestX;
            float distanceY = center.Y - closestY;

            Console.WriteLine(distanceX + "   " + distanceY); 

            //Since the distance is always bigger than the individual distanceX and distanceY (pythagoras), return false if 
            //either is larger than the radius
            if(distanceX > radius || distanceY > radius)
            {
                return false;
            }
            // If the distance is less than the circle's radius, an intersection occurs
            float distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);
            return distanceSquared <= (radius * radius);
        }

        public LoopingObjectList Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        public int Layer
        {
            get { return this.layer; }
        }

        public String ID
        {
            get { return this.id; }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int) this.position.X, (int) this.position.Y, this.Width, this.Height); }
        }

        public virtual int Width
        {
            get { return 0; }
        }

        public virtual int Height
        {
            get { return 0; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Vector2 Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }

        public Vector2 DisplacementPerUpdate
        {
            get { return this.displacementPerUpdate; }
        }
    }
}
