/*using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class Emitter<T> : WorldEntity where T : WorldEntity, new()
    {
        private Rectangle emitRect;
        private int delayMsecs;
        private DateTime lastEmitTime;
        private Type emitType;


        #region Properties
        
        public Rectangle EmitRect
        {
            get
            {
                return emitRect;
            }
            set
            {
                emitRect = value;
            }
        }

        public int DelayMsecs
        {
            get
            {
                return delayMsecs;
            }
            set
            {
                delayMsecs = value;
            }
        }


        public DateTime LastEmitTime
        {
            get
            {
                return lastEmitTime;
            }
        }


        public Type EmitType
        {
            get
            {
                return emitType;
            }
        }
        
        #endregion
        

        #region Constructors

        public Emitter(IWorld world, int delayMSecs) : this(world, delayMSecs, new Rectangle(0, 0, 1, 1)){}

        public Emitter(IWorld world, int delayMSecs, Rectangle emitRect) : base(world)
        {
            this.delayMsecs     = delayMSecs;
            this.emitRect       = emitRect;
            this.lastEmitTime   = DateTime.MinValue;
            this.emitType       = typeof(T);
        }

        #endregion


        #region Emit()

        public virtual bool Emit(EntityGroup<T> entityGroup)
        {
            bool emitted = false;


            if (lastEmitTime.AddMilliseconds(delayMsecs) <= DateTime.Now)
            {
                lastEmitTime = DateTime.Now;

                // Add the new object

                T entity = new T();
                
                if (entityGroup.AddEntity(entity))
                {
                    SetInitialState(entity);
                    emitted = true;
                }
            }

            return emitted;
        }

        #endregion


        #region SetInitialState(entity)

        protected virtual void SetInitialState(T entity)
        {
            entity.WorldParams.Position.X = this.WorldParams.Position.X + emitRect.X + GameEngine2D.Randomizer.Next(emitRect.Width);
            entity.WorldParams.Position.Y = this.WorldParams.Position.Y + emitRect.Y + GameEngine2D.Randomizer.Next(emitRect.Height);
            entity.WorldParams.Position.Z = this.WorldParams.Position.Z;
        }

        #endregion

    }
}*/
