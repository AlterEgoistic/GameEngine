#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#endregion

namespace Engine
{
    public abstract class GameObject : ILoopGameObject
    {
        protected Vector2 position;

        protected Vector2 velocity;

        protected String id;

        protected int layer;

        protected LoopingObjectList parent;

        protected bool isVisible;

        protected bool isInView;

        protected bool generateCollider;

        protected IRectangle rectangleCollider;

        protected ICircle circleCollider;

        public GameObject(String id = "", int layer = 0)
        {
            this.isVisible = true;
            this.id = id;
            this.layer = layer; 
            this.velocity = Vector2.Zero;
            this.position = Vector2.Zero;
            this.isInView = false;
            this.rectangleCollider = this as IRectangle;
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
            if(GameEnvironment.Camera.IsInView(this))
            {
                this.isInView = true;
            }
            else
            {
                this.isInView = false;
            }
            this.position += (this.velocity * (float) gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public virtual void HandleInput(InputHelper inputHelper)
        {

        }

        public virtual void Reset()
        {
            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
        }

        public bool CollidesWith(GameObject other, bool includeInvisible = false)
        {
            if((!this.isVisible || !other.IsVisible || !this.isInView || !other.IsInView) && !includeInvisible)
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

        public virtual int Width
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
    }
}
