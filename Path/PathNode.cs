using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D.Path
{
    public class PathNode
    {
        protected Vector4 m_Vector;
        protected float m_Length;


        #region Public Properties

        public Vector4 Vector
        {
            get
            {
                return m_Vector;
            }
            set
            {
                m_Vector = value;
            }
        }

        #endregion


        public PathNode()
        {

        }


        public PathNode(Vector4 vector, float length)
        {
            m_Vector = vector;
            m_Length = length;
        }
    }
}
