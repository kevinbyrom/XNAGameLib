using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNAGameLib2D
{
    public class GameEntity
    {
        private Guid guid;
        private int currState;
        private bool enabled;


        #region Properties

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }

        #endregion


        public GameEntity()
        {
            ClearToDefaults();
        }


        #region ClearToDefaults()

        public virtual void ClearToDefaults()
        {
            guid        = Guid.NewGuid();
            currState   = 0;
            enabled     = true;
        }

        #endregion


        #region Initialize()

        public virtual void Initialize()
        {            
        }

        #endregion


        #region Update(gameTime)

        public virtual void Update(GameTime gameTime)
        {         
        }

        #endregion


        #region ProcessInput(gameTime, padState)

        public virtual void ProcessInput(GameTime gameTime, GamePadState padState)
        {
        }

        #endregion


        #region Load Routines

        public void Load(string filename, string node)
        {
            XmlDocument xml = new XmlDocument();

            xml.Load(System.IO.Path.Combine(GameEngine2D.AssetsPath, filename));
            
            ClearToDefaults();
            
            Load(xml["gameEntities"][node]);
        }


        public virtual void Load(XmlNode node)
        {
        }

        #endregion


        #region GetElapsedSeconds(gameTime)

        protected float GetElapsedSeconds(GameTime gameTime)
        {
            return ((float)gameTime.ElapsedGameTime.Milliseconds / 1000);
        }

        #endregion


        #region Messaging Routines

        public virtual void SendMessage(GameEntity sender, int msg, string[] msgParams)
        {
        }


        public virtual void SendMessage(GameEntity sender, Guid target, int msg, string[] msgParams)
        {
        }

        #endregion


        #region State Routines

        public bool InState(int state)
        {
            return ((this.currState & state) > 0);
        }

        public void SetState(int state)
        {
            bool stateChanged = this.currState != state;

            this.currState = state;

            if (stateChanged)
                OnStateChanged();
        }

        public void AddState(int state)
        {
            bool stateChanged = this.currState != (this.currState | state);

            this.currState |= state;

            if (stateChanged)
                OnStateChanged();
        }

        public void RemoveState(int state)
        {
            bool stateChanged = this.currState != (this.currState & ~state);

            this.currState &= ~state;

            if (stateChanged)
                OnStateChanged();
        }

        public virtual void OnStateChanged()
        {
        }


        public virtual void OnStateEnter()
        {
        }


        public virtual void OnStateExit()
        {
        }

        #endregion

    }
}
