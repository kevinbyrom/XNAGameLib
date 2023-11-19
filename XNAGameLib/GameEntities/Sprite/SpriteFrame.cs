using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public class SpriteFrame : GameEntity, ICloneable
    {
        private Rectangle rect;
        private Vector2 origin;
        private int timeMSecs;
        private int soundID;


        #region Public Properties

        public Rectangle Rect
        {
            get
            {
                return rect;
            }
            set
            {
                rect = value;
            }
        }


        public Vector2 Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }
        }


        public int TimeMSecs
        {
            get
            {
                return timeMSecs;
            }
            set
            {
                timeMSecs = value;
            }
        }


        private int SoundID
        {
            get
            {
                return soundID;
            }
            set
            {
                soundID = value;
            }
        }

        #endregion


        #region Constructors

        public SpriteFrame() : this(Rectangle.Empty, Vector2.Zero, 0) {}
        
        public SpriteFrame(Rectangle rect, Vector2 origin, int timeMSecs)
        {
            this.rect       = rect;
            this.origin     = origin;
            this.timeMSecs  = timeMSecs;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {

            // Load the rectangle values

            if (node["rect"] != null)
                rect = XmlUtil.GetNodeRectangle(node["rect"]);           


            // Load the origin

            if (node["origin"] != null)
                origin = XmlUtil.GetNodeVector2(node["origin"]);


            // Load the time 

            if (node.Attributes["timeMSecs"] != null)
                timeMSecs = Convert.ToInt32(XmlUtil.GetNodeAttribute(node, "timeMSecs", "0"));

        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new SpriteFrame(rect, origin, timeMSecs);
        }

        #endregion

    }
}
