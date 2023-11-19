using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public class ScreenEntity : GameEntity
    {
        public Vector2 ScreenPos;
        public Vector2 ScreenSize;


        #region Constructors

        public ScreenEntity() : this(Vector2.Zero, Vector2.Zero) {}

        public ScreenEntity(Vector2 screenPos, Vector2 screenSize) : base()
        {
            this.ScreenPos      = screenPos;
            this.ScreenSize     = screenSize;
        }

        #endregion


        #region override ClearToDefaults()

        public override void ClearToDefaults()
        {
            base.ClearToDefaults();

            ScreenPos   = Vector2.Zero;
            ScreenSize  = Rectangle.Empty;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {
            base.Load(node);

            XmlNode screenNode = node["screen"];

            if (screenNode != null)
            {
                if (screenNode["position"] != null)
                    ScreenPos = XmlUtil.GetNodeVector2(screenNode["position"]);

                if (screenNode["size"] != null)
                    ScreenSize = XmlUtil.GetNodeVector2(screenNode["size"]);
            }
        }

        #endregion


        #region Draw Routines

        public virtual void Draw(GraphicsDevice graphics, GameTime gameTime)
        {
        }

        #endregion

    }
}
