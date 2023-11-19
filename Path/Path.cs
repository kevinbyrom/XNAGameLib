using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D.Path
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
        protected List<Vector4> m_PathNodes;
        protected float m_CurrPos;
        protected PathRepeatMode m_RepeatMode;
        protected PathInterpolation m_Interpolation;


        public Vector4 CurrVector
        {
            get
            {
                return GetVectorByInterpolation();
            }
        }


        public void Advance()
        {
            m_CurrPos += 1;
        }

        public void Advance(float distance)
        {
            m_CurrPos += distance;
        }


        protected Vector4 GetVectorByInterpolation()
        {
            switch(m_Interpolation)
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
            return GetVector((int)m_CurrPos);
        }


        protected Vector4 GetVectorLinear()
        {
            Vector4 vector1 = GetVector((int)m_CurrPos);
            Vector4 vector2 = GetVector((int)m_CurrPos + 1);
            float percent   = m_CurrPos - (int)m_CurrPos;

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
            Vector4 vector1 = GetVector((int)m_CurrPos);
            Vector4 vector2 = GetVector((int)m_CurrPos + 1);
            float percent   = m_CurrPos - (int)m_CurrPos;

            return new Vector4(Interpolation.Sine(vector1.X, vector2.X, percent),
                               Interpolation.Sine(vector1.Y, vector2.Y, percent),
                               Interpolation.Sine(vector1.Z, vector2.Z, percent),
                               Interpolation.Sine(vector1.W, vector2.W, percent));
        }


        protected Vector4 GetVector(int pos)
        {

            // Make sure the position is in range

            if (pos < 0)
                pos = (m_RepeatMode == PathRepeatMode.Repeat ?  m_PathNodes.Count + pos : 0);
            
            if (pos >= m_PathNodes.Count)
                pos = (m_RepeatMode == PathRepeatMode.Repeat ?  0 + (pos - m_PathNodes.Count) : m_PathNodes.Count - 1);


            // Return the vector at the specified position
            
            return m_PathNodes[pos];
        }
    }
}
