using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D.Utilities
{
    static public class VectorHelper
    {

        static public Vector3 RotateVector3(Vector3 vector, float radians)
        {
            Vector3 rotated;
            
            rotated.X = (float)(Math.Cos(radians) * vector.X - Math.Sin(radians) * vector.Y);
            rotated.Y = (float)(Math.Cos(radians) * vector.Y + Math.Sin(radians) * vector.X);
            rotated.Z = vector.Z;

            return rotated;
        }
                   
        static public float Vector3ToRadians(Vector3 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X) + MathHelper.PiOver2;
        }

        static public Vector3 RadiansToVector3(float radians)
        {
            return new Vector3((float)(Math.Cos(radians - MathHelper.PiOver2)), (float)(Math.Sin(radians - MathHelper.PiOver2)), 0);
        }

    }
}