using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public interface ICircle
    {
        /// <summary>
        /// The circle that is used to check collision with
        /// </summary>
        Circle BoundingCircle
        {
            get;
            set;
        }
    }
}
