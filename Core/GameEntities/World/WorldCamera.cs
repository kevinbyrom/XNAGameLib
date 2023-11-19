using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class WorldCamera<TWorld> : WorldEntity<TWorld> where TWorld : World
    {
        private float zoom;


        #region Properties
        
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }

        #endregion


        #region Constructors

        public WorldCamera() : base() {} 

        #endregion


        #region Initialize()

        public override void Initialize()
        {
            base.Initialize();

            zoom = 1f;
        }

        #endregion

        /*
        #region Update(gameTime)

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            // Follow the tracking objects

            if (targets.Count > 0)
            {
                float x = 0, y = 0;

                foreach (WorldEntity obj in targets)
                {
                    x += obj.WorldParams.Position.X;
                    y += obj.WorldParams.Position.Y;
                }

                this.WorldParams.Position.X = x / targets.Count;
                this.WorldParams.Position.Y = y / targets.Count;
            }


            // Make sure we do not exceed bounds

            this.WorldParams.Position.X = MathHelper.Clamp(this.WorldParams.Position.X, halfViewWidth / this.ScreenParams.Scale, this.World.Width - (halfViewWidth / this.ScreenParams.Scale));
            this.WorldParams.Position.Y = MathHelper.Clamp(this.WorldParams.Position.Y, halfViewHeight / this.ScreenParams.Scale, this.World.Height - (halfViewHeight / this.ScreenParams.Scale));
        }

        #endregion


        #region ConvertScreenParams(obj)

        public ScreenEntityParams ConvertScreenParams(WorldEntity obj)
        {
            ScreenEntityParams converted = (ScreenEntityParams)obj.ScreenParams.Clone();

            converted.Position.X    = ((obj.WorldParams.Position.X - this.WorldParams.Position.X) * this.ScreenParams.Scale) + halfViewWidth;
            converted.Position.Y    = ((obj.WorldParams.Position.Y - this.WorldParams.Position.Y) * this.ScreenParams.Scale) + halfViewHeight;
            converted.Scale         = obj.WorldParams.Scale * this.ScreenParams.Scale;
            
            return converted;
        }

        #endregion


        #region IsInVisibleRange(rect)

        public bool IsInVisibleRange(Rectangle rect)
        {
            Rectangle cameraRect    = new Rectangle((int)(this.WorldParams.Position.X - (this.ScreenParams.Scale * halfViewWidth)), (int)(this.WorldParams.Position.Y - (this.ScreenParams.Scale * halfViewHeight)), (int)(this.ScreenParams.Scale * viewWidth), (int)(this.ScreenParams.Scale * viewHeight));
            bool inRange            = true;

            if ((rect.Left < cameraRect.Left && rect.Right < cameraRect.Left) ||
                (rect.Left > cameraRect.Right && rect.Right > cameraRect.Right) ||
                (rect.Top < cameraRect.Top && rect.Bottom < cameraRect.Top) ||
                (rect.Top > cameraRect.Bottom && rect.Bottom > cameraRect.Bottom))
                inRange = false;

            return inRange;
        }

        #endregion
        */
    }
}
