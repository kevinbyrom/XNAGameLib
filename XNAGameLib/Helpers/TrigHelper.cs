using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D.Utilities
{
    static public class TrigHelper
    {

        static public float Pythagorean(float x, float y)
        {
            return (float)Math.Sqrt((x * x) + (y * y)); 
        }

        static public float Pythagorean(float x, float y, float z)
        {
            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }

    }
}