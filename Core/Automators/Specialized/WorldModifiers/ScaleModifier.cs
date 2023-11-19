using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D
{
    public class ScaleAutomator : Automator<WorldEntityParams>
    {
        private float delta;
        private float speed;

        
        #region Properties
        
        public float Delta
        {
            get
            {
                return delta;
            }
            protected set
            {
                delta = value;
            }
        }


        public float Speed
        {
            get
            {
                return speed;
            }
            protected set
            {
                speed = value;
            }
        }

        #endregion


        public ScaleAutomator(float delta, float speed, float maxLifeMsecs) : base(maxLifeMsecs)
        {
            this.delta = delta;
            this.speed = speed;
        }


        #region override Apply(objParams, gameTime)

        public override void Apply(WorldEntityParams objParams, GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;


            base.Apply(objParams, gameTime);


            // Update the scale

            objParams.Scale += this.delta * this.speed * elapsed;

        }

        #endregion

    }
}
