/*using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;


namespace XNAGameLib2D.Core.Engine
{
    public static class Graphics
    {
        static public GraphicsDeviceManager GraphicsDevice  = null;
        static public ContentManager Content                = null;
        static public TextureManager TextureManager         = new TextureManager();
        static public string AssetsPath                     = "";
        static public Stack<RenderTarget2D> RenderTargets   = new Stack<RenderTarget2D>();
        static public SpriteFont DebugFont                  = null;
        

        #region Render Target Routines

        static public void PushRenderTarget(RenderTarget2D target)
        {
           RenderTargets.Push((RenderTarget2D)Graphics.GraphicsDevice.GetRenderTarget(0));
           GraphicsDevice.SetRenderTarget(0, target);
           GraphicsDevice.ResolveRenderTarget(0);
        }


        static public void PopRenderTarget()
        {
            
            // Resolve the current target

            GraphicsDevice.ResolveRenderTarget(0);


            // Pop the previous render target

            RenderTarget2D prevtarget = null;

            if (RenderTargets.Count > 0)
                prevtarget = RenderTargets.Pop();

            GraphicsDevice.SetRenderTarget(0, prevtarget);
        }

        #endregion

    }
}
*/