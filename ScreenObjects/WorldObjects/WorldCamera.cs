using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class WorldCamera : WorldObject
    {
        protected float m_ViewWidth;
        protected float m_ViewHeight;
        protected float m_HalfViewWidth;
        protected float m_HalfViewHeight;
        protected List<WorldObject> m_Targets;


        #region Public Properties
        
        public List<WorldObject> Targets
        {
            get
            {
                return m_Targets;
            }
        }


        public int ViewWidth
        {
            set
            {
                m_ViewWidth        = value;
                m_HalfViewWidth    = m_ViewWidth / 2;
            }
        }


        public int ViewHeight
        {
            set
            {
                m_ViewHeight        = value;
                m_HalfViewHeight    = m_ViewHeight / 2;
            }
        }

        #endregion


        #region Constructors

        public WorldCamera(World world) : base(world) 
        {
            m_Targets = new List<WorldObject>();

            /*m_ViewWidth         = viewwidth;
            m_ViewHeight        = viewheight;
            m_HalfViewWidth     = viewwidth / 2;
            m_HalfViewHeight    = viewheight / 2;*/
        }

        #endregion


        #region Initialize()

        public override void Initialize()
        {
            base.Initialize();

            m_ViewWidth       = GameManager.Graphics.PreferredBackBufferWidth;
            m_ViewHeight      = GameManager.Graphics.PreferredBackBufferHeight;
            m_HalfViewWidth   = m_ViewWidth / 2;
            m_HalfViewHeight  = m_ViewHeight / 2;

            m_WorldParams.Position.X = m_World.Width / 2;
            m_WorldParams.Position.Y = m_World.Height / 2;
        }

        #endregion


        #region Update(gametime)

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);


            // Follow the tracking objects

            if (m_Targets.Count > 0)
            {
                float x = 0, y = 0;

                foreach (WorldObject obj in m_Targets)
                {
                    x += obj.WorldParams.Position.X;
                    y += obj.WorldParams.Position.Y;
                }

                m_WorldParams.Position.X = x / m_Targets.Count;
                m_WorldParams.Position.Y = y / m_Targets.Count;
            }


            // Make sure we do not exceed bounds

            m_WorldParams.Position.X = MathHelper.Clamp(m_WorldParams.Position.X, m_HalfViewWidth / m_ScreenParams.Scale, m_World.Width - (m_HalfViewWidth / m_ScreenParams.Scale));
            m_WorldParams.Position.Y = MathHelper.Clamp(m_WorldParams.Position.Y, m_HalfViewHeight / m_ScreenParams.Scale, m_World.Height - (m_HalfViewHeight / m_ScreenParams.Scale));
        }

        #endregion


        #region ConvertScreenParams(obj)

        public ScreenObjParams ConvertScreenParams(WorldObject obj)
        {
            ScreenObjParams converted = (ScreenObjParams)obj.ScreenParams.Clone();

            converted.Position.X    = ((obj.WorldParams.Position.X - m_WorldParams.Position.X) * m_ScreenParams.Scale) + m_HalfViewWidth;
            converted.Position.Y    = ((obj.WorldParams.Position.Y - m_WorldParams.Position.Y) * m_ScreenParams.Scale) + m_HalfViewHeight;
            converted.Scale         = obj.WorldParams.Scale * m_ScreenParams.Scale;
            
            return converted;
        }

        #endregion


        #region IsInVisibleRange(rect)

        public bool IsInVisibleRange(Rectangle rect)
        {
            Rectangle camerarect = new Rectangle((int)(m_WorldParams.Position.X - (m_ScreenParams.Scale * m_HalfViewWidth)), (int)(m_WorldParams.Position.Y - (m_ScreenParams.Scale * m_HalfViewHeight)), (int)(m_ScreenParams.Scale * m_ViewWidth), (int)(m_ScreenParams.Scale * m_ViewHeight));
            bool inrange = true;

            if ((rect.Left < camerarect.Left && rect.Right < camerarect.Left) ||
                (rect.Left > camerarect.Right && rect.Right > camerarect.Right) ||
                (rect.Top < camerarect.Top && rect.Bottom < camerarect.Top) ||
                (rect.Top > camerarect.Bottom && rect.Bottom > camerarect.Bottom))
                inrange = false;

            return inrange;
        }

        #endregion

    }
}
