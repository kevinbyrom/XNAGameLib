using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public class SpriteAnimFrame : GameEntity, ICloneable
    {
        private Rectangle rect;
        private int timeMSecs;


        #region Public Properties

        public Rectangle Rect
        {
            get
            {
                return rect;
            }
            protected set
            {
                rect = value;
            }
        }

        public int TimeMSecs
        {
            get
            {
                return timeMSecs;
            }
            protected set
            {
                timeMSecs = value;
            }
        }

        #endregion


        #region Constructors

        public SpriteAnimFrame() : this(Rectangle.Empty, 0) {}
        
        public SpriteAnimFrame(Rectangle rect, int timeMSecs)
        {
            this.rect      = rect;
            this.timeMSecs = timeMSecs;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {

            // Load the rectangle values

            rect = XmlUtil.GetNodeRectangle(node);           


            // Load the time 

            timeMSecs = Convert.ToInt32(XmlUtil.GetNodeAttribute(node, "timemsecs", "0"));

        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new SpriteAnimFrame(rect, timeMSecs);
        }

        #endregion

    }
}
