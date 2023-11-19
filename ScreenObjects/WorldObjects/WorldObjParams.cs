using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class WorldObjParams : GameObject, ICloneable 
    {
        public Vector3 Size;
        public Vector3 Position;
        public Vector3 Local;
        public Vector2 Normal;
        public Vector2 Velocity;
        public float Rotation;
        public GameAttribute Accel;
        public GameAttribute Friction;
        public GameAttribute MaxSpeed;
        public GameAttribute MaxRotateSpeed;
        public float Scale;
        public bool UseCamera;


        #region Constructors

        public WorldObjParams() : this(Vector3.Zero, Vector3.Zero, Vector3.Zero, new Vector2(1, 0), Vector2.Zero, 0, new GameAttribute(), new GameAttribute(), new GameAttribute(), new GameAttribute(), 1f, true) { }
        
        public WorldObjParams(Vector3 size, Vector3 position, Vector3 local, Vector2 normal, Vector2 velocity, float rotation, GameAttribute accel, GameAttribute friction, GameAttribute maxspeed, GameAttribute maxrotatespeed, float scale, bool usecamera)
        {
            this.Size           = size;
            this.Position       = position;
            this.Local          = local;
            this.Normal         = normal;
            this.Velocity       = velocity;
            this.Rotation       = rotation;
            this.Accel          = (GameAttribute)accel.Clone();
            this.Friction       = (GameAttribute)friction.Clone();
            this.MaxSpeed       = (GameAttribute)maxspeed.Clone();
            this.MaxRotateSpeed = (GameAttribute)maxrotatespeed.Clone();
            this.Scale          = scale;
            this.UseCamera      = usecamera;
        }

        #endregion


        #region Load Routines

        public override void Load(XmlNode node)
        {
            Rotation    = (float)Convert.ToDouble(XmlUtil.GetNodeText(node, "rotation", "0"));
            Scale       = (float)Convert.ToDouble(XmlUtil.GetNodeText(node, "scale", "1"));
            UseCamera   = Convert.ToBoolean(XmlUtil.GetNodeText(node, "usecamera", "true"));

            if (node["size"] != null)
                Size = XmlUtil.GetNodeVector3(node["size"]);

            if (node["position"] != null)
                Position = XmlUtil.GetNodeVector3(node["position"]);

            if (node["local"] != null)
                Local = XmlUtil.GetNodeVector3(node["local"]);

            if (node["normal"] != null)
                Normal = XmlUtil.GetNodeVector2(node["normal"]);

            if (node["velocity"] != null)
                Velocity = XmlUtil.GetNodeVector2(node["velocity"]);

            if (node["accel"] != null)
                Accel.Load(node["accel"]);

            if (node["friction"] != null)
                Friction.Load(node["friction"]);

            if (node["maxspeed"] != null)
                MaxSpeed.Load(node["maxspeed"]);

            if (node["maxrotatespeed"] != null)
                MaxRotateSpeed.Load(node["maxrotatespeed"]);
        }

        #endregion


        #region Clone()

        public object Clone()
        {
            return new WorldObjParams(Size, Position, Local, Normal, Velocity, Rotation, Accel, Friction, MaxSpeed, MaxRotateSpeed, Scale, UseCamera);
        }

        #endregion

    }
}
