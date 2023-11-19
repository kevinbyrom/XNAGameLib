using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class TextureManager
    {

        #region Instance

        static public SingleInstance<AssetsManager> Instance = new SingleInstance<AssetsManager>();

        #endregion


        private NamedCache<Texture2D> textureCache;
        

        public TextureManager()
        {
            textureCache = new NamedCache<Texture2D>();
        }



        /// <summary>
        /// Loads a texture
        /// </summary>
        /// <param name="name">Name of the texture</param>
        /// <param name="filename">Filename of the texture</param>
        /// <returns>Returns the loaded texture</returns>
        
        public Texture2D LoadTexture(string name, string filename)
        {
            Texture2D texture = null;

            if (textureCache.Contains(name))
                texture = textureCache.Get(name);
            else
            {
                texture = Texture2D.FromFile(GameEngine2D.Graphics.GraphicsDevice, AssetsManager.Instance.Current.GetAssetFilename(filename));
                textureCache.Add(name, texture);
            }

            return texture;
        }


        /// <summary>
        /// Unloads a cached texture
        /// </summary>
        /// <param name="name">Name of the texture to unload</param>
        
        public void UnloadTexture(string name)
        {            
            if (this.textureCache.Contains(name))
            {
                Texture2D texture = textureCache.Get(name);

                texture.Dispose();

                textureCache.Remove(name);
            }
        }


        
        /// <summary>
        /// Unloads all cached textures
        /// </summary>
        
        public void UnloadAllTextures()
        {
            foreach (string name in textures.Keys)
                UnloadTexture(name);

            textureCache.RemoveAll();
        }

        
    }
}
