using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public enum PathRepeatMode
    {
        RunOnce,
        Repeat
    }

    public enum PathInterpolation
    {
        Direct,
        Linear,
        Catmull,
        Sine
    }

    public class Path
    {
        private List<Vector4> pathNodes;
        private float currNodePos;
        private PathRepeatMode repeatMode;
        private PathInterpolation pathInterpolation;


        #region Properties

        public List<Vector4> PathNodes
        {
            get
            {
                return pathNodes;
            }
            set
            {
                pathNodes = value;
            }
        }


        public float CurrNodePos
        {
            get
            {
                return currNodePos;
            }
            set
            {
                currNodePos = value;
            }
        }


        public PathRepeatMode RepeatMode
        {
            get
            {
                return repeatMode;
            }
            set
            {
                repeatMode = value;
            }
        }


        public PathInterpolation PathInterpolation
        {
            get
            {
                return pathInterpolation;
            }
            set
            {
                pathInterpolation = value;
            }
        }


        public Vector4 CurrVector
        {
            get
            {
                return GetVectorByInterpolation();
            }
        }

        #endregion


        #region Advance Routines

        public void Advance()
        {
            currNodePos += 1;
        }

        public void Advance(float distance)
        {
            currNodePos += distance;
        }

        #endregion


        #region GetVector Routines

        protected Vector4 GetVectorByInterpolation()
        {
            switch(pathInterpolation)
            {
                case PathInterpolation.Linear:
                    return GetVectorLinear();

                case PathInterpolation.Catmull:
                    return GetVectorCatmull();

                case PathInterpolation.Sine:
                    return GetVectorSine();

                default:
                    return GetVectorDirect();
            }
        }


        protected Vector4 GetVectorDirect()
        {
            return GetVector((int)currNodePos);
        }


        protected Vector4 GetVectorLinear()
        {
            Vector4 vector1 = GetVector((int)currNodePos);
            Vector4 vector2 = GetVector((int)currNodePos + 1);
            float percent   = currNodePos - (int)currNodePos;

            return new Vector4(Interpolation.Linear(vector1.X, vector2.X, percent),
                               Interpolation.Linear(vector1.Y, vector2.Y, percent),
                               Interpolation.Linear(vector1.Z, vector2.Z, percent),
                               Interpolation.Linear(vector1.W, vector2.W, percent));
        }


        protected Vector4 GetVectorCatmull()
        {
            return Vector4.Zero;
        }


        protected Vector4 GetVectorSine()
        {
            Vector4 vector1 = GetVector((int)currNodePos);
            Vector4 vector2 = GetVector((int)currNodePos + 1);
            float percent   = currNodePos - (int)currNodePos;

            return new Vector4(Interpolation.Sine(vector1.X, vector2.X, percent),
                               Interpolation.Sine(vector1.Y, vector2.Y, percent),
                               Interpolation.Sine(vector1.Z, vector2.Z, percent),
                               Interpolation.Sine(vector1.W, vector2.W, percent));
        }


        protected Vector4 GetVector(int pos)
        {

            // Make sure the position is in range

            if (pos < 0)
                pos = (repeatMode == PathRepeatMode.Repeat ?  pathNodes.Count + pos : 0);
            
            if (pos >= pathNodes.Count)
                pos = (repeatMode == PathRepeatMode.Repeat ?  0 + (pos - pathNodes.Count) : pathNodes.Count - 1);


            // Return the vector at the specified position
            
            return pathNodes[pos];
        }

        #endregion

    }
}
