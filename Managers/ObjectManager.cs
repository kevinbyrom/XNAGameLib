using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace XNAGameLib2D
{
    public class ObjectManager<T> where T : WorldObject 
    {
        protected List<T> m_Objects;
        protected int m_MaxObjects;
        protected DateTime m_LastUpdateTime;
        protected Thread m_Thread;


        #region Public Properties

        public int MaxObjects
        {
            get
            {
                return m_MaxObjects;
            }
            set
            {
                m_MaxObjects = value;
            }
        }

        #endregion


        public ObjectManager(int maxobjects, bool threaded)
        {
            m_MaxObjects        = maxobjects;
            m_Objects           = new List<T>(maxobjects);
            m_LastUpdateTime    = DateTime.Now;
            
            if (threaded)
                StartUpdateThread();
        }


        #region Update(gametime)

        public virtual void Update(GameTime gametime)
        {
            lock(m_Objects)
            {

                // Update each object and remove the dead ones

                for (int i = m_Objects.Count - 1; i >=0; i--)
                {
                    T obj = m_Objects[i];

                    obj.Update(gametime);

                    if (!obj.IsAlive)
                        m_Objects.Remove(obj);
                }
            }
        }

        #endregion


        #region Draw Routines

        public virtual void Draw(SpriteBatch spritebatch, GameTime gametime)
        {
            lock(m_Objects)
            {
                foreach (T obj in m_Objects)
                    obj.Draw(spritebatch, gametime);
            }
        }

        public virtual void DrawInView(WorldView view, SpriteBatch spritebatch, GameTime gametime)
        {
            lock(m_Objects)
            {
                foreach (T obj in m_Objects)
                {
                    obj.DrawInView(view, spritebatch, gametime);
                }
            }
        }

        #endregion


        #region Add / Remove Object Routines

        public bool AddObject(T obj)
        {
            bool objectadded = false;

            lock(m_Objects)
            {
                if (m_Objects.Count < m_MaxObjects)
                {
                    m_Objects.Add(obj);
                    objectadded = true;
                }
            }

            return objectadded;
        }


        public void RemoveObject(T obj)
        {
            lock(m_Objects)
            {
                m_Objects.Remove(obj);
            }
        }

        #endregion


        #region Thread Routines

        public void StartUpdateThread()
        {
            if (m_Thread == null)
            {
                m_Thread                = new Thread(new ThreadStart(ThreadProc));
                m_Thread.IsBackground   = true;
                m_Thread.Start();
            }
        }


        public void StopUpdateThread()
        {
            if (m_Thread != null)
            {
                m_Thread.Abort();
                m_Thread = null;
            }
        }


        private void ThreadProc()
        {
            try
            {
                while (true)
                {

                    // Determine the time elapsed and create a gametime

                    TimeSpan elapsed = DateTime.Now - m_LastUpdateTime;
                    m_LastUpdateTime = DateTime.Now;

                    GameTime gametime = new GameTime(elapsed, elapsed, elapsed, elapsed);


                    // Update the objects

                    Update(gametime);


                    // Sleep a bit to allow to free cpu

                    Thread.Sleep(20);

                }
            }
            catch(ThreadAbortException)
            {
                Thread.ResetAbort();
            }
        }

        #endregion
        
    }
}
