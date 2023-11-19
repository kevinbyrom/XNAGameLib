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
    public class WorldObject : ScreenObject
    {
        protected WorldObjParams m_WorldParams;
        protected List<Modifier<WorldObjParams>> m_WorldModifiers;
        protected World m_World;
        protected bool m_IsAlive;
        protected int m_AIState;
        protected List<WorldObject> m_AITargets;


        #region Public Properties

        public WorldObjParams WorldParams
        {
            get
            {
                return m_WorldParams;
            }
        }

        public bool IsAlive
        {
            get
            {
                return m_IsAlive;
            }
        }

        public int AIState
        {
            get
            {
                return m_AIState;
            }
        }

        public List<WorldObject> AITarget
        {
            get
            {
                return m_AITargets;
            }
        }

        public List<Modifier<WorldObjParams>> WorldModifiers
        {
            get
            {
                return m_WorldModifiers;
            }
        }

        #endregion


        #region Constructors

        public WorldObject(World world)
        {
            m_World             = world;
            m_WorldModifiers    = new List<Modifier<WorldObjParams>>();
            m_WorldParams       = new WorldObjParams();
            m_AIState           = 0;
            m_AITargets         = new List<WorldObject>();
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {
            base.Load(node);

            m_AIState = Convert.ToInt32(XmlUtil.GetNodeText(node, "aistate", "0"));

            if (node["worldparams"] != null)
                m_WorldParams.Load(node["worldparams"]);
        }

        #endregion


        #region Initialize(graphics)

        public override void Initialize()
        {
            base.Initialize();

            m_WorldParams.Position.X = 100;
            m_WorldParams.Position.Y = 100;
        }

        #endregion


        #region Update(gametime)

        public override void Update(GameTime gametime)
        {
            float elapsed = ((float)gametime.ElapsedGameTime.Milliseconds / 1000);


            // Call the base update

            base.Update(gametime);


            // Change the velocity based on the thrust

            if (m_WorldParams.Accel.Val > 0f)
            {

                // Calculate the target velocity

                Vector2 target;

                target.X = m_WorldParams.Normal.X * m_WorldParams.MaxSpeed.Val;
                target.Y = m_WorldParams.Normal.Y * m_WorldParams.MaxSpeed.Val;

                float highx = Math.Max(Math.Abs(target.X), Math.Abs(m_WorldParams.Velocity.X));
                float highy = Math.Max(Math.Abs(target.Y), Math.Abs(m_WorldParams.Velocity.Y));

                m_WorldParams.Velocity.X = MathHelper.Clamp(m_WorldParams.Velocity.X + (target.X * m_WorldParams.Accel.Val * elapsed), -highx, highx);
                m_WorldParams.Velocity.Y = MathHelper.Clamp(m_WorldParams.Velocity.Y + (target.Y * m_WorldParams.Accel.Val * elapsed), -highy, highy);
            }


            // Add some friction

            m_WorldParams.Velocity.X = m_WorldParams.Velocity.X * (1f - m_WorldParams.Friction.Val);
            m_WorldParams.Velocity.Y = m_WorldParams.Velocity.Y * (1f - m_WorldParams.Friction.Val);


            // Adjust the position, scale and rotation

            m_WorldParams.Position.X += elapsed * m_WorldParams.Velocity.X;
            m_WorldParams.Position.Y += elapsed * m_WorldParams.Velocity.Y;     


            // Update the modifiers

            if (m_WorldModifiers.Count > 0)
                ProcessWorldModifiers(gametime);
        }
        

        public virtual void Update(WorldObject parent, GameTime gametime)
        {
            float elapsed = ((float)gametime.ElapsedGameTime.Milliseconds / 1000);
                        
            
            // Call the base update

            base.Update(gametime);


            // Set the position relative to the parent

            m_WorldParams.Position.X = parent.WorldParams.Position.X + m_WorldParams.Local.X;
            m_WorldParams.Position.Y = parent.WorldParams.Position.Y + m_WorldParams.Local.Y;

        }
        
        #endregion


        #region ProcessWorldModifiers(gametime)

        private void ProcessWorldModifiers(GameTime gametime)
        {
            foreach (Modifier<WorldObjParams> modifier in m_WorldModifiers)
                if (!modifier.IsDone)
                    modifier.Update(m_WorldParams, gametime);
        }

        #endregion


        #region ProcessAI(gametime)

        public virtual void ProcessAI(GameTime gametime)
        {
        }

        #endregion


        #region Draw Routines

        public virtual void DrawInView(WorldView view, SpriteBatch spritebatch, GameTime gametime)
        {
            if (m_WorldParams.UseCamera)
                m_ScreenParams = view.WorldToScreen(this);

            Draw(spritebatch, gametime);
        }


        public override void Draw(SpriteBatch spritebatch, GameTime gametime)
        {
         
            // If this object is tied to the camera, get the screen params

            //if (m_WorldParams.UseCamera)
                //m_ScreenParams = m_World.Camera.ConvertScreenParams(this);


            // Adjust the rotation

            m_ScreenParams.Rotation = TrigHelper.VectorToRadians(m_WorldParams.Normal);

            
            // Call the base draw

            //if (m_World.Camera.IsInVisibleRange(screenparams.GetScreenRect()))
                base.Draw(spritebatch, gametime);
        }

        #endregion


        #region AIState Routines

        public bool InAIState(int state)
        {
            return ((m_AIState & state) > 0);
        }

        public void SetAIState(int state)
        {
            m_AIState = state;
        }

        public void AddAIState(int state)
        {
            m_AIState |= state;
        }

        public void RemoveAIState(int state)
        {
            m_AIState &= ~state;
        }

        #endregion


        #region RotateToVector(destvector, gametime)

        public void RotateToVector(Vector2 destvector, GameTime gametime)
        {
            float elapsed       = ((float)gametime.ElapsedGameTime.Milliseconds / 1000);
            float destradians   = TrigHelper.VectorToRadians(destvector);
            float radians1      = TrigHelper.VectorToRadians(m_WorldParams.Normal);
            float radians2      = radians1 + MathHelper.TwoPi;
            float dist1         = destradians - radians1;
            float dist2         = destradians - radians2;
            float maxradians    = m_WorldParams.MaxRotateSpeed.Val * elapsed;

            
            if (Math.Abs(dist1) <= maxradians || Math.Abs(dist2) <= maxradians)
                m_WorldParams.Normal = TrigHelper.RadiansToVector(TrigHelper.VectorToRadians(destvector));
            else if (Math.Abs(destradians - radians1) < Math.Abs(destradians - radians2))
                m_WorldParams.Normal = TrigHelper.RadiansToVector(radians1 + (dist1 > 1 ? maxradians : -maxradians));           
            else
                m_WorldParams.Normal = TrigHelper.RadiansToVector(radians2 + (dist2 > 1 ? maxradians : -maxradians));
        }

        #endregion

    }
}
