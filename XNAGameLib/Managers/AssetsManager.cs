using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XNAGameLib2D
{
    public class AssetsManager
    {
        public string AssetsPath { get; set; }


        public AssetsManager()
        {
            this.AssetsPath = "";
        }


        static public string GetAssetFilename(string filename)
        {
            return System.IO.Path.Combine(this.AssetsPath, filename); 
        }

    }
}
