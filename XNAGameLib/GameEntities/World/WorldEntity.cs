using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics; 
using XNAGameLib2D.Utilities;


namespace XNAGameLib2D
{
    public class WorldEntity<TWorld> : SpriteEntity where TWorld : World
    {
        public TWorld World;
        public Vector3 WorldPos;
        public Vector3 Size;
        public Vector3 LocalPos;
        public Vector3 Normal;
        public Vector3 Velocity;
        public GameAttribute Accel;
        public GameAttribute Mass;
        public GameAttribute MaxSpeed;
        public GameAttribute MaxRotateSpeed;
        public List<WorldEntityBound> Bounds;
        private bool isAlive;
        private int aiState;
        
        public event EventHandler WorldPosChanged;
        public event EventHandler StateChanged;
        public event EventHandler Died;
 

        #region Properties

        public bool IsAlive
        {
            get { return isAlive; }
        }


        public int AIState
        {
            get { return aiState; }
            set { aiState = value; }
        }

        #endregion


        #region Constructors

        public WorldEntity() : this(null) {}

        public WorldEntity(TWorld world) : base()
        {
            World = world;
        }

        #endregion


        #region Initialize()

        public override void Initialize()
        {
            base.Initialize();

            WorldPos        = new Vector3();
            Size            = new Vector3();
            LocalPos        = new Vector3();
            Normal          = new Vector3(1,0,0);
            Velocity        = new Vector3();
            Accel           = new GameAttribute();
            Mass            = new GameAttribute();
            MaxSpeed        = new GameAttribute();
            MaxRotateSpeed  = new GameAttribute();
            Bounds          = new List<WorldEntityBound>();
            
            this.isAlive    = true;
            this.aiState    = 0;
        }

        #endregion

        
        #region AI Routines

        public virtual void ProcessAI(GameTime gameTime)
        {
        }


        public bool InAIState(int state)
        {
            return ((aiState & state) > 0);
        }


        public void SetAIState(int state)
        {
            aiState = state;
        }


        public void AddAIState(int state)
        {
            aiState |= state;
        }


        public void RemoveAIState(int state)
        {
            aiState &= ~state;
        }

        #endregion


        #region RotateToVector(destVector, gameTime)

        public void RotateToVector(Vector2 destVector, GameTime gameTime)
        {
            /*float elapsed       = ((float)gameTime.ElapsedGameTime.Milliseconds / 1000);
            float destRadians   = TrigHelper.Vector2ToRadians(destVector);
            float radians1      = TrigHelper.Vector2ToRadians(normal);
            float radians2      = radians1 + MathHelper.TwoPi;
            float dist1         = destRadians - radians1;
            float dist2         = destRadians - radians2;
            float maxRadians    = maxRotateSpeed.CurrVal * elapsed;

            
            if (Math.Abs(dist1) <= maxRadians || Math.Abs(dist2) <= maxRadians)
                normal = TrigHelper.RadiansToVector2(TrigHelper.Vector2ToRadians(destVector));
            else if (Math.Abs(destRadians - radians1) < Math.Abs(destRadians - radians2))
                normal = TrigHelper.RadiansToVector2(radians1 + (dist1 > 1 ? maxRadians : -maxRadians));           
            else
                normal = TrigHelper.RadiansToVector2(radians2 + (dist2 > 1 ? maxRadians : -maxRadians));*/
        }

        #endregion


        #region CheckCollision(entity)

        public bool CheckCollision(WorldEntity<TWorld> target)
        {
            
            // Check if our bounds intersects the target's

            foreach (WorldEntityBound myBound in this.Bounds)
            {
                foreach (WorldEntityBound hisBound in target.Bounds)
                {
                    if (myBound.Intersects(hisBound))
                        return true;
                }
            }

            return false;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {
            base.Load(node);

            XmlNode worldNode = node["world"];

            if (worldNode != null)
            {
                aiState = Convert.ToInt32(XmlUtil.GetNodeText(worldNode, "aiState", "0"));
     
                if (worldNode["size"] != null)
                    Size = XmlUtil.GetNodeVector3(worldNode["size"]);

                if (worldNode["localPos"] != null)
                    LocalPos = XmlUtil.GetNodeVector3(worldNode["localPos"]);

                if (worldNode["normal"] != null)
                    Normal = XmlUtil.GetNodeVector3(worldNode["normal"]);

                if (worldNode["velocity"] != null)
                    Velocity = XmlUtil.GetNodeVector3(worldNode["velocity"]);

                if (worldNode["accel"] != null)
                    Accel.Load(worldNode["accel"]);

                if (worldNode["mass"] != null)
                    Mass.Load(worldNode["mass"]);

                if (worldNode["maxSpeed"] != null)
                    MaxSpeed.Load(worldNode["maxSpeed"]);

                if (worldNode["maxRotateSpeed"] != null)
                    MaxRotateSpeed.Load(worldNode["maxRotateSpeed"]);

                if (worldNode["boundings"] != null)
                {
                    foreach (XmlNode boundingNode in worldNode["boundings"].SelectNodes("bounding"))
                    {
                        WorldEntityBound entityBound = new WorldEntityBound();
                        
                        entityBound.Load(boundingNode);

                        Bounds.Add(entityBound);
                    }
                }

            }
        }

        #endregion


    }
}
