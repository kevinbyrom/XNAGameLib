using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D
{
    public class World : GameEntity
    {
        private float width;
        private float height;
        

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

        #endregion


        #region Constructors

        public World(float width, float height)
        {        
            this.width     = width;
            this.height    = height;
        }

        #endregion

    }
}
