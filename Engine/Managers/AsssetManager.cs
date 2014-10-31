#region Using statements
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Engine
{
    public sealed class AssetManager
    {
        private ContentManager content;

        public AssetManager(ContentManager content)
        {
            this.content = content; 
        }

        /// <summary>
        /// Loads an asset of given type
        /// </summary>
        /// <typeparam name="T">The kind of asset to load</typeparam>
        /// <param name="assetName">The name of the asset to load (include the file extension for non xnb files)</param>
        /// <returns>The asset of given type</returns>
        public T GetAsset<T>(String assetName)
        {
            T asset = default(T);
            try
            {
                asset = this.content.Load<T>(assetName);
            }
            catch(ContentLoadException e)
            {
                e = new ContentLoadException("The asset is not set to copy always or is not marked as content file (file properties)");
                throw e;
            }
            return asset;
        }

        /// <summary>
        /// Loads a sound effect and plays it straight away
        /// </summary>
        /// <param name="assetName">The name of the soundeffect to play (include the file extension for non xnb files)</param>
        public void PlaySoundEffect(String assetName)
        {
            SoundEffect sound = this.content.Load<SoundEffect>(assetName);
            sound.Play();
        }

        /// <summary>
        /// Plays a music on the background using the MediaPlayer class
        /// </summary>
        /// <param name="assetName">The name of the music file</param>
        /// <param name="isRepeating">Whether the music should repeat after ending</param>
        public void PlayBackgroundMusic(String assetName, bool isRepeating = true)
        {
            MediaPlayer.IsRepeating = isRepeating;
            MediaPlayer.Play(this.content.Load<Song>(assetName));
        }
    }
}
