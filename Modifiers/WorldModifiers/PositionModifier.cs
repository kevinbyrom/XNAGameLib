using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D.Modifiers.WorldModifiers
{
    public class PositionModifier : Modifier<WorldObjParams>
    {
        protected Vector3 m_Delta;
        protected float m_Speed;


        public PositionModifier(Vector3 delta, float speed, float maxlife) : base(maxlife)
        {
            m_Delta = delta;
            m_Speed = speed;
        }


        public override void Update(WorldObjParams objparams, GameTime gametime)
        {
            float elapsed = (float)gametime.ElapsedGameTime.Milliseconds / 1000;


            base.Update(objparams, gametime);


            // Update the position

            objparams.Position.X += (m_Delta.X * m_Speed * elapsed);
            objparams.Position.Y += (m_Delta.Y * m_Speed * elapsed);
            objparams.Position.Z += (m_Delta.Z * m_Speed * elapsed);

        }
    }
}
