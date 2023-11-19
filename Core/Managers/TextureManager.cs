using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class TextureManager
    {
        private Dictionary<string, Texture2D> textures;


        #region Properties

        public Dictionary<string, Texture2D> Textures
        {
            get
            {
                return textures;
            }
        }

        #endregion


        public TextureManager()
        {
            textures = new Dictionary<string, Texture2D>();
        }


        #region LoadTexture(name, filename)

        public Texture2D LoadTexture(string name, string filename)
        {
            Texture2D texture = null;

            if (textures.ContainsKey(name.ToUpper()))
                texture = textures[name.ToUpper()];
            else
            {
                texture = Texture2D.FromFile(GameEngine2D.Graphics.GraphicsDevice, System.IO.Path.Combine(GameEngine2D.AssetsPath, filename));
                textures.Add(name.ToUpper(), texture);
            }

            return texture;
        }

        #endregion


        #region UnloadTexture(name)

        public void UnloadTexture(string name)
        {
            if (textures.ContainsKey(name.ToUpper()))
            {
                Texture2D texture = textures[name.ToUpper()];

                texture.Dispose();

                textures.Remove(name.ToUpper());
            }
        }

        #endregion


        #region UnloadAllTextures()

        public void UnloadAllTextures()
        {
            foreach (Texture2D texture in textures.Values)
                texture.Dispose();

            textures.Clear();
        }

        #endregion

    }
}
