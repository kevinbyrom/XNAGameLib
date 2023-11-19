using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class WorldView : ScreenLayer
    {
        protected World m_World;
        protected int m_HalfWidth;
        protected int m_HalfHeight;


        #region Constructors

        public WorldView(World world, int width, int height) : base(width, height)
        {
            m_World = world;
            m_HalfWidth = width / 2;
            m_HalfHeight = height / 2;
        }

        public WorldView(World world, int width, int height, Color backcolor) : base(width, height, backcolor)
        {
            m_World = world;
            m_HalfWidth = width / 2;
            m_HalfHeight = height / 2;
        }

        #endregion


        #region WorldToScreen(obj)

        public ScreenObjParams WorldToScreen(WorldObject obj)
        {
            ScreenObjParams retparams = (ScreenObjParams)obj.ScreenParams.Clone();

            retparams.Position.X    = ((obj.WorldParams.Position.X - m_World.Camera.WorldParams.Position.X) * m_World.Camera.ScreenParams.Scale) + m_HalfWidth;
            retparams.Position.Y    = ((obj.WorldParams.Position.Y - m_World.Camera.WorldParams.Position.Y) * m_World.Camera.ScreenParams.Scale) + m_HalfHeight;
            retparams.Scale         = obj.WorldParams.Scale * m_World.Camera.ScreenParams.Scale;
            
            return retparams;
        }

        #endregion

    }
}
