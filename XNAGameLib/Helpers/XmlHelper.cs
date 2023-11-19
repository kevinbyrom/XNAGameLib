using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D.Utilities
{
    static public class XmlHelper
    {

        static public string TryGetNodeText(XmlNode node, string field, string defval)
        {
            if (node[field] != null)
                return node[field].InnerText;
            else
                return defval;
        }

        static public string TryGetNodeAttribute(XmlNode node, string attr, string defval)
        {
            if (node.Attributes[attr] != null)
                return node.Attributes[attr].InnerText;
            else
                return defval;
        }

        static public Rectangle TryGetNodeRectangle(XmlNode node, Rectangle defVal)
        {
            int x, y, width, height;

            x       = Convert.ToInt32(TryGetNodeAttribute(node, "x", defVal.X.ToString()));
            y       = Convert.ToInt32(TryGetNodeAttribute(node, "y", defVal.Y.ToString()));
            width   = Convert.ToInt32(TryGetNodeAttribute(node, "width", defVal.Width.ToString()));
            height  = Convert.ToInt32(TryGetNodeAttribute(node, "height", defVal.Height.ToString()));
            
            return new Rectangle(x, y, width, height);
        }

        static public Vector2 TryGetNodeVector2(XmlNode node, Vector2 defVal)
        {
            Vector2 vector = new Vector2();

            vector.X = (float)Convert.ToDouble(TryGetNodeAttribute(node, "x", defVal.X.ToString()));
            vector.Y = (float)Convert.ToDouble(TryGetNodeAttribute(node, "y", defVal.Y.ToString()));

            return vector;
        }

        static public Vector3 TryGetNodeVector3(XmlNode node, Vector3 defVal)
        {
            Vector3 vector = new Vector3();

            vector.X = (float)Convert.ToDouble(TryGetNodeAttribute(node, "x", defVal.X.ToString()));
            vector.Y = (float)Convert.ToDouble(TryGetNodeAttribute(node, "y", defVal.Y.ToString()));
            vector.Z = (float)Convert.ToDouble(TryGetNodeAttribute(node, "z", defVal.Z.ToString()));

            return vector;
        }

        static public Vector4 TryGetNodeVector4(XmlNode node, Vector4 defVal)
        {
            Vector4 vector = new Vector4();

            vector.X = (float)Convert.ToDouble(GetNodeAttribute(node, "x", defVal.X.ToString()));
            vector.Y = (float)Convert.ToDouble(GetNodeAttribute(node, "y", defVal.Y.ToString()));
            vector.Z = (float)Convert.ToDouble(GetNodeAttribute(node, "z", defVal.Z.ToString()));
            vector.W = (float)Convert.ToDouble(GetNodeAttribute(node, "w", defVal.W.ToString()));

            return vector;
        }

        static public Color TryGetNodeColor(XmlNode node, Vector4 defVal)
        {
            Vector4 vector = GetNodeVector4(node, defVal);

            return new Color(vector);
        }

    }
}
