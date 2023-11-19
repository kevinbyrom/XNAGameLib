using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D.Utilities
{
    static public class TrigHelper
    {

        #region Vector Routines

        static public Vector2 RotateVector2(Vector2 vector, float radians)
        {
            Vector2 rotated;
            
            rotated.X = (float)(Math.Cos(radians) * vector.X - Math.Sin(radians) * vector.Y);
            rotated.Y = (float)(Math.Cos(radians) * vector.Y + Math.Sin(radians) * vector.X);

            return rotated;
        }
                    

        static public float Vector2ToRadians(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X) + MathHelper.PiOver2;
        }


        static public Vector2 RadiansToVector2(float radians)
        {
            return new Vector2((float)(Math.Cos(radians - MathHelper.PiOver2)), (float)(Math.Sin(radians - MathHelper.PiOver2)));
        }

        #endregion


        #region Pythagorean Routines

        static public float Pythagorean(float x, float y)
        {
            return (float)Math.Sqrt((x * x) + (y * y)); 
        }

        static public float Pythagorean(float x, float y, float z)
        {
            return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        #endregion

    }
}