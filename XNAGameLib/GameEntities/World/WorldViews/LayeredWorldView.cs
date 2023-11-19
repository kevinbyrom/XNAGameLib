using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class LayeredWorldView<TWorld> : WorldView<TWorld> where TWorld : World
    {
        public float FarClip;
        
        private Vector2 origin;
        private int halfScreenWidth;
        private int halfScreenHeight;


        #region Constructors

        public LayeredWorldView(Vector2 screenPos, Vector2 screenSize, TWorld world, float farClip) : base(screenPos, screenSize, world)
        {
            FarClip = farClip;
        }

        #endregion


        #region Initialize()

        public override void Initialize()
        {
            base.Initialize();

            origin              = new Vector2(World.Width / 2, World.Height / 2);
            halfScreenWidth     = (int)this.ScreenSize.X / 2;
            halfScreenHeight    = (int)this.ScreenSize.Y / 2;
        }

        #endregion


        #region WorldToScreen(obj)

        public override Vector2 WorldToScreen(Vector3 worldPos)
        {
            Vector2 screenPos = new Vector2();

            // Objects with Z values that match the Far clip will not move at all and stay in view
        
            float zPercent = (FarClip - worldPos.Z) / FarClip;
            
            screenPos.X = ((worldPos.X - MathHelper.Lerp(origin.X, Camera.WorldPos.X, zPercent)) * Camera.Zoom) + (halfScreenWidth);
            screenPos.Y = ((worldPos.Y - MathHelper.Lerp(origin.Y, Camera.WorldPos.Y, zPercent)) * Camera.Zoom) + (halfScreenHeight);
  
            return screenPos;
        }

        #endregion

    }
}
