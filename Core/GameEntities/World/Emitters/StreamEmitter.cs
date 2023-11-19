/*using System;
using System.Collections.Generic;
using System.Text;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public class StreamEmitter<T> : Emitter<T> where T : WorldEntity, new()
    {
        private float minForce;
        private float maxForce;


        #region Public Properties

        public float MinForce
        {
            get
            {
                return minForce;
            }
            set
            {
                minForce = value;
            }
        }


        public float MaxForce
        {
            get
            {
                return maxForce;
            }
            set
            {
                maxForce = value;
            }
        }

        #endregion


        #region Constructors

        public StreamEmitter(IWorld world, int delayMSecs, float minForce, float maxForce) : base(world, delayMSecs)
        {
            this.minForce  = minForce;
            this.maxForce  = maxForce;
        }

        #endregion


        #region SetInitialState(obj)

        protected override void SetInitialState(T obj)
        {
            base.SetInitialState(obj);

            
            // Calculate the force

            float force = Interpolation.Linear(minForce, maxForce, (float)GameEngine2D.Randomizer.NextDouble());


            // Point the object in the direction of the emitter and set the initial velocity

            obj.WorldParams.Normal         = this.WorldParams.Normal;
            obj.WorldParams.Velocity.X     = obj.WorldParams.Normal.X * force;
            obj.WorldParams.Velocity.Y     = obj.WorldParams.Normal.Y * force;

        }

        #endregion

    }
}
*/