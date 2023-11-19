using System;
using System.Collections.Generic;
using System.Text;

namespace XNAGameLib2D
{
    public class ConeEmitter<T> : Emitter<T> where T : WorldObject 
    {
        protected Random m_Random;
        protected float m_MinForce;
        protected float m_MaxForce;
        protected float m_Angle;


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


        public float Angle
        {
            get
            {
                return m_Angle;
            }
            set
            {
                m_Angle = value;
            }
        }

        #endregion


        #region Constructors

        public ConeEmitter(World world, int delaymsecs, float minforce, float maxforce, float angle) : base(world, delaymsecs)
        {
            m_MinForce  = minforce;
            m_MaxForce  = maxforce;
            m_Random    = new Random();
            m_Angle     = angle;
        }

        #endregion


        #region SetInitialState(obj)

        protected override void SetInitialState(WorldObject obj)
        {
            base.SetInitialState(obj);

            
            // Calculate the angle and force for this object

            float angle = ((float)m_Random.NextDouble() * m_Angle) - (m_Angle / 2);
            float force = Interpolation.Linear(m_MinForce, m_MaxForce, (float)m_Random.NextDouble());


            // Point the object in the direction of the emitter and set the initial velocity

            obj.WorldParams.Normal         = TrigHelper.RotateVector(m_WorldParams.Normal, angle);
            obj.WorldParams.Velocity.X     = obj.WorldParams.Normal.X * force;
            obj.WorldParams.Velocity.Y     = obj.WorldParams.Normal.Y * force;

        }

        #endregion

    }
}
