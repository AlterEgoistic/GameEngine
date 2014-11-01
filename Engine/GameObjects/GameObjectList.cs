#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Engine
{
    public class GameObjectList : LoopingObjectList, ILoopGameObject
    {
        /// <summary>
        /// THe layer of the objects in this list to be drawn on
        /// </summary>
        protected int layer;

        /// <summary>
        /// The name the objectlist can be identified with
        /// </summary>
        protected String id;

        /// <summary>
        /// The objectlist or gamestate that is the direct parent of this
        /// </summary>
        protected LoopingObjectList parent;

        public GameObjectList(String id = "", int layer = 0)
        {
            this.id = id;
            this.layer = layer; 
        }

        public int Layer
        {
            get { return this.layer; }
        }

        public String ID
        {
            get { return this.id; }
        }

        public LoopingObjectList Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        /// <summary>
        /// Gets if all objects in the list are visible or not
        /// Sets visibility of all objects in this list
        /// </summary>
        public bool IsVisible
        {
            get
            {
                foreach(ILoopGameObject gameObj in this)
                {
                    if(!gameObj.IsVisible)
                    {
                        return false;
                    }
                }
                return true;
            }
            set
            {
                foreach(ILoopGameObject gameObj in this)
                {
                    gameObj.IsVisible = value;
                }
            }
        }
    }
}
