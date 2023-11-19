using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D
{
    public class World
    {
        static public readonly float DefaultFriction = .01f;

        protected float m_Width;
        protected float m_Height;
        protected WorldCamera m_Camera;


        #region Public Properties

        public float Width
        {
            get
            {
                return m_Width;
            }
        }


        public float Height
        {
            get
            {
                return m_Height;
            }
        }


        public WorldCamera Camera
        {
            get
            {
                return m_Camera;
            }
        }

        #endregion


        #region Constructors

        public World(float width, float height)
        {        
            m_Width     = width;
            m_Height    = height;
            m_Camera    = new WorldCamera(this);
        }

        #endregion

    }
}
