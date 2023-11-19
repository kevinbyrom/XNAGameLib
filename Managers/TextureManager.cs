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
        static protected Dictionary<string, Texture2D> m_Textures = new Dictionary<string,Texture2D>();


        #region LoadTexture(name, filename)

        static public Texture2D LoadTexture(string name, string filename)
        {
            Texture2D texture = null;

            if (m_Textures.ContainsKey(name.ToUpper()))
                texture = m_Textures[name.ToUpper()];
            else
            {
                texture = Texture2D.FromFile(GameManager.Graphics.GraphicsDevice, System.IO.Path.Combine(GameManager.AssetsPath, filename));
                m_Textures.Add(name.ToUpper(), texture);
            }

            return texture;
        }

        #endregion


        #region UnloadTexture(name)

        static public void UnloadTexture(string name)
        {
            if (m_Textures.ContainsKey(name.ToUpper()))
            {
                Texture2D texture = m_Textures[name.ToUpper()];

                texture.Dispose();

                m_Textures.Remove(name.ToUpper());
            }
        }

        #endregion


        #region UnloadAllTextures()

        static public void UnloadAllTextures()
        {
            foreach (Texture2D texture in m_Textures.Values)
                texture.Dispose();

            m_Textures.Clear();
        }

        #endregion

    }
}
