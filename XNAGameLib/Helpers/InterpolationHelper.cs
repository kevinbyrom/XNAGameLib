using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D.Utilities
{
    public static class InterpolationHelper
    {
        
        public static float Linear(float val1, float val2, float pct)
        {
            return MathHelper.Lerp(val1, val2, pct);
        }

        public static float Catmull(float val1, float val2, float val3, float val4, float pct)
        { 
            if (val1 != val2)
                return (val1 * (-.05f * pct * pct * pct + pct * pct - 0.5f * pct) +
                        val2 * (1.5f * pct * pct * pct + -2.5f * pct * pct + 1.0f) + 
                        val3 * (-1.5f * pct * pct * pct + 2.0f * pct * pct + 0.5f * pct) +
                        val4 * (0.5f * pct * pct * pct - 0.5f * pct * pct));
            else
                return val1;
        }

        public static float Sine(float val1, float val2, float pct)
        {
            float angle     = MathHelper.PiOver4 * pct;
            float sinpct    = (float)Math.Sin(angle);
 
            if (val1 != val2)
                return (val1 + ((val2 - val1) * sinpct));
            else
                return val1;
        }

    }
}
