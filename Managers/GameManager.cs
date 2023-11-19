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
    public class GameManager
    {
        static public GraphicsDeviceManager Graphics        = null;
        static public ContentManager Content                = null;
        static public string AssetsPath                     = "";
        static public Stack<RenderTarget2D> RenderTargets   = new Stack<RenderTarget2D>();
        static public SpriteFont DebugFont                  = null;
        static public Stack<Stage> Stages                   = new Stack<Stage>();
        


        static public void PushRenderTarget(RenderTarget2D target)
        {
            RenderTargets.Push((RenderTarget2D)Graphics.GraphicsDevice.GetRenderTarget(0));
            Graphics.GraphicsDevice.SetRenderTarget(0, target);
            Graphics.GraphicsDevice.ResolveRenderTarget(0);
        }

        static public void PopRenderTarget()
        {
            
            // Resolve the current target

            Graphics.GraphicsDevice.ResolveRenderTarget(0);


            // Pop the previous render target

            RenderTarget2D prevtarget = null;

            if (RenderTargets.Count > 0)
                prevtarget = RenderTargets.Pop();

            Graphics.GraphicsDevice.SetRenderTarget(0, prevtarget);
        }
    }
}
