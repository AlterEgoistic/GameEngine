using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine
{
    public class Circle
    {
        /// <summary>
        /// Radius of the circle
        /// </summary>
        private float radius;

        /// <summary>
        /// Center point of the circle
        /// </summary>
        private Vector2 center;

        public Circle()
        {

        }

        /// <summary>
        /// Creates a copy of given circle
        /// </summary>
        /// <param name="toCopy">Circle to copy radius and center of</param>
        public Circle(Circle toCopy)
        {
            this.radius = toCopy.radius;
            this.center = toCopy.center;
        }

        /// <summary>
        /// Creates a new circle from given radius and center point
        /// </summary>
        /// <param name="radius">Radius of the circle</param>
        /// <param name="center">Center point of the circle</param>
        public Circle(float radius, Vector2 center)
        {
            this.radius = radius;
            this.center = center;
        }

        /// <summary>
        /// Whether this is overlapping the given circle or not
        /// </summary>
        /// <param name="other">The circle to check intersection with</param>
        /// <returns>Whether it is intersecting or not</returns>
        public bool Intersects(Circle other)
        {
            Vector2 distance = other.Center - this.center;
            if(distance.X > this.radius || distance.Y > this.radius)
            {
                return false;
            }
            return distance.LengthSquared() < this.radius * this.radius;
        }

        /// <summary>
        /// Whether this is overlapping the given rectangle or not
        /// </summary>
        /// <param name="other">The rectangle to check intersection with</param>
        /// <returns></returns>
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
