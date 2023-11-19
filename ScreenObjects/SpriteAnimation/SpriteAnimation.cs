using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;



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



    public class SpriteAnimation : GameObject, ICloneable
    {
        protected bool m_Enabled;
        protected Dictionary<string, List<SpriteAnimFrame>> m_FrameReels;
        protected List<SpriteAnimFrame> m_CurrFrameReel;
        protected int m_CurrFramePos;
        protected int m_RemainingFrameMSecs;
        protected SpritePlayMode m_PlayMode;
        protected SpriteRepeatMode m_RepeatMode;


        #region Public Properties

        public bool Enabled
        {
            get
            {
                return m_Enabled;
            }
            set
            {
                m_Enabled = value;
            }
        }


        public Dictionary<string, List<SpriteAnimFrame>> FrameReels
        {
            get
            {
                return m_FrameReels;
            }
        }
        

        public List<SpriteAnimFrame> CurrFrameReel
        {
            get
            {
                return m_CurrFrameReel;
            }
        }


        public SpriteAnimFrame CurrFrame
        {
            get
            {
                if (CurrFrameReel != null)
                    return CurrFrameReel[m_CurrFramePos];
                else
                    return null;
            }
        }


        public int RemainingFrameMSecs
        {
            get
            {
                return m_RemainingFrameMSecs;
            }
        }


        public SpritePlayMode PlayMode
        {
            get
            {
                return m_PlayMode;
            }
            set
            {
                m_PlayMode = value;
            }
        }


        public SpriteRepeatMode RepeatMode
        {
            get
            {
                return m_RepeatMode;
            }
            set
            {
                m_RepeatMode = value;
            }
        }
        
        #endregion


        #region Constructors

        public SpriteAnimation() : this(new Dictionary<string, List<SpriteAnimFrame>>()) {}
 
        public SpriteAnimation(Dictionary<string, List<SpriteAnimFrame>> framereels) : this(false, framereels, 0, 0, SpritePlayMode.Auto, SpriteRepeatMode.Repeat) {}

        public SpriteAnimation(bool enabled, Dictionary<string, List<SpriteAnimFrame>> framereels, int currframepos, int remainingframemsecs, SpritePlayMode playmode, SpriteRepeatMode repeatmode)
        {
            m_Enabled       = enabled;
            m_FrameReels    = framereels;
            m_CurrFramePos  = 0;
            m_PlayMode      = playmode;
            m_RepeatMode    = repeatmode;
            
            MoveToFirstFrame();
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {            
            m_Enabled = Convert.ToBoolean(XmlUtil.GetNodeAttribute(node, "enabled", "false"));
      

            // Load the frame reels

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.ToLower() == "framereel")
                    LoadFrameReel(child);
            }

        }

        private void LoadFrameReel(XmlNode node)
        {
            string reelname = node.Attributes["name"].InnerText.ToLower();
            List<SpriteAnimFrame> animframes = new List<SpriteAnimFrame>();
                      

            // Load the anim frames

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.ToLower() == "animframe")
                {
                    SpriteAnimFrame animframe = new SpriteAnimFrame();

                    animframe.Load(child);

                    animframes.Add(animframe);
                }
            }


            // Add the frame set

            m_CurrFrameReel = animframes;
            m_FrameReels.Add(reelname, animframes);

        }

        #endregion
        

        #region Update(gametime)

        public override void Update(GameTime gametime)
        {
            
            if (m_Enabled && m_PlayMode == SpritePlayMode.Auto)
            {

                // Change the remaining time

                m_RemainingFrameMSecs -= gametime.ElapsedGameTime.Milliseconds;


                // If this frame is out of time...

                if (m_RemainingFrameMSecs <= 0)
                {

                    // Move to the next frame

                    MoveToNextFrame();
                }
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

                if (m_CurrFramePos < CurrFrameReel.Count - 1)
                {
                    MoveToFrame(m_CurrFramePos + 1); 
                }
                else if (m_RepeatMode == SpriteRepeatMode.Repeat)
                {
                    MoveToFirstFrame();
                }
            }
        }


        public void MoveToFrame(int pos)
        {
            if (CurrFrameReel != null)
            {
                m_CurrFramePos = pos;
                
                if (CurrFrameReel.Count > 0)
                    m_RemainingFrameMSecs = CurrFrame.TimeMSecs;
                else
                    m_RemainingFrameMSecs = 0;
            }
        }

        #endregion


        #region SetCurrFrameReel Routines

        public void SetCurrFrameReel(string name)
        {
            SetCurrFrameReel(name, SpritePlayMode.Auto, SpriteRepeatMode.Repeat);
        }

        public void SetCurrFrameReel(string name, SpritePlayMode playmode, SpriteRepeatMode repeatmode)
        {
            if (m_FrameReels.ContainsKey(name))
            {                
                                
                // If we are changing to a new reel, start at the first frame

                List<SpriteAnimFrame> destframereel = m_FrameReels[name];

                if (m_CurrFrameReel != destframereel)
                {
                    m_CurrFrameReel = destframereel;                    
                    MoveToFirstFrame();
                }

                // Set the play and repeat modes

                m_PlayMode = playmode;
                m_RepeatMode = repeatmode;

            }
            else
                m_CurrFrameReel = null;
        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new SpriteAnimation(m_Enabled, m_FrameReels, m_CurrFramePos, m_RemainingFrameMSecs, m_PlayMode, m_RepeatMode);
        }

        #endregion

    }
}
