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
    public class LoopingObjectList : Collection<ILoopGameObject>
    {

        protected LoopingObjectList() 
            : base()
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var obj in this)
            {
                obj.Draw(gameTime, spriteBatch);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var obj in this)
            {
                obj.Update(gameTime);
            }
        }

        public virtual void HandleInput(InputHelper inputHelper)
        {
            foreach(var obj in this)
            {
                obj.HandleInput(inputHelper); 
            }
        }

        public virtual void Reset()
        {
            foreach (var obj in this)
            {
                obj.Reset();
            }
        }

        public new void Add(ILoopGameObject objToAdd)
        {
            objToAdd.Parent = this;
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
            /*
            int i = this.Count/2; 
            while(i >= 0 && i <= this.Count - 1)
            {
                if(objToAdd.Layer == this.Items[i].Layer)
                {
                    break;
                }
                if(objToAdd.Layer < this.Items[i].Layer)
                {
                    i /= 2;
                }
                else 
                {
                    i += (this.Count - i);
                }
             }
            this.Insert(i, objToAdd); 
            */
        }

        public ILoopGameObject Find(String id)
        {
            foreach (var obj in this)
            {
                if(obj.ID == id)
                {
                    return obj;
                }
                if(obj is GameObjectList)
                {
                    GameObjectList objList = obj as GameObjectList;
                    ILoopGameObject subObj = objList.Find(id);
                    if(subObj != null)
                    {
                        return subObj;
                    }
                }
                
            }
            throw new Exception("GameID not found");
        }
    }
}
