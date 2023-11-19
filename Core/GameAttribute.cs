using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using XNAGameLib2D.Utilities;

 
namespace XNAGameLib2D
{

    public enum GameAttrModType
    {
        Add         = 0,
        Multiply    = 1
    }


    public class GameAttribute : ICloneable 
    {
        private float rawVal;
        private float minVal;
        private float maxVal;
        private float modifier;
        private GameAttrModType gameAttrModType;
        private DateTime modifierTime;


        #region Properties

        public float CurrVal
        {
            get
            {
                switch (gameAttrModType)
                {           
                    case GameAttrModType.Multiply:
                        return rawVal * modifier;
                
                    case GameAttrModType.Add:
                    default:
                        return rawVal + modifier;
                }
            }
        }


        public float RawVal
        {
            get
            {
                return rawVal;
            }
            set
            {
                rawVal = MathHelper.Clamp(value, minVal, maxVal);
            }
        }


        public float Min
        {
            get
            {
                return minVal;
            }
            set
            {
                minVal = value;
            }
        }


        public float Max
        {
            get
            {
                return maxVal;
            }
            set
            {
                maxVal = value;
            }
        }


        public DateTime ModifierTime
        {
            get
            {
                return modifierTime;
            }
        }

        #endregion


        #region SetModifier(modifier, type)

        public void SetModifier(float modifier, GameAttrModType type)
        {
            this.modifier           = modifier;
            this.gameAttrModType    = type;
            this.modifierTime       = DateTime.Now;
        }

        #endregion


        #region Constructors

        public GameAttribute() : this(0f, 1f){}
           
        public GameAttribute(float minVal, float maxVal) : this(0f, minVal, maxVal){}
     
        public GameAttribute(float rawVal, float minVal, float maxVal) : this(rawVal, minVal, maxVal, 0f, GameAttrModType.Add, DateTime.Now){}
              
        protected GameAttribute(float rawVal, float minVal, float maxVal, float modifier, GameAttrModType gameAttrModType, DateTime modifierTime)
        {
            this.rawVal             = rawVal;
            this.minVal             = minVal;
            this.maxVal             = maxVal;
            this.modifier           = modifier;
            this.gameAttrModType    = gameAttrModType;
            this.modifierTime       = modifierTime;
        }

        #endregion


        #region Load Routines

        public void Load(XmlNode node)
        {
            this.rawVal            = (float)Convert.ToDouble(XmlUtil.GetNodeAttribute(node, "val", "0"));
            this.minVal            = (float)Convert.ToDouble(XmlUtil.GetNodeAttribute(node, "min", "0"));
            this.maxVal            = (float)Convert.ToDouble(XmlUtil.GetNodeAttribute(node, "max", "1"));
            this.modifier          = (float)Convert.ToDouble(XmlUtil.GetNodeAttribute(node, "modifier", "0"));
            this.gameAttrModType   = XmlUtil.GetNodeText(node, "modifiertype", "add").ToUpper() == "MULTIPLY" ? GameAttrModType.Multiply : GameAttrModType.Add;
        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new GameAttribute(this.rawVal, this.minVal, this.maxVal, this.modifier, this.gameAttrModType, this.modifierTime);
        }

        #endregion

    }
}
