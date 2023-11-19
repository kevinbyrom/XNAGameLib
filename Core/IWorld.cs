using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D
{
    public interface IWorld
    {
        float Width { get; }
        float Height { get; }
        WorldCamera Camera { get; }
    }
}
