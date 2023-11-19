using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;


namespace XNAGameLib2D
{
    public class GameEngine2D : Microsoft.Xna.Framework.Game
    {
        static public GraphicsDeviceManager Graphics        = null;
        static public ContentManager Content                = null;
        static public TextureManager TextureManager         = new TextureManager();
        static public string AssetsPath                     = "";
        static public Stack<RenderTarget2D> RenderTargets   = new Stack<RenderTarget2D>();
        static public SpriteFont DebugFont                  = null;
        static public Stack<Stage> Stages                   = new Stack<Stage>();
        static public Random Randomizer = new Random();


        #region Properties

        static public int ScreenWidth
        {
            get
            {
                return Graphics.PreferredBackBufferWidth; 
            }
        }


        static public int ScreenHeight
        {
            get
            {
                return Graphics.PreferredBackBufferHeight; 
            }
        }

        #endregion


        #region SetScreenSize(width, height, fullScreen)

        static public void SetScreenSize(int width, int height, bool fullScreen)
        {
            Graphics.PreferredBackBufferWidth   = width;
            Graphics.PreferredBackBufferHeight  = height;
            Graphics.IsFullScreen               = fullScreen;
            Graphics.ApplyChanges();
        }

        #endregion

    }
}
