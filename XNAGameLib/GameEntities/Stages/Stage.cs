using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class Stage : GameEntity 
    {
        protected GameMessageQueue messageQueue;


        public Stage() : base()
        {
            messageQueue = new GameMessageQueue();
        }


        #region Draw(graphics, gameTime)

        public virtual void Draw(GraphicsDevice graphics, GameTime gameTime)
        {
        }

        #endregion


        #region Message Routines

        public void SendMessage(GameMessage msg)
        {
           
            // If the time to deliver is immediate then call RouteMessage for instant processing
            // otherwise enqueue the message for later processing
           
            if (msg.TimeToDeliver <= DateTime.Now)
                RouteMessage(msg);
            else
                EnqueueMessage(msg);

        }


        public void EnqueueMessage(GameMessage msg)
        {
            messageQueue.EnqueueMessage(msg);
        }

        #endregion

    }
}
