using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D
{
    public enum ChainRepeatMode
    {
        RunOnce,
        Repeat
    }

    public class ChainAutomator<T> : Automator<T>
    {
        private Automator<T>[] automators;
        private int currAutoPos;
        private ChainRepeatMode repeatMode;


        #region Properties

        public Automator<T>[] Automators
        {
            get
            {
                return automators;
            }
            protected set
            {
                automators = value;
            }
        }


        public int CurrAutoPos
        {
            get
            {
                return currAutoPos;
            }
            protected set
            {
                currAutoPos = value;
            }
        }


        public ChainRepeatMode RepeatMode
        {
            get
            {
                return repeatMode;
            }
            protected set
            {
                repeatMode = value;
            }
        }

        #endregion


        public ChainAutomator(Automator<T>[] automators, ChainRepeatMode repeatMode, float maxLifeMsecs) : base(maxLifeMsecs)
        {
            this.automators     = automators;
            this.currAutoPos    = 0;
            this.repeatMode     = repeatMode;
        }


        #region Apply(objparams, gametime)

        public override void Apply(T objParams, GameTime gameTime)
        {

            // Update the current modifier

            automators[currAutoPos].Apply(objParams, gameTime);


            // If the current automator is done, move on to the next one

            if (automators[currAutoPos].IsDone)
            {
                if (currAutoPos >= automators.Length - 1)
                {
                    if (repeatMode == ChainRepeatMode.Repeat)
                    {
                        currAutoPos = 0;
                        automators[currAutoPos].Restart();
                    }
                    else
                    {
                        IsDone = true;
                    }
                }
                else
                {
                    currAutoPos++;
                    automators[currAutoPos].Restart();
                }
            }
        }

        #endregion

    }
}
