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
        private GameEntity parent;
        private int state;
        private bool enabled;


        public event EventHandler ParentChanged;
        public event EventHandler StateChanged;
        public event EventHandler EnabledChanged;


        public Guid Guid
        {
            get
            {
                return this.guid;
            }
        }
        
        public GameEntity Parent
        {
            get
            {
                return this.parent;
            }
        }
        
        public int State
        {
            get
            {
                return this.state;
            }
            set
            {
                int origState       = this.state;
                bool stateChanged   = this.state != value;

                this.state = state;

                if (stateChanged)
                    OnStateChanged(origState);
            }
        }
        
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;

                if (this.EnabledChanged != null)
                    this.EnabledChanged(this, null);
            }
        }



        #region Constructors

        public GameEntity()
        {
            Initialize();
        }


        public GameEntity(GameEntity parent) : this()
        {
            this.Parent = parent;
        }

        #endregion


        public virtual void ClearToDefaults()
        {
            
        }


        public virtual void Initialize()
        {   
            this.guid        = Guid.NewGuid();
            this.currState   = 0;
            this.enabled     = true;
        }


        public virtual void Update(GameTime gameTime)
        {         
        }

        
        public virtual void ProcessInput(GameTime gameTime, GamePadState padState)
        {
        }



        #region Load Routines

        public void Load(string filename, string node)
        {
            XmlDocument xml = new XmlDocument();

            xml.Load(System.IO.Path.Combine(AssetsManager.AssetsPath, filename));
            
            Initialize();
            
            Load(xml["gameEntities"][node]);
        }


        public virtual void Load(XmlNode node)
        {
        }

        #endregion



        #region Messaging Routines

        public virtual void ProcessMessage(GameMessage msg)
        {
        }


        public virtual void RouteMessage(GameMessage msg)
        {

            // If the message target is all or specifically matches this Guid then 
            // process the message

            if (msg.TargetGuid == GameMessage.AllTargets || msg.TargetGuid == this.guid)
                ProcessMessage(msg);

        }


        public virtual void SendMessage(GameMessage msg)
        {
            if (parent != null)
                parent.SendMessage(msg);
        }

        #endregion


        #region State Routines

        /// <summary>
        /// Determines whether or not the entity contains the specified state
        /// </summary>
        /// <param name="state">State to check</param>
        /// <returns>Returns true if the entity contains the specified state</returns>
        
        public bool InState(int state)
        {
            return ((this.state & state) > 0);
        }

        #endregion



        protected virtual void OnStateChanged(int origState)
        {
            if (this.StateChanged != null)
                this.StateChanged(this, null);
        }


        protected void OnEnabledChanged()
        {
            if (this.EnabledChanged != null)
                this.EnabledChanged(this, null);
        }


        protected void OnParentChanged()
        {
            if (this.ParentChanged)
                this.ParentChanged(this, null);
        }
    }
}
