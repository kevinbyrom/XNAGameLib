using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class ScreenLayer : ScreenObject
    {
        protected RenderTarget2D m_RenderTarget;
        protected Color m_BackColor;


        #region Public Properties

        public Color BackColor
        {
            get
            {
                return m_BackColor;
            }
            set
            {
                m_BackColor = value;
            }
        }

        #endregion


        #region Constructors

        public ScreenLayer(int width, int height) : base()
        {
            m_ScreenParams.SpriteRect   = new Rectangle(0, 0, width, height);
            m_BackColor                 = new Color(Vector4.Zero);
        }
        
        public ScreenLayer(int width, int height, Color backcolor) : base()
        {
            m_ScreenParams.SpriteRect   = new Rectangle(0, 0, width, height);
            m_BackColor                 = backcolor;
        }

        #endregion


        #region Initialize(graphics)

        public override void Initialize()
        {
            base.Initialize();

            m_RenderTarget = new RenderTarget2D(GameManager.Graphics.GraphicsDevice, m_ScreenParams.SpriteRect.Width, m_ScreenParams.SpriteRect.Height, 0, SurfaceFormat.Color);
        }

        #endregion


        #region Draw Routines

        public override void Draw(SpriteBatch spritebatch, GameTime gametime)
        {
            
            // Setup so drawing is done on the offscreen buffer

            GameManager.PushRenderTarget(m_RenderTarget);
            GameManager.Graphics.GraphicsDevice.Clear(ClearOptions.Target, BackColor, 0, 0);


            // Draw the objects

            DrawObjects(gametime);


            // Revert back to drawing to the previous target

            GameManager.PopRenderTarget();
            m_ScreenParams.SpriteTexture = m_RenderTarget.GetTexture(); 

            
            // Draw this layer to the previous target

            SpriteBatch outerbatch = new SpriteBatch(GameManager.Graphics.GraphicsDevice);
            
            outerbatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);                   
            outerbatch.Draw(m_ScreenParams.SpriteTexture, m_ScreenParams.Position, m_ScreenParams.SpriteRect, m_ScreenParams.Tint, m_ScreenParams.Rotation, m_ScreenParams.Origin, m_ScreenParams.Scale, SpriteEffects.None, m_ScreenParams.Depth);
            outerbatch.End();
        }


        public virtual void DrawObjects(GameTime gametime)
        {
        }

        #endregion
        
    }
}
