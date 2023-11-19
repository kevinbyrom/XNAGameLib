using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D
{
    public class PositionAutomator : Automator<WorldEntity>
    {
        private Vector3 delta;
        private float speed;


        #region Properties
        
        public Vector3 Delta
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


        public PositionAutomator(Vector3 delta, float speed, float maxLifeMsecs) : base(maxLifeMsecs)
        {
            this.delta = delta;
            this.speed = speed;
        }


        #region override Apply(entity, gameTime)

        public override void Apply(WorldEntity entity, GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.Milliseconds / 1000;


            base.Apply(entity, gameTime);


            // Update the position

            entity.Position.X += (this.delta.X * this.speed * elapsed);
            entity.Position.Y += (this.delta.Y * this.speed * elapsed);
            entity.Position.Z += (this.delta.Z * this.speed * elapsed);

        }

        #endregion

    }
}
