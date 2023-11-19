using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{

    public enum SpritePlayMode
    {
        Auto    = 0,
        Manual  = 1
    }


    public enum SpriteRepeatMode
    {
        PlayOnce    = 0,
        Repeat      = 1
    }


    public class Sprite : GameEntity, ICloneable, IDrawable 
    {
        public Texture2D Texture;
        public Rectangle SpriteRect;
        public Vector2 Origin;
        public float Rotation;
        public float Scale;
        public float Depth;
        public SpriteEffects Effects;
        public Dictionary<int, SpriteAnimation> Animations;
        
        private SpriteAnimation currAnimation;     
        private float opacity;
        private Vector3 tint;
        private Color color;

        
        #region Properties

        public float Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                opacity = value;
                this.color = new Color(new Vector4(tint.X, tint.Y, tint.Z, opacity));
            }
        }


        public Vector3 Tint
        {
            get
            {
                return tint;
            }
            set
            {
                tint = value;
                this.color = new Color(new Vector4(tint.X, tint.Y, tint.Z, opacity));
            }
        }
       
        #endregion


        #region Constructors

        public Sprite() : this(null, Rectangle.Empty, Vector2.Zero, 0, 1, 1, 1, new Vector3(1f,1f,1f)) { }
        
        public Sprite(Texture2D texture, Rectangle spriteRect, Vector2 origin, float rotation, float scale, float depth, float opacity, Vector3 tint) : this(texture, spriteRect, origin, rotation, scale, depth, 1, new Vector3(1f,1f,1f), SpriteEffects.None, null, new Dictionary<int,SpriteAnimation>()) { }
        
        public Sprite(Texture2D texture, Rectangle spriteRect, Vector2 origin, float rotation, float scale, float depth, float opacity, Vector3 tint, SpriteEffects effects, SpriteAnimation currAnimation, Dictionary<int, SpriteAnimation> animations) : base()
        {
            Texture        = texture;
            SpriteRect     = spriteRect;
            Origin         = origin;
            Rotation       = rotation;
            Scale          = scale;
            Depth          = depth; 
            Effects        = effects;
            Animations     = animations;

            this.opacity        = opacity;
            this.tint           = tint;
            this.color          = new Color(new Vector4(tint.X, tint.Y, tint.Z, opacity));
            this.currAnimation  = currAnimation;            
        }

        #endregion


        #region override ClearToDefaults()

        public override void ClearToDefaults()
        {
            base.ClearToDefaults();

            this.Texture        = null;
            this.Rotation       = 0;
            this.Scale          = 0;
            this.Depth          = 1; 
            this.Effects        = SpriteEffects.None;
            this.Animations     = new Dictionary<int, SpriteAnimation>();

            this.opacity        = 1;
            this.tint           = new Vector3(1f,1f,1f);
            this.color          = new Color(new Vector4(tint.X, tint.Y, tint.Z, opacity));
            this.currAnimation  = null;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {

            // Load the standard properties

            Rotation        = (float)Convert.ToDouble(XmlUtil.GetNodeAttribute(node, "rotation", "0"));
            Scale           = (float)Convert.ToDouble(XmlUtil.GetNodeAttribute(node, "scale", "1"));
            Depth           = (float)Convert.ToDouble(XmlUtil.GetNodeAttribute(node, "depth", "0"));
            this.opacity    = (float)Convert.ToDouble(XmlUtil.GetNodeAttribute(node, "opacity", "1"));

            if (node["origin"] != null)
                Origin = XmlUtil.GetNodeVector2(node["origin"]);

            if (node["tint"] != null)
                this.tint = XmlUtil.GetNodeVector3(node["tint"]);

            if (node["texture"] != null)
                Texture = GameEngine2D.TextureManager.LoadTexture(node["texture"].Attributes["name"].InnerText, node["texture"].Attributes["filename"].InnerText);
            
            if (node["spriteRect"] != null)
                SpriteRect = XmlUtil.GetNodeRectangle(node["spriteRect"]);
            
            this.color = new Color(new Vector4(tint.X, tint.Y, tint.Z, opacity));

            
            // Load the animations

            if (node["animations"] != null)
            {
                foreach (XmlNode animNode in node["animations"].SelectNodes("animation"))
                {
                    int type;
                    SpriteAnimation anim = new SpriteAnimation();

                    type = Convert.ToInt32(animNode.Attributes["type"].InnerText);
                    
                    anim.Load(animNode);

                    Animations.Add(type, anim);
                }
            }
        }

        #endregion


        #region Draw Routines

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 screenPos)
        {

            // If animations are enabled, update them

            if (this.currAnimation != null)
            {
                this.currAnimation.Update(gameTime);
                SpriteRect  = currAnimation.CurrFrame.Rect;
                Origin      = currAnimation.CurrFrame.Origin;
            }

            spriteBatch.Draw(Texture, screenPos, SpriteRect, this.color, Rotation, Origin, Scale, Effects, Depth);
        }

        #endregion


        #region SetAnimation(type)

        public void SetAnimation(int type)
        {
            if (Animations.ContainsKey(type))
            {
                this.currAnimation = Animations[type];
                this.currAnimation.CurrTimeMSecs = 0;
            }
            else
            {
                throw new Exception(String.Format("Animation type ({0}) not found", type));
            }
        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new Sprite(Texture, SpriteRect, Origin, Rotation, Scale, Depth, this.opacity, this.tint, Effects, this.currAnimation, Animations);
        }

        #endregion

    }
}
