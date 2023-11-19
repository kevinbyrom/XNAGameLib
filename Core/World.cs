using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D
{
    public class World : IWorld
    {
        private float width;
        private float height;
        private WorldCamera camera;


        #region Public Properties

        public float Width
        {
            get
            {
                return width;
            }
        }


        public float Height
        {
            get
            {
                return height;
            }
        }


        public WorldCamera Camera
        {
            get
            {
                return camera;
            }
            set
            {
                camera = value;
            }
        }

        #endregion


        #region Constructors

        public World(float width, float height)
        {        
            this.width     = width;
            this.height    = height;
            this.camera    = new WorldCamera(this);
        }

        #endregion

    }
}
