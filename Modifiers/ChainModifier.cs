using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D.Modifiers
{
    public enum ChainModRepeatMode
    {
        RunOnce,
        Repeat
    }

    public class ChainModifier<T> : Modifier<T>
    {
        protected Modifier<T>[] m_Modifiers;
        protected int m_CurrPos;
        protected ChainModRepeatMode m_RepeatMode;


        public ChainModifier(Modifier<T>[] modifiers, ChainModRepeatMode repeatmode, float maxlife) : base(maxlife)
        {
            m_Modifiers     = modifiers;
            m_CurrPos       = 0;
            m_RepeatMode    = repeatmode;
        }


        #region Update(objparams, gametime)

        public override void Update(T objparams, GameTime gametime)
        {

            // Update the current modifier

            m_Modifiers[m_CurrPos].Update(objparams, gametime);


            // If the current automator is done, move on to the next one

            if (m_Modifiers[m_CurrPos].IsDone)
            {
                if (m_CurrPos >= m_Modifiers.Length - 1)
                {
                    if (m_RepeatMode == ChainModRepeatMode.Repeat)
                    {
                        m_CurrPos = 0;
                        m_Modifiers[m_CurrPos].Restart();
                    }
                    else
                    {
                        m_IsDone = true;
                    }
                }
                else
                {
                    m_CurrPos++;
                    m_Modifiers[m_CurrPos].Restart();
                }
            }
        }

        #endregion

    }
}
