using System;
using System.Collections.Generic;
using System.Text;

namespace XNAGameLib2D
{
    public class StreamEmitter<T> : Emitter<T> where T : WorldObject
    {
        protected Random m_Random;
        protected float m_MinForce;
        protected float m_MaxForce;


        #region Public Properties

        public float MinForce
        {
            get
            {
                return m_MinForce;
            }
            set
            {
                m_MinForce = value;
            }
        }


        public float MaxForce
        {
            get
            {
                return m_MaxForce;
            }
            set
            {
                m_MaxForce = value;
            }
        }

        #endregion


        #region Constructors

        public StreamEmitter(World world, int delaymsecs, float minforce, float maxforce) : base(world, delaymsecs)
        {
            m_MinForce  = minforce;
            m_MaxForce  = maxforce;
            m_Random    = new Random();
        }

        #endregion


        #region SetInitialState(obj)

        protected override void SetInitialState(WorldObject obj)
        {
            base.SetInitialState(obj);

            
            // Calculate the force

            float force = Interpolation.Linear(m_MinForce, m_MaxForce, (float)m_Random.NextDouble());


            // Point the object in the direction of the emitter and set the initial velocity

            obj.WorldParams.Normal         = m_WorldParams.Normal;
            obj.WorldParams.Velocity.X     = obj.WorldParams.Normal.X * force;
            obj.WorldParams.Velocity.Y     = obj.WorldParams.Normal.Y * force;

        }

        #endregion

    }
}
