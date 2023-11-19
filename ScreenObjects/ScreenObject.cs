using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using XNAGameLib2D.Modifiers;


namespace XNAGameLib2D
{
    public class ScreenObject : GameObject
    {
        protected ScreenObjParams m_ScreenParams;
        protected List<Modifier<ScreenObjParams>> m_ScreenModifiers;
        protected int m_State;


        #region Public Properties

        public ScreenObjParams ScreenParams
        {
            get
            {
                return m_ScreenParams;
            }
            set
            {
                m_ScreenParams = value;
            }
        }


        public List<Modifier<ScreenObjParams>> ScreenModifiers
        {
            get
            {
                return m_ScreenModifiers;
            }
        }


        public int State
        {
            get
            {
                return m_State;
            }
        }

        #endregion


        #region Constructors

        public ScreenObject()
        {
            m_ScreenParams      = new ScreenObjParams();
            m_ScreenModifiers   = new List<Modifier<ScreenObjParams>>();
            m_State             = 0;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {
            base.Load(node);

            m_State = Convert.ToInt32(XmlUtil.GetNodeText(node, "state", "0"));

            if (node["screenparams"] != null)
                m_ScreenParams.Load(node["screenparams"]);
        }

        #endregion


        #region Draw(spritebatch)

        public override void Draw(SpriteBatch spritebatch, GameTime gametime)
        {
            Draw(spritebatch, gametime, m_ScreenParams);
        }

        public virtual void Draw(SpriteBatch spritebatch, GameTime gametime, ScreenObjParams screenparams)
        {          
            
            // If the object is visible...

            if (screenparams.IsVisible && screenparams.SpriteTexture != null)
            {
                
                // Process any screen modifiers

                if (m_ScreenModifiers.Count > 0)
                    ProcessScreenModifiers(gametime);

                             
                // If there are sprite animations, get the next frame

                if (screenparams.SpriteAnimation.Enabled)
                {
                    screenparams.SpriteAnimation.Update(gametime);
                    screenparams.SpriteRect = screenparams.SpriteAnimation.CurrFrame.Rect;
                }


                // Draw the sprite

                spritebatch.Draw(screenparams.SpriteTexture, screenparams.Position, screenparams.SpriteRect, screenparams.Tint, screenparams.Rotation, screenparams.Origin, screenparams.Scale, SpriteEffects.None, screenparams.Depth);
            }
        }

        #endregion
        

        #region ProcessScreenModifiers(gametime)

        private void ProcessScreenModifiers(GameTime gametime)
        {
            foreach (Modifier<ScreenObjParams> modifier in m_ScreenModifiers)
                if (!modifier.IsDone)
                    modifier.Update(m_ScreenParams, gametime);

            /*Queue<string> removequeue = new Queue<string>();


            // Process the animators

            foreach(string key in m_ScreenAutomators.Keys) 
            {
                Automator<ScreenObjParams> anim = m_ScreenAutomators[key];

                if (!anim.IsDone)
                {
                    anim.Update(gametime);
                    anim.Apply(m_ScreenParams);
                }
                else
                    removequeue.Enqueue(key);
            }


            // Remove finished animators

            while (removequeue.Count > 0)
            {
                string key = removequeue.Dequeue();
                m_ScreenAutomators.Remove(key);
            }*/
        }

        #endregion


        #region GetScreenVisibilityRect()

        public Rectangle GetScreenVisibilityRect()
        {
            float maxdimension = Math.Max(m_ScreenParams.SpriteRect.Width, m_ScreenParams.SpriteRect.Height) * m_ScreenParams.Scale;

            return new Rectangle((int)(m_ScreenParams.Position.X - (maxdimension / 2)), (int)(m_ScreenParams.Position.Y - (maxdimension /2)), (int)(maxdimension), (int)(maxdimension));
        }

        #endregion


        #region State Routines

        public bool InState(int state)
        {
            return ((m_State & state) > 0);
        }

        public void SetState(int state)
        {
            bool statechanged = m_State != state;

            m_State = state;

            if (statechanged)
                StateChanged();
        }

        public void AddState(int state)
        {
            bool statechanged = m_State != (m_State | state);

            m_State |= state;

            if (statechanged)
                StateChanged();
        }

        public void RemoveState(int state)
        {
            bool statechanged = m_State != (m_State & ~state);

            m_State &= ~state;

            if (statechanged)
                StateChanged();
        }

        public virtual void StateChanged()
        {
        }

        #endregion

    }
}
