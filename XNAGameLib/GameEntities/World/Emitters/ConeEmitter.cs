/*using System;
using System.Collections.Generic;
using System.Text;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public class ConeEmitter<T> : Emitter<T> where T : WorldEntity, new()
    {
        private float minForce;
        private float maxForce;
        private float angle;


        #region Properties

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


        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
            }
        }

        #endregion


        #region Constructors

        public ConeEmitter(IWorld world, int delayMSecs, float minForce, float maxForce, float angle) : base(world, delayMSecs)
        {
            this.minForce  = minForce;
            this.maxForce  = maxForce;
            this.angle     = angle;
        }

        #endregion


        #region SetInitialState(obj)

        protected override void SetInitialState(T obj)
        {
            base.SetInitialState(obj);

            
            // Calculate the angle and force for this object

            float angle = ((float)GameEngine2D.Randomizer.NextDouble() * this.angle) - (this.angle / 2);
            float force = Interpolation.Linear(minForce, maxForce, (float)GameEngine2D.Randomizer.NextDouble());


            // Point the object in the direction of the emitter and set the initial velocity

            obj.WorldParams.Normal         = TrigHelper.RotateVector2(WorldParams.Normal, angle);
            obj.WorldParams.Velocity.X     = obj.WorldParams.Normal.X * force;
            obj.WorldParams.Velocity.Y     = obj.WorldParams.Normal.Y * force;

        }

        #endregion

    }
}
*/