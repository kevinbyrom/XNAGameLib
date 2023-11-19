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
    public class SpriteEntity : ScreenEntity
    {
        public Sprite Sprite;


        #region Constructors

        public SpriteEntity() : base() {}
        public SpriteEntity(Vector2 screenPos, Vector2 screenSize) : base(screenPos, screenSize) {}
    
        #endregion


        #region override ClearToDefaults()

        public override void ClearToDefaults()
        {
            base.ClearToDefaults();

            Sprite = new Sprite();
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {
            base.Load(node);

            if (node["sprite"] != null)
                Sprite.Load(node["sprite"]);
        }

        #endregion


        #region Draw Routines

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            // Draw the sprite

            Sprite.Draw(spriteBatch, gameTime, ScreenPos);

        }

        #endregion

    }
}
