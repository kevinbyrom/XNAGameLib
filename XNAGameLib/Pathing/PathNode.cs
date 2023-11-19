using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class PathNode
    {
        private Vector4 vector;
        private float length;


        #region Public Properties

        public Vector4 Vector
        {
            get
            {
                return vector;
            }
            set
            {
                vector = value;
            }
        }


        public float Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        #endregion


        public PathNode()
        {

        }


        public PathNode(Vector4 vector, float length)
        {
            this.vector = vector;
            this.length = length;
        }

    }
}
