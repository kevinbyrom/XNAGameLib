using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D.Utilities
{
    public static class GameTimeHelper
    {

        public static float GetElapsedSeconds(GameTime gameTime)
        {
            return ((float)gameTime.ElapsedGameTime.Milliseconds / 1000);
        }

    }
}

