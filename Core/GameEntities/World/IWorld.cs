using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace XNAGameLib2D.Core
{
    public interface IWorld
    {
        Vector2 Friction { get; }
        Vector2 Gravity { get; }
        float Width { get; }
        float Height { get; }
        WorldCamera Camera { get; }
    }
}
