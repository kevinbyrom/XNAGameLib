using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D.Modifiers.WorldModifiers
{
    public class ScaleModifier : Modifier<WorldObjParams>
    {
        protected float m_Delta;
        protected float m_Speed;


        public ScaleModifier(float delta, float speed, float maxlife) : base(maxlife)
        {
            m_Delta = delta;
            m_Speed = speed;
        }


        public override void Update(WorldObjParams objparams, GameTime gametime)
        {
            float elapsed = (float)gametime.ElapsedGameTime.Milliseconds / 1000;


            base.Update(objparams, gametime);


            // Update the scale

            objparams.Scale += m_Delta * m_Speed * elapsed;
        }
    }
}
