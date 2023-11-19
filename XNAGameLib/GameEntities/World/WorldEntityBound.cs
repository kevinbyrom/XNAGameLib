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
    public enum WorldEntityBoundType
    {
        Sphere = 1,
        Box = 2
    }


    public class WorldEntityBound : GameEntity
    {
        public Vector3 WorldPos;
        public Vector3 LocalPos;
        
        public Vector3 size;
        private int boundType;
        private BoundingSphere sphere;
        private BoundingBox box;


        #region Properties

        public int BoundType
        {
            get
            {
                return boundType;
            }
        }


        public BoundingSphere Sphere
        {
            get
            {
                return sphere;
            }
        }


        public BoundingBox Box
        {
            get
            {
                return box;
            }
        }

        #endregion


        public WorldEntityBound() : base()
        {
            WorldPos    = Vector3.Zero;
            LocalPos    = Vector3.Zero;

            size        = Vector3.Zero;
            boundType   = 0;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            // Perform translations

            switch(boundType)
            {
                case (int)WorldEntityBoundType.Sphere:
                    sphere.Center = WorldPos;
                    sphere.Radius = size.X / 2;
                    break;

                case (int)WorldEntityBoundType.Box:
                    box.Min = new Vector3(WorldPos.X, WorldPos.Y, WorldPos.Z);
                    box.Max = new Vector3(WorldPos.X + size.X, WorldPos.Y + size.Y, WorldPos.Z + size.Z);
                    break;
            }
        }


        #region Intersects(target)

        public bool Intersects(WorldEntityBound target)
        {

            // Check for collisions

            switch(target.BoundType)
            {
                case (int)WorldEntityBoundType.Sphere:
                    if (boundType == (int)WorldEntityBoundType.Sphere && sphere.Intersects(target.Sphere))
                        return true;
                    else if (boundType == (int)WorldEntityBoundType.Box && box.Intersects(target.Sphere))
                        return true;
                    break;

                case (int)WorldEntityBoundType.Box:
                    if (boundType == (int)WorldEntityBoundType.Sphere && sphere.Intersects(target.Box))
                        return true;
                    else if (boundType == (int)WorldEntityBoundType.Box && box.Intersects(target.Box))
                        return true;
                    break;

                default:
                    break;
            }

            return false;
        }

        #endregion


        public override void Load(XmlNode node)
        {
            base.Load(node);

            boundType = Convert.ToInt32(XmlUtil.GetNodeAttribute(node, "boundType", "0"));
            
            if (node["size"] != null)
                size = XmlUtil.GetNodeVector3(node["size"]);

            if (node["localPos"] != null)
                LocalPos = XmlUtil.GetNodeVector3(node["localPos"]);
        }
    }
}
