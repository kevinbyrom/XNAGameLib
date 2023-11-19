using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public abstract class WorldView<TWorld> : ScreenLayer where TWorld : World
    {
        public TWorld World;
        public WorldCamera<TWorld> Camera;
        

        #region Constructors

        public WorldView(Vector2 screenPos, Vector2 screenSize, TWorld world) : base(screenPos, screenSize)
        {
            World = world;
        }

        #endregion


        #region Initialize()

        public override void Initialize()
        {
            base.Initialize();

            Camera = new WorldCamera<TWorld>(this.World);
            Camera.Initialize();
        }

        #endregion


        #region WorldToScreen(obj)

        public abstract Vector2 WorldToScreen(Vector3 worldPos);

        #endregion

    }
}
