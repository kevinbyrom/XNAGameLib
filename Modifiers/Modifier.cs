using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D.Modifiers
{
    public class Modifier<T>
    {
        protected bool m_IsDone;
        protected float m_TimeAliveMsecs;
        protected float m_MaxLifeMsecs;


        #region Public Properties

        public bool IsDone 
        { 
            get
            {
                return m_IsDone;
            }
        }

        #endregion


        public Modifier(float maxlife)
        {
            m_IsDone            = false;
            m_TimeAliveMsecs    = 0;
            m_MaxLifeMsecs      = maxlife;
        }


        #region Update(objparams, gametime)

        public virtual void Update(T objparams, GameTime gametime)
        {

            // Increase the alive count

            m_TimeAliveMsecs    = MathHelper.Clamp(m_TimeAliveMsecs + gametime.ElapsedGameTime.Milliseconds, 0, m_MaxLifeMsecs);
            m_IsDone            = ((m_MaxLifeMsecs > 0) && (m_TimeAliveMsecs >= m_MaxLifeMsecs));

        }

        #endregion


        #region Apply(objparams)

        public virtual void Apply(T objparams)
        {
        }

        #endregion


        #region Restart()

        public void Restart()
        {
            m_IsDone            = false;
            m_TimeAliveMsecs    = 0;
        }

        #endregion

    }
}
