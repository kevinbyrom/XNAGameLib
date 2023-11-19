using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public class Sprite : GameEntity, ICloneable 
    {
        private bool isVisible;
        private Vector2 origin;
        private float rotation;
        private float scale;
        private float depth;
        private Color tint;
        private Texture2D texture;
        private SpriteAnimation animation;


        #region Properties

        public bool IsVisible 
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
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


        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }


        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }


        public float Depth
        {
            get
            {
                return depth;
            }
            set
            {
                depth = value;
            }
        }


        public Color Tint
        {
            get
            {
                return tint;
            }
            set
            {
                tint = value;
            }
        }


        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }


        public SpriteAnimation Animation
        {
            get
            {
                return animation;
            }
            set
            {
                animation = value;
            }
        }


        public Rectangle SpriteRect
        {
            get
            {
                return animation.CurrFrame.Rect;
            }
        }
        
        #endregion


        #region Constructors

        public Sprite() : this(true, Color.White, 0, 1, 1) { }
        
        public Sprite(bool isVisible, Color tint, float rotation, float scale, float depth) : this(position, tint, rotation, scale, depth, Vector2.Zero, null, new SpriteAnimation()) { }
        
        public Sprite(bool isVisible, Color tint, float rotation, float scale, float depth, Vector2 origin, Texture2D texture, SpriteAnimation animation)
        {
            this.isVisible    = isVisible;
            this.tint         = tint;
            this.rotation     = rotation;
            this.scale        = scale;
            this.depth        = depth;            
            this.origin       = origin;
            this.texture      = texture;
            this.animation    = animation;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {
            isVisible   = Convert.ToBoolean(XmlUtil.GetNodeText(node, "isVisible", "true"));
            rotation    = (float)Convert.ToDouble(XmlUtil.GetNodeText(node, "rotation", "0"));
            scale       = (float)Convert.ToDouble(XmlUtil.GetNodeText(node, "scale", "1"));
            depth       = (float)Convert.ToDouble(XmlUtil.GetNodeText(node, "depth", "0"));

            if (node["origin"] != null)
                origin = XmlUtil.GetNodeVector2(node["origin"]);

            if (node["position"] != null)
                position = XmlUtil.GetNodeVector2(node["position"]);

            if (node["tint"] != null)
                tint = XmlUtil.GetNodeColor(node["tint"]);

            if (node["spritetexture"] != null)
                texture = GameEngine2D.TextureManager.LoadTexture(node["spritetexture"].Attributes["name"].InnerText, node["spriteTexture"].Attributes["filename"].InnerText);
            
            if (node["spriteAnimation"] != null)
                animation.Load(node["spriteAnimation"]);

        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new Sprite(tint, rotation, scale, depth, isVisible, origin, texture, animation);
        }

        #endregion

    }
}
