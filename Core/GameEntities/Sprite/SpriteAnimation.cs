using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{

    public enum SpriteAnimRepeatMode
    {
        PlayOnce    = 0,
        Repeat      = 1
    }



    public class SpriteAnimation : GameEntity, ICloneable
    {
        private int currFramePos;
        private List<SpriteFrame> frames;
        private int currTimeMSecs;
        private int repeatMode;


        #region Properties

        public int CurrFramePos
        {
            get
            {
                return currFramePos;
            }
            set
            {
                currFramePos    = value;
                currTimeMSecs   = 0;
            }
        }


        public List<SpriteFrame> Frames
        {
            get
            {
                return frames;
            }
            set
            {
                frames = value;
            }
        }


        public SpriteFrame CurrFrame
        {
            get
            {
                return frames[currFramePos];
            }
        }


        public int CurrTimeMSecs
        {
            get
            {
                return currTimeMSecs;
            }
            set
            {
                currTimeMSecs = value;
            }
        }


        public int RepeatMode
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

        public SpriteAnimation() : this(new List<SpriteFrame>()) {}
 
        public SpriteAnimation(List<SpriteFrame> frames) : this(true, 0, frames, 0, (int)SpriteAnimRepeatMode.Repeat) {}

        public SpriteAnimation(bool enabled, int currFramePos, List<SpriteFrame> frames, int currTimeMSecs, int repeatMode) : base()
        {
            this.Enabled        = enabled;
            this.currFramePos   = 0;
            this.frames         = frames;
            this.currTimeMSecs  = currTimeMSecs;
            this.repeatMode     = repeatMode;
            
            MoveToFirstFrame();
        }

        #endregion


        #region override Initialize()

        public override void Initialize()
        {
            base.Initialize();

            this.currFramePos   = 0;
            this.frames         = new List<SpriteFrame>();
            this.currTimeMSecs  = 0;
            this.repeatMode     = (int)SpriteAnimRepeatMode.PlayOnce;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {            
                  
            // Load the repeat mode

            repeatMode = Convert.ToInt32(XmlUtil.GetNodeAttribute(node, "repeatMode", "0"));


            // Load the frames

            foreach (XmlNode frameNode in node.SelectNodes("spriteFrame"))
            {
                SpriteFrame frame = new SpriteFrame();

                frame.Load(frameNode);

                frames.Add(frame);
            }
        }

        #endregion
        

        #region Update(gameTime)

        public override void Update(GameTime gameTime)
        {
            
            if (Enabled)
            {

                // Change the current time

                currTimeMSecs += gameTime.ElapsedGameTime.Milliseconds;

                
                // If this frame is out of time then move to the next frame

                if (currTimeMSecs >= CurrFrame.TimeMSecs)
                    MoveToNextFrame();
            
            }
        }

        #endregion


        #region MoveToFrame Routines

        public void MoveToFirstFrame()
        {
            MoveToFrame(0);
        }


        public void MoveToLastFrame()
        {
            MoveToFrame(frames.Count - 1);
        }


        public void MoveToNextFrame()
        {            
            
            // Move to the next frame

            if (currFramePos < frames.Count - 1)
                MoveToFrame(currFramePos + 1); 
            else if (repeatMode == (int)SpriteRepeatMode.Repeat)
                MoveToFirstFrame();

        }


        public void MoveToFrame(int pos)
        {
            currFramePos    = pos;
            currTimeMSecs   = 0;    
        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new SpriteAnimation(Enabled, currFramePos, frames, currTimeMSecs, repeatMode);
        }

        #endregion

    }
}
