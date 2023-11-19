/*using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace XNAGameLib2D
{
    public class EntityGroup<T> : GameEntity where T : GameEntity, new()
    {
        private List<T> entityList;
        private int maxEntities;
        private DateTime lastUpdateTime;
        private Thread updateThread;


        #region Public Properties

        public List<T> EntityList
        {
            get
            {
                return entityList;
            }
        }


        public int MaxEntities
        {
            get
            {
                return maxEntities;
            }
        }


        public DateTime LastUpdateTime
        {
            get
            {
                return lastUpdateTime;
            }
        }

        #endregion


        public EntityGroup(int maxEntities, bool useUpdateThread) : base()
        {
            this.maxEntities        = maxEntities;
            this.entityList         = new List<T>(maxEntities);
            lastUpdateTime          = DateTime.Now;
            
            if (useUpdateThread)
                StartUpdateThread();
        }


        #region Update(gametime)

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            lock(entityList)
            {
                foreach (T entity in entityList)
                    entity.Update(gameTime);
            }
        }

        #endregion


        #region Draw Routines

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            lock(entityList)
            {
                foreach (GameEntity entity in entityList)
                    entity.Draw(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);

            lock(entityList)
            {
                foreach (GameEntity entity in entityList)
                    entity.Draw(spriteBatch, gameTime);
            }
        }

        #endregion


        #region Add / Remove Object Routines

        public bool AddEntity(T entity)
        {
            bool added = false;

            lock(entityList)
            {
                if (entityList.Count < maxEntities)
                {
                    entityList.Add(entity);
                    added = true;
                }
            }

            return added;
        }


        public void RemoveEntity(T entity)
        {
            lock(entityList)
            {
                entityList.Remove(entity);
            }
        }

        #endregion


        #region Update Thread Routines

        public void StartUpdateThread()
        {
            if (updateThread == null)
            {
                updateThread                = new Thread(new ThreadStart(UpdateThreadProc));
                updateThread.IsBackground   = true;
                updateThread.Start();
            }
        }


        public void StopUpdateThread()
        {
            if (updateThread != null)
            {
                updateThread.Abort();
                updateThread = null;
            }
        }


        private void UpdateThreadProc()
        {
            try
            {
                while (true)
                {

                    // Determine the time elapsed and create a gametime

                    TimeSpan elapsed = DateTime.Now - lastUpdateTime;
                    lastUpdateTime = DateTime.Now;

                    GameTime gameTime = new GameTime(elapsed, elapsed, elapsed, elapsed);


                    // Update the objects

                    Update(gameTime);


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
*/