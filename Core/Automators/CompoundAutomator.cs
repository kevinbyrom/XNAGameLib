using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D
{
    public enum CompoundRepeatMode
    {
        RunOnce,
        Repeat
    }

    public class CompoundAutomator<T> : Automator<T>
    {
        private Automator<T>[] automators;
        private CompoundRepeatMode repeatMode;


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

        public CompoundRepeatMode RepeatMode
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


        public CompoundAutomator(Automator<T>[] automators, CompoundRepeatMode repeatMode, float maxLifeMsecs) : base(maxLifeMsecs)
        {
            this.automators = automators;
            this.repeatMode = repeatMode;
        }


        #region Apply(objParams, gameTime)

        public override void Apply(T objParams, GameTime gameTime)
        {
            bool allDone = true;


            // Update the current automator

            foreach (Automator<T> automator in automators)
            {
                automator.Apply(objParams, gameTime);

                if (!automator.IsDone)                               
                    allDone = false;
                else if (repeatMode == CompoundRepeatMode.Repeat)
                    automator.Restart();
            }


            // If all automators are done and we are in RunOnce mode, set the IsDone flag

            if (allDone && repeatMode == CompoundRepeatMode.RunOnce)
                IsDone = true;

        }

        #endregion

    }
}
