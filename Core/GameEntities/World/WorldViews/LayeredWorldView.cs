using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class LayeredWorldView<TWorld> : WorldView<TWorld> where TWorld : World
    {
        private Vector2 origin;
        private float farClip;
        private int halfScreenWidth;
        private int halfScreenHeight;


        #region Properties

        public float FarClip
        {
            get { return farClip; }
            set { farClip = value; }
        }

        #endregion


        #region Constructors

        public LayeredWorldView(Vector2 screenPos, Rectangle screenSize, TWorld world) : base(screenPos, screenSize, world)
        {
            farClip = 1;
        }

        #endregion


        #region Initialize()

        public override void Initialize()
        {
            base.Initialize();

            origin              = new Vector2(this.World.Width / 2, World.Height / 2);
            halfScreenWidth     = this.ScreenSize.Width / 2;
            halfScreenHeight    = this.ScreenSize.Height / 2;
        }

        #endregion


        #region WorldToScreen(obj)

        public override Vector2 WorldToScreen(Vector3 worldPos)
        {
            Vector2 screenPos = new Vector2();

            screenPos.X = ((worldPos.X - MathHelper.Lerp(origin.X, this.Camera.WorldPos.X, ((farClip - worldPos.Z) / farClip))) * this.Camera.Zoom) + (halfScreenWidth);
            screenPos.Y = ((worldPos.Y - MathHelper.Lerp(origin.Y, this.Camera.WorldPos.Y, ((farClip - worldPos.Z) / farClip))) * this.Camera.Zoom) + (halfScreenHeight);
  
            return screenPos;
        }

        #endregion

    }
}
