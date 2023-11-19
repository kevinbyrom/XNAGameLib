using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class SpriteAnimFrame : GameObject, ICloneable
    {
        protected Rectangle m_Rect;
        protected int m_TimeMSecs;


        #region Public Properties

        public Rectangle Rect
        {
            get
            {
                return m_Rect;
            }
        }

        public int TimeMSecs
        {
            get
            {
                return m_TimeMSecs;
            }
        }

        #endregion


        #region Constructors

        public SpriteAnimFrame() : this(Rectangle.Empty, 0) {}
        
        public SpriteAnimFrame(Rectangle rect, int timemsecs)
        {
            m_Rect      = rect;
            m_TimeMSecs = timemsecs;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {

            // Load the rectangle values

            m_Rect = XmlUtil.GetNodeRectangle(node);           


            // Load the time 

            m_TimeMSecs = Convert.ToInt32(XmlUtil.GetNodeAttribute(node, "timemsecs", "0"));

        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new SpriteAnimFrame(m_Rect, TimeMSecs);
        }

        #endregion

    }
}
