#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
#endregion

namespace Engine
{
    public abstract class LoopingObjectList : Collection<ILoopGameObject>
    {
        /// <summary>
        /// Create a new List of Objects that have a draw, update and handleinput method
        /// </summary>
        public LoopingObjectList() 
            : base()
        {

        }

        /// <summary>
        /// Draws all the objects in this list
        /// </summary>
        /// <param name="gameTime">Information about the times in the game</param>
        /// <param name="spriteBatch">The spritebatch to draw on</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var obj in this)
            {
                obj.Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// Updates all the object in this list
        /// </summary>
        /// <param name="gameTime">Information about the times in the game</param>
        public virtual void Update(GameTime gameTime)
        {
            foreach (var obj in this)
            {
                obj.Update(gameTime);
            }
        }

        /// <summary>
        /// Calls fixed update on all objects in the last every set frame
        /// </summary>
        /// <param name="gameTime"> INformaton about the times in the game</param>
        public virtual void FixedUpdate(GameTime gameTime)
        {
            foreach(var obj in this)
            {
                obj.FixedUpdate(gameTime);
            }
        }

        /// <summary>
        /// Handles input on all the objects within this list
        /// </summary>
        /// <param name="inputHelper">The inputhelper to check the input with</param>
        public virtual void HandleInput(InputHelper inputHelper)
        {
            foreach(var obj in this)
            {
                obj.HandleInput(inputHelper); 
            }
        }

        /// <summary>
        /// Resets all the objects in this list to their original state
        /// </summary>
        public virtual void Reset()
        {
            foreach (var obj in this)
            {
                obj.Reset();
            }
        }

        /// <summary>
        /// Adds a new GameObject or GameObjectList to this list and sorts the list on layer
        /// </summary>
        /// <param name="objToAdd">The GameObject or GameObjectList to add</param>
        public new void Add(ILoopGameObject objToAdd)
        {
            objToAdd.Parent = this;
            for(int i = 0; i < this.Count - 1; i++)
            {
                if(this.Items[i].Layer > objToAdd.Layer)
                {
                    this.Insert(i, objToAdd);
                    return;
                }
            }
            base.Add(objToAdd);
        }

        /// <summary>
        /// Looks up a GameObject in the list and children of the lists in the list if appicable
        /// </summary>
        /// <param name="id">The name of the GameObject(List) to look up</param>
        /// <returns>The GameObject(List) searched for</returns>
        public ILoopGameObject Find(String id, bool logErrors = true)
        {
            if(id == null || id == "")
            {
                throw new ArgumentException("Invalid argument, id cannot be null or empty");
            }
            foreach (ILoopGameObject obj in this)
            {
                if(obj.ID == id)
                {
                    return obj;
                }
                if(obj is GameObjectList)
                {
                    GameObjectList objList = obj as GameObjectList;
                    ILoopGameObject subObj = objList.Find(id, false);
                    if(subObj != null)
                    {
                        return subObj;
                    }
                }
            }
            if(logErrors)
            {
                Console.Error.WriteLine("No GameObject was found with the id " + id);
            }
            return null;
        }


        public ILoopGameObject[] FindAll(String id, bool logErrors = true)
        {
            if(id == null || id == "")
            {
                throw new ArgumentException("Invalid argument, id cannot be null or empty");
            }
            GameObjectList foundGameObjects = new GameObjectList();

            foreach(ILoopGameObject obj in this)
            {
                if(obj.ID == id)
                {
                    foundGameObjects.Add(obj);
                }
                if(obj is GameObjectList)
                {
                    GameObjectList objList = obj as GameObjectList;
                    ILoopGameObject[] children = objList.FindAll(id, false);
                    if(children != null)
                    {
                        foreach(GameObject child in children)
                        {
                            foundGameObjects.Add(child);
                        }
                    }
                }
            }
            if(logErrors && foundGameObjects.Count == 0)
            {
                Console.Error.WriteLine("No GameObject was found with the id " + id);
            }
            return foundGameObjects.ToArray();
        }
    }
}
