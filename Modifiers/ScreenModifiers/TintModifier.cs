using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D.Modifiers
{
    public class TintModifier : Modifier<ScreenObjParams>
    {
        public static readonly float IgnoreColor = float.MaxValue;

        protected Vector4 m_StartTint;
        protected Vector4 m_EndTint;
        

        public TintModifier(Vector4 starttint, Vector4 endtint, float maxlife) : base(maxlife)
        {
            m_StartTint     = starttint;
            m_EndTint       = endtint;
        }


        public override void Update(ScreenObjParams scrparams, GameTime gametime)
        {

            base.Update(scrparams, gametime);


            // Store the tint

             Vector4 objtint = scrparams.Tint.ToVector4();
            
            
            // Calculate the current start and end tints

            Vector4 starttint = new Vector4(m_StartTint.X < IgnoreColor ? m_StartTint.X : objtint.X,
                                           m_StartTint.Y < IgnoreColor ? m_StartTint.Y : objtint.Y,
                                           m_StartTint.Z < IgnoreColor ? m_StartTint.Z : objtint.Z,
                                           m_StartTint.W < IgnoreColor ? m_StartTint.W : objtint.W);

            Vector4 endtint = new Vector4(m_EndTint.X < IgnoreColor ? m_EndTint.X : objtint.X,
                                           m_EndTint.Y < IgnoreColor ? m_EndTint.Y : objtint.Y,
                                           m_EndTint.Z < IgnoreColor ? m_EndTint.Z : objtint.Z,
                                           m_EndTint.W < IgnoreColor ? m_EndTint.W : objtint.W);


            // Interpolate the new color

            float pct = m_TimeAliveMsecs / m_MaxLifeMsecs;

            Vector4 vector = new Vector4(Interpolation.Linear(starttint.X, endtint.X, pct),
                                         Interpolation.Linear(starttint.Y, endtint.Y, pct),
                                         Interpolation.Linear(starttint.Z, endtint.Z, pct),
                                         Interpolation.Linear(starttint.W, endtint.W, pct));

            scrparams.Tint = new Color(vector);
        }


        #region Pre-Defined Tint Automators

        public static TintModifier FadeIn(float maxopacity, float maxlife)
        {
            return new TintModifier(new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,IgnoreColor), new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,maxopacity), maxlife);
        }


        public static TintModifier FadeOut(float minopacity, float maxlife)
        {
            return new TintModifier(new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,IgnoreColor), new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,minopacity), maxlife);
        }

        
        public static ChainModifier<ScreenObjParams> FadeOutIn(float minopacity, float maxopacity, float maxlife, ChainModRepeatMode repeatmode)
        {
            Modifier<ScreenObjParams>[] modifiers = new Modifier<ScreenObjParams>[] { FadeOut(minopacity, maxlife / 2), FadeIn(maxopacity, maxlife / 2) };
 
            return new ChainModifier<ScreenObjParams>(modifiers, repeatmode, 0);
        }


        public static TintModifier ToColor(Vector4 color, float maxlife)
        {
            return new TintModifier(new Vector4(IgnoreColor,IgnoreColor,IgnoreColor,IgnoreColor), color, maxlife);
        }

        #endregion

    }
}
