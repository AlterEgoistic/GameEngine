using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine
{
    public class Circle
    {
        private float radius;

        private Vector2 center;

        public Circle()
        {

        }

        public Circle(Circle toCopy)
        {
            this.radius = toCopy.radius;
            this.center = toCopy.center;
        }

        public Circle(float radius, Vector2 center)
        {
            this.radius = radius;
            this.center = center;
        }

        public bool Intersects(Circle other)
        {
            Vector2 distance = other.Center - this.center;
            if(distance.X > this.radius || distance.Y > this.radius)
            {
                return false;
            }
            return distance.LengthSquared() < this.radius * this.radius;
        }

        public bool Intersects(Rectangle other)
        {
            Vector2 distance = new Vector2(this.center.X - MathHelper.Clamp((int) this.center.X, other.Left, other.Right), 
                                            this.center.Y - MathHelper.Clamp((int) this.center.Y, other.Top, other.Bottom));

            //Since the distance is always bigger than the individual distanceX and distanceY (pythagoras), return false if 
            //either is larger than the radius
            if(distance.X > this.radius || distance.Y > this.radius)
            {
                return false;
            }
            return distance.LengthSquared() <= (this.radius * this.radius);
        }

        public float Radius
        {
            get { return this.radius; }
        }

        public Vector2 Center
        {
            get { return this.center; }
        }
    }
}
