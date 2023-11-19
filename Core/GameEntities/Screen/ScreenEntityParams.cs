using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public class ScreenEntityParams : GameEntity, ICloneable 
    {
        public bool IsVisible;
        public Vector2 Origin;
        public Vector2 Position;
        public float Rotation;
        public float Scale;
        public float Depth;
        public Color Tint;
        public Texture2D SpriteTexture;
        public Rectangle SpriteRect;
        public SpriteAnimation SpriteAnimation;
        public float LayerDepth;


        #region Constructors

        public ScreenEntityParams() : this(Vector2.Zero, Color.White, 0, 1, 1) { }
        
        public ScreenEntityParams(Vector2 position, Color tint, float rotation, float scale, float depth) : this(position, tint, rotation, scale, depth, true, Vector2.Zero, null, Rectangle.Empty, new SpriteAnimation()) { }
        
        public ScreenEntityParams(Vector2 position, Color tint, float rotation, float scale, float depth, bool isVisible, Vector2 origin, Texture2D spritetexture, Rectangle spriterect, SpriteAnimation spriteanimation)
        {
            this.Position           = position;
            this.Tint               = tint;
            this.Rotation           = rotation;
            this.Scale              = scale;
            this.Depth              = depth;
            this.IsVisible          = isVisible;
            this.Origin             = origin;
            this.SpriteTexture      = spritetexture;
            this.SpriteRect         = spriterect;
            this.SpriteAnimation    = spriteanimation;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {
            IsVisible   = Convert.ToBoolean(XmlUtil.GetNodeText(node, "isvisible", "true"));
            Rotation    = (float)Convert.ToDouble(XmlUtil.GetNodeText(node, "rotation", "0"));
            Scale       = (float)Convert.ToDouble(XmlUtil.GetNodeText(node, "scale", "1"));
            Depth       = (float)Convert.ToDouble(XmlUtil.GetNodeText(node, "depth", "0"));

            if (node["origin"] != null)
                Origin = XmlUtil.GetNodeVector2(node["origin"]);

            if (node["position"] != null)
                Position = XmlUtil.GetNodeVector2(node["position"]);

            if (node["tint"] != null)
                Tint = XmlUtil.GetNodeColor(node["tint"]);

            if (node["spritetexture"] != null)
                SpriteTexture = GameEngine2D.TextureManager.LoadTexture(node["spritetexture"].Attributes["name"].InnerText, node["spritetexture"].Attributes["filename"].InnerText);
            
            if (node["spriterect"] != null)
                SpriteRect = XmlUtil.GetNodeRectangle(node["spriterect"]);

            if (node["spriteanimation"] != null)
                SpriteAnimation.Load(node["spriteanimation"]);

        }

        #endregion


        #region GetScreenRect()

        public Rectangle GetScreenRect()
        {
            float scaledWidth   = SpriteRect.Width * Scale;
            float scaledHeight  = SpriteRect.Height * Scale;

            return new Rectangle((int)(Position.X - (scaledWidth / 2)), (int)(Position.Y - (scaledHeight / 2)), (int)(scaledWidth), (int)(scaledHeight));
        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new ScreenEntityParams(Position, Tint, Rotation, Scale, Depth, IsVisible, Origin, SpriteTexture, SpriteRect, SpriteAnimation);
        }

        #endregion

    }
}
