#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#endregion

namespace Engine
{
    public abstract class GameObject : ILoopGameObject
    {
        /// <summary>
        /// THe layer to draw the object on
        /// </summary>
        protected int layer;

        /// <summary>
        /// Whether the object should be visible (drawn on screen) or not
        /// </summary>
        protected bool isVisible;

        /// <summary>
        /// Whether the object is visible in the current viewport/camera view
        /// </summary>
        protected bool isInView;

        /// <summary>
        /// If a collider should be generated for this object, based on the interface implemented
        /// </summary>
        protected bool generateCollider;

        /// <summary>
        /// Whether the object should carry on playing animations when no longer visible in the current viewport/camera view
        /// </summary>
        protected bool alwaysAnimate;

        /// <summary>
        /// If the object should also check for collisions when not visible in the viewport/camera view
        /// </summary>
        protected bool alwaysCheckCollision;

        /// <summary>
        /// The identifying name/tag/id for this object; to recognize the GameObject with
        /// </summary>
        protected String id;

        /// <summary>
        /// The current position of the object on the screen/spritebatch
        /// </summary>
        protected Vector2 position;

        /// <summary>
        /// The displacement of the object per second
        /// </summary>
        protected Vector2 velocity;

        /// <summary>
        /// The ObjectList or GameState that contains this object (the one directly above only)
        /// </summary>
        protected LoopingObjectList parent;

        public GameObject(String id = "", int layer = 0)
        {
            this.alwaysCheckCollision = true;
            this.isVisible = true;
            this.id = id;
            this.layer = layer; 
            this.velocity = Vector2.Zero;
            this.position = Vector2.Zero;
            this.isInView = false;
            if(this.generateCollider)
            {
                ICircle circleCollider = this as ICircle;
                if(circleCollider != null)
                {
                    circleCollider.BoundingCircle = new Circle(this.Width > this.Height ? this.Width : this.Height, this.Center);
                }
                IRectangle rectCollider = this as IRectangle;
                if(rectCollider != null)
                {
                    rectCollider.BoundingBox = new Rectangle((int) this.position.X, (int) this.position.Y, this.Width, this.Height);
                }
            }
            if(this is ICircle && this is IRectangle)
            {
                throw new NotSupportedException("GameObjects cannot implement multiple colliders/collider shapes");
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            this.isInView = GameEnvironment.Camera.IsInView(this);
            this.position += (this.velocity * (float) gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        /// <summary>
        /// Check if any keys or mouse buttons are pressed
        /// </summary>
        /// <param name="inputHelper"></param>
        public virtual void HandleInput(InputHelper inputHelper)
        {

        }

        /// <summary>
        /// Resets the object to it's starting state
        /// </summary>
        public virtual void Reset()
        {
            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
        }

        /// <summary>
        /// Checks whether two objects, that both have a collider, are colliding with eachother or not
        /// </summary>
        /// <param name="other">The object to check collision with</param>
        /// <param name="includeInvisible">Whether it should check collision if objects that have isVisible set to false</param>
        /// <returns>Objects collide or not</returns>
        public bool CollidesWith(GameObject other, bool includeInvisible = false)
        {
            if(((!this.isVisible || !other.IsVisible) && !includeInvisible) || (this.isInView && this.alwaysCheckCollision))
            {
                return false;
            }
            
            IRectangle ownRectCollider = this as IRectangle;
            if (ownRectCollider != null)
            {
                IRectangle otherRectCollider = other as IRectangle;
                if (otherRectCollider != null)
                {
                    return ownRectCollider.BoundingBox.Intersects(otherRectCollider.BoundingBox);
                }
                ICircle otherCircleCollider = other as ICircle;
                if(otherCircleCollider != null)
                {
                    return ownRectCollider.BoundingBox.Intersects(otherCircleCollider.BoundingCircle);
                }
            }
            ICircle ownCircleCollider = this as ICircle;
            if(ownCircleCollider != null)
            {
                IRectangle otherRectCollider = other as IRectangle;
                if(otherRectCollider != null)
                {
                    return ownCircleCollider.BoundingCircle.Intersects(otherRectCollider.BoundingBox);
                }
                ICircle otherCircleColider = other as ICircle;
                if(otherCircleColider != null)
                {
                    return ownCircleCollider.BoundingCircle.Intersects(otherCircleColider.BoundingCircle);
                }
            }
            throw new ArgumentException("Not every GameObject has a shape attached");
        }

        /*
        public Rectangle GetCollisionRectangle(GameObject gameObj, bool includeInvisibleObjects)
        {
            if(!this.isInView || (!this.isVisible && includeInvisibleObjects) || (!gameObj.IsVisible && includeInvisibleObjects))
            {
                return Rectangle.Empty;
            }
            return Rectangle.Intersect(gameObj.BoundingBox, this.BoundingBox);
        }
         */

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
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        public abstract int Width
        {
            get;
        }

        public abstract int Height
        {
            get;
        }

        public abstract Vector2 Center
        {
            get;
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

        public bool IsInView
        {
            get { return this.isInView; }
        }

        /// <summary>
        /// The rectangle around the sprite itself
        /// Used for culling
        /// </summary>
        internal Rectangle DrawingBox
        {
            get { return new Rectangle((int) this.position.X, (int) this.position.Y, this.Width, this.Height); }
        }
    }
}
