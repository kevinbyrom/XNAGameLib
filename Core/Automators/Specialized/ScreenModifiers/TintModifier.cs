using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public class TintAutomator : Automator<ScreenEntity>
    {
        public static readonly float IgnoreColor = float.MaxValue;

        private Vector4 startTint;
        private Vector4 endTint;
        

        public TintAutomator(Vector4 startTint, Vector4 endTint, float maxLifeMsecs) : base(maxLifeMsecs)
        {
            this.startTint     = startTint;
            this.endTint       = endTint;
        }


        #region Apply(srcParams, gameTime)

        public override void Apply(ScreenEntityParams scrParams, GameTime gameTime)
        {

            base.Apply(scrParams, gameTime);


            // Store the tint

             Vector4 objTint = scrParams.Tint.ToVector4();
            
            
            // Calculate the current start and end tints

            Vector4 currStartTint = new Vector4(this.startTint.X < IgnoreColor ? this.startTint.X : objTint.X,
                                           this.startTint.Y < IgnoreColor ? this.startTint.Y : objTint.Y,
                                           this.startTint.Z < IgnoreColor ? this.startTint.Z : objTint.Z,
                                           this.startTint.W < IgnoreColor ? this.startTint.W : objTint.W);

            Vector4 currEndTint = new Vector4(this.endTint.X < IgnoreColor ? this.endTint.X : objTint.X,
                                           this.endTint.Y < IgnoreColor ? this.endTint.Y : objTint.Y,
                                           this.endTint.Z < IgnoreColor ? this.endTint.Z : objTint.Z,
                                           this.endTint.W < IgnoreColor ? this.endTint.W : objTint.W);


            // Interpolate the new color

            float pct = TimeAliveMsecs / MaxLifeMsecs;

            Vector4 vector = new Vector4(Interpolation.Linear(currStartTint.X, currEndTint.X, pct),
                                         Interpolation.Linear(currStartTint.Y, currEndTint.Y, pct),
                                         Interpolation.Linear(currStartTint.Z, currEndTint.Z, pct),
                                         Interpolation.Linear(currStartTint.W, currEndTint.W, pct));

            scrParams.Tint = new Color(vector);
        }

        #endregion


        #region Pre-Defined Tint Automators

        public static TintAutomator FadeIn(float maxOpacity, float maxLifeMsecs)
        {
            return new TintAutomator(new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,IgnoreColor), new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,maxOpacity), maxLifeMsecs);
        }


        public static TintAutomator FadeOut(float minOpacity, float maxLifeMsecs)
        {
            return new TintAutomator(new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,IgnoreColor), new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,minOpacity), maxLifeMsecs);
        }

        
        public static ChainAutomator<ScreenEntityParams> FadeOutIn(float minOpacity, float maxOpacity, float maxLifeMsecs, ChainRepeatMode repeatMode)
        {
            Automator<ScreenEntityParams>[] automators = new Automator<ScreenEntityParams>[] { FadeOut(minOpacity, maxLifeMsecs / 2), FadeIn(maxOpacity, maxLifeMsecs / 2) };
 
            return new ChainAutomator<ScreenEntityParams>(automators, repeatMode, 0);
        }


        public static TintAutomator ToColor(Vector4 color, float maxLifeMsecs)
        {
            return new TintAutomator(new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,IgnoreColor), color, maxLifeMsecs);
        }

        #endregion

    }
}
