using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D
{
    public class Automator<T> 
    {
        private bool isDone;
        private float timeAliveMsecs;
        private float maxLifeMsecs;


        #region Properties

        public bool IsDone 
        { 
            get
            {
                return isDone;
            }
            protected set
            {
                isDone = value;
            }
        }


        public float TimeAliveMsecs
        {
            get
            {
                return timeAliveMsecs;
            }
        }


        public float MaxLifeMsecs
        {
            get
            {
                return maxLifeMsecs;
            }
            protected set
            {
                maxLifeMsecs = value;
            }
        }

        #endregion


        public Automator(float maxLifeMsecs)
        {
            this.isDone            = false;
            this.timeAliveMsecs    = 0;
            this.maxLifeMsecs      = maxLifeMsecs;
        }


        #region Apply Routines

        public virtual void Apply(T obj, GameTime gameTime)
        {

            // Increase the alive count

           timeAliveMsecs    = MathHelper.Clamp(timeAliveMsecs + gameTime.ElapsedGameTime.Milliseconds, 0, maxLifeMsecs);
           isDone            = ((maxLifeMsecs > 0) && (timeAliveMsecs >= maxLifeMsecs));

        }


        public virtual void Apply(T obj)
        {
        }

        #endregion


        #region Restart()

        public void Restart()
        {
            isDone            = false;
            timeAliveMsecs    = 0;
        }

        #endregion

    }
}
