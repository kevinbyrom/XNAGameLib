using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{

    public enum SpritePlayMode
    {
        Auto    = 0,
        Manual  = 1
    }


    public enum SpriteRepeatMode
    {
        PlayOnce    = 0,
        Repeat      = 1
    }



    public class SpriteAnimation : GameEntity, ICloneable
    {
        private Dictionary<string, List<SpriteAnimFrame>> frameReels;
        private List<SpriteAnimFrame> currFrameReel;
        private int currFramePos;
        private int remainingFrameMSecs;
        private SpritePlayMode playMode;
        private SpriteRepeatMode repeatMode;


        #region Properties

        public Dictionary<string, List<SpriteAnimFrame>> FrameReels
        {
            get
            {
                return frameReels;
            }
            protected set
            {
                frameReels = value;
            }
        }
        

        public List<SpriteAnimFrame> CurrFrameReel
        {
            get
            {
                return currFrameReel;
            }
            protected set
            {
                currFrameReel = value;
            }
        }


        public SpriteAnimFrame CurrFrame
        {
            get
            {
                if (CurrFrameReel != null)
                    return CurrFrameReel[currFramePos];
                else
                    return null;
            }
        }


        public int RemainingFrameMSecs
        {
            get
            {
                return remainingFrameMSecs;
            }
            protected set
            {
                remainingFrameMSecs = value;
            }
        }


        public SpritePlayMode PlayMode
        {
            get
            {
                return playMode;
            }
            set
            {
                playMode = value;
            }
        }


        public SpriteRepeatMode RepeatMode
        {
            get
            {
                return repeatMode;
            }
            set
            {
                repeatMode = value;
            }
        }
        
        #endregion


        #region Constructors

        public SpriteAnimation() : this(new Dictionary<string, List<SpriteAnimFrame>>()) {}
 
        public SpriteAnimation(Dictionary<string, List<SpriteAnimFrame>> frameReels) : this(false, frameReels, 0, 0, SpritePlayMode.Auto, SpriteRepeatMode.Repeat) {}

        public SpriteAnimation(bool enabled, Dictionary<string, List<SpriteAnimFrame>> frameReels, int currFramePos, int remainingFrameMSecs, SpritePlayMode playMode, SpriteRepeatMode repeatMode)
        {
            this.Enabled        = enabled;
            this.frameReels     = frameReels;
            this.currFramePos   = 0;
            this.playMode       = playMode;
            this.repeatMode     = repeatMode;
            
            MoveToFirstFrame();
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {            
            Enabled = Convert.ToBoolean(XmlUtil.GetNodeAttribute(node, "enabled", "false"));
      

            // Load the frame reels

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.ToLower() == "framereel")
                    LoadFrameReel(child);
            }

        }

        private void LoadFrameReel(XmlNode node)
        {
            string reelName = node.Attributes["name"].InnerText.ToLower();
            List<SpriteAnimFrame> animFrames = new List<SpriteAnimFrame>();
                      

            // Load the anim frames

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.ToLower() == "animframe")
                {
                    SpriteAnimFrame animFrame = new SpriteAnimFrame();

                    animFrame.Load(child);

                    animFrames.Add(animFrame);
                }
            }


            // Add the frame set

            currFrameReel = animFrames;
            frameReels.Add(reelName, animFrames);

        }

        #endregion
        

        #region Update(gameTime)

        public override void Update(GameTime gameTime)
        {
            
            if (Enabled && playMode == SpritePlayMode.Auto)
            {

                // Change the remaining time

                remainingFrameMSecs -= gameTime.ElapsedGameTime.Milliseconds;


                // If this frame is out of time then move to the next frame

                if (remainingFrameMSecs <= 0)
                    MoveToNextFrame();
            
            }
        }

        #endregion


        #region MoveToFrame Routines

        public void MoveToFirstFrame()
        {
            if (CurrFrameReel != null)
                MoveToFrame(0);
        }


        public void MoveToLastFrame()
        {
            if (CurrFrameReel != null)
                MoveToFrame(CurrFrameReel.Count - 1);
        }


        public void MoveToNextFrame()
        {            
            if (CurrFrameReel != null)
            {
                // Move to the next frame

                if (currFramePos < CurrFrameReel.Count - 1)
                {
                    MoveToFrame(currFramePos + 1); 
                }
                else if (repeatMode == SpriteRepeatMode.Repeat)
                {
                    MoveToFirstFrame();
                }
            }
        }


        public void MoveToFrame(int pos)
        {
            if (CurrFrameReel != null)
            {
                currFramePos = pos;
                
                if (CurrFrameReel.Count > 0)
                    remainingFrameMSecs = CurrFrame.TimeMSecs;
                else
                    remainingFrameMSecs = 0;
            }
        }

        #endregion


        #region SetCurrFrameReel Routines

        public void SetCurrFrameReel(string name)
        {
            SetCurrFrameReel(name, SpritePlayMode.Auto, SpriteRepeatMode.Repeat);
        }

        public void SetCurrFrameReel(string name, SpritePlayMode playMode, SpriteRepeatMode repeatMode)
        {
            if (frameReels.ContainsKey(name))
            {                
                                
                // If we are changing to a new reel, start at the first frame

                List<SpriteAnimFrame> destFrameReel = frameReels[name];

                if (currFrameReel != destFrameReel)
                {
                    currFrameReel = destFrameReel;                    
                    MoveToFirstFrame();
                }

                // Set the play and repeat modes

                this.playMode   = playMode;
                this.RepeatMode = repeatMode;

            }
            else
                currFrameReel = null;
        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new SpriteAnimation(Enabled, frameReels, currFramePos, remainingFrameMSecs, playMode, repeatMode);
        }

        #endregion

    }
}
