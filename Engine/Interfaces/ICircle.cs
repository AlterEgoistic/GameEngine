using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public interface ICircle
    {
        public Circle BoundingCircle
        {
            get;
            set;
        }
    }
}
