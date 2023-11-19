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
    public class GraphicsManager
    {
        protected Stack<Viewport> viewportStack;

        public GraphicsDeviceManager Graphics { get; set; }
        public ContentManager Content { get; set; }
        public SpriteFont DebugFont                  = null;


        #region Properties

        public int ScreenWidth
        {
            get
            {
                return Graphics.PreferredBackBufferWidth; 
            }
        }


        public int ScreenHeight
        {
            get
            {
                return Graphics.PreferredBackBufferHeight; 
            }
        }


        public Viewport CurrentViewport
        {
            get
            {
                return viewportStack.Peek();
            }
        }

        #endregion


        public GraphicsManager()
        {
        }


        #region Screen Size Routines

        public void SetScreenSize(int width, int height, bool fullScreen)
        {
            this.Graphics.PreferredBackBufferWidth   = width;
            this.Graphics.PreferredBackBufferHeight  = height;
            this.Graphics.IsFullScreen               = fullScreen;
            this.Graphics.ApplyChanges();
        }


        public void ToggleFullscreen()
        {
            this.Graphics.IsFullScreen = !this.Graphics.IsFullScreen;
            this.Graphics.ApplyChanges();
        }

        #endregion


        #region Viewport Routines

        public void PushViewport(Viewport viewport)
        {
            this.Graphics.GraphicsDevice.Viewport = viewport;
            this.viewportStack.Push(viewport);
        }

        public void PopViewport()
        {            
            this.viewportStack.Pop();

            if (this.viewportStack.Count > 0)
                this.Graphics.GraphicsDevice.Viewport = this.viewportStack.Peek();
            else
                this.Graphics.GraphicsDevice.Viewport = null;
        }

        #endregion

    }
}
