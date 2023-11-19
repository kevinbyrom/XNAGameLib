using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D.Core
{
    public enum CompoundModRepeatMode
    {
        RunOnce,
        Repeat
    }

    public class CompoundModifier<T> : Modifier<T>
    {
        protected Modifier<T>[] m_Modifiers;
        protected ChainModRepeatMode m_RepeatMode;


        public CompoundModifier(Modifier<T>[] modifiers, ChainModRepeatMode repeatmode, float maxlife) : base(maxlife)
        {
            m_Modifiers     = modifiers;
            m_RepeatMode    = repeatmode;
        }


        #region Update(objparams, gametime)

        public override void Update(T objparams, GameTime gametime)
        {
            bool alldone = true;


            // Update the current modifier

            foreach (Modifier<T> modifier in m_Modifiers)
            {
                modifier.Update(objparams, gametime);

                if (!modifier.IsDone)                               
                    alldone = false;
                else if (m_RepeatMode == ChainModRepeatMode.Repeat)
                    modifier.Restart();
            }


            // If all modifiers are done and we are in RunOnce mode, set the IsDone flag

            if (alldone && m_RepeatMode == ChainModRepeatMode.RunOnce)
                m_IsDone = true;
        }

        #endregion

    }
}
