using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine
{
    internal static class RectangleExtensions
    {
        /// <summary>
        /// Whether this is overlapping the given circle or not
        /// </summary>
        /// <param name="other">The circle to check intersection with</param>
        /// <returns>Whether it is intersecting or not</returns>
        public static bool Intersects(this Rectangle self, Circle other)
        {
            Vector2 distance = new Vector2(other.Center.X - MathHelper.Clamp((int) other.Center.X, self.Left, self.Right),
                                            other.Center.Y - MathHelper.Clamp((int) other.Center.Y, self.Top, self.Bottom));
            //Since the distance is always bigger than the individual distanceX and distanceY (pythagoras), return false if 
            //either is larger than the radius
            if(distance.X > other.Radius || distance.Y > other.Radius)
            {
                return false;
            }
            return distance.LengthSquared() <= (other.Radius * other.Radius);
        }
    }
}
