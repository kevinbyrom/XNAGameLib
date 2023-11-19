using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class Emitter<T> : WorldObject where T : WorldObject
    {

        protected Random m_Rnd;
        protected Rectangle m_EmitRect;
        protected int m_DelayMsecs;
        protected DateTime m_LastEmitTime;
        protected ObjectManager<T> m_ObjManager;


        #region Public Properties
        
        public Rectangle EmitRect
        {
            get
            {
                return m_EmitRect;
            }
            set
            {
                m_EmitRect = value;
            }
        }

        public int DelayMsecs
        {
            get
            {
                return m_DelayMsecs;
            }
            set
            {
                m_DelayMsecs = value;
            }
        }

        public ObjectManager<T> ObjectManager
        {
            get
            {
                return m_ObjManager;
            }
            set
            {
                m_ObjManager = value;
            }
        }

        #endregion
        

        #region Constructors

        public Emitter(World world, int delaymsecs) : this(world, delaymsecs, new Rectangle(0, 0, 1, 1)){}

        public Emitter(World world, int delaymsecs, Rectangle emitrect) : base(world)
        {
            m_DelayMsecs    = delaymsecs;
            m_EmitRect      = emitrect;
            m_LastEmitTime  = DateTime.MinValue;
            m_ObjManager    = null;
            m_Rnd           = new Random();
        }

        #endregion


        #region Update(gametime)

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            // Call the manager update method

            if (m_ObjManager != null)
                m_ObjManager.Update(gametime);
        }

        #endregion


        #region Draw Routines

        public override void DrawInView(WorldView view, SpriteBatch spritebatch, GameTime gametime)
        {
 
            // Call the manager draw method

            if (m_ObjManager != null)
                m_ObjManager.DrawInView(view, spritebatch, gametime);

        }


        public override void Draw(SpriteBatch spritebatch, GameTime gametime)
        {
           
            // Call the manager draw method

            if (m_ObjManager != null)
                m_ObjManager.Draw(spritebatch, gametime);

        }

        #endregion


        #region Emit(obj)

        public virtual void Emit(T obj)
        {
            if (m_LastEmitTime.AddMilliseconds(m_DelayMsecs) <= DateTime.Now)
            {
                m_LastEmitTime = DateTime.Now;

                // Add the new object

                SetInitialState(obj);
                m_ObjManager.AddObject(obj);
               
            }
        }

        #endregion


        #region SetInitialState(obj)

        protected virtual void SetInitialState(WorldObject obj)
        {
            obj.WorldParams.Position.X = m_WorldParams.Position.X + m_EmitRect.X + m_Rnd.Next(m_EmitRect.Width);
            obj.WorldParams.Position.Y = m_WorldParams.Position.Y + m_EmitRect.Y + m_Rnd.Next(m_EmitRect.Height);
            obj.WorldParams.Position.Z = m_WorldParams.Position.Z;
        }

        #endregion

    }
}
