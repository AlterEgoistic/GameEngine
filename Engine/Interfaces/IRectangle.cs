using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine
{
    public interface IRectangle
    {
        public Rectangle BoundingBox
        {
            get;
            set;
        }
    }
}
