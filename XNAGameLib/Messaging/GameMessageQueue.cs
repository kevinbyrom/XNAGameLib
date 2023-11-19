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
    public class GameMessageQueue
    {

        protected List<GameMessage> messages;


        #region Constructors

        public GameMessageQueue()   
        {
            messages = new List<GameMessage>();
        }

        public GameMessageQueue(int initialSize)   
        {
            messages = new List<GameMessage>(initialSize);
        }

        #endregion


        #region Clear()

        public void Clear()
        {
            messages.Clear();
        }

        #endregion


        #region EnqueueMessage(msg)

        public void EnqueueMessage(GameMessage msg)
        {
            lock(messages)
            {
                messages.Add(msg);
            }
        }

        #endregion


        #region GetNextMessageToDeliver()

        public GameMessage GetNextMessageToDeliver()
        {
            GameMessage msg = null;

            lock(messages)
            {
                
                // Look thru the message list and find the first message that needs to be delivered

                foreach (GameMessage currMsg in messages)
                {
                    if (currMsg.TimeToDeliver <= DateTime.Now)
                    {
                        msg = currMsg;
                        break;
                    }
                }


                // If we have a message to deliver, remove it from the list

                if (msg != null)
                    messages.Remove(msg);
            }

            return msg;
        }

        #endregion

    }
}
