using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public abstract class WorldView<TWorld> : ScreenLayer where TWorld : World
    {
        private WorldCamera<TWorld> camera;
        private TWorld world;


        #region Properties

        public WorldCamera Camera
        {
            get { return camera; }
            set { camera = value; }
        }


        public TWorld World
        {
            get { return world; }
            set { world = value; }
        }

        #endregion


        #region Constructors

        public WorldView(Vector2 screenPos, Rectangle screenSize, TWorld world) : base(screenPos, screenSize)
        {
            this.world = world;
        }

        #endregion


        #region Initialize()

        public override void Initialize()
        {
            base.Initialize();

            camera = new WorldCamera();
            camera.Initialize();
        }

        #endregion


        #region WorldToScreen(worldPos)

        public abstract Vector2 WorldToScreen(Vector3 worldPos);

        #endregion

    }
}
