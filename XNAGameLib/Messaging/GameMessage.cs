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
    public class GameMessage
    {

        #region Properties

        public int MessageType  { get; set; }
        public GameEntity Sender { get; set; }
        public Guid TargetGuid { get; set; }
        public object State { get; set; }
        public DateTime TimeToDeliver { get; set; }
        public bool ContinueRouting { get; set; }

        static public Guid AllTargets { get { return Guid.Empty; } }

        #endregion


        #region Constructors

        public GameMessage(GameEntity sender)
        {
            this.MessageType        = 0;
            this.Sender             = sender;
            this.TargetGuid         = GameMessage.AllTargets;
            this.State              = null;
            this.TimeToDeliver      = DateTime.Now;
            this.ContinueRouting    = true;
        }


        public GameMessage(GameEntity sender, int messageType, Guid targetGuid, object state, DateTime timeToDeliver)
        {
            this.MessageType        = messageType;
            this.Sender             = sender;
            this.TargetGuid         = targetGuid;
            this.State              = state;
            this.TimeToDeliver      = timeToDeliver;
            this.ContinueRouting    = true;
        }

        #endregion

    }
}
