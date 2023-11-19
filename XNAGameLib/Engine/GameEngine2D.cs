using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;


namespace XNAGameLib2D
{
    public class GameEngine2D
    {

        #region Instance

        static private GameEngine2D instance = null;

        static public GameEngine2D Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameEngine2D();

                return instance;
            }
        }

        #endregion

        public GraphicsManager GraphicsManager { get; set; }
        public TextureManager TextureManager { get; set; }
        public AssetsManager AssetsManager { get; set; }


        public GameEngine2D()
        {
            this.GraphicsManager = new GraphicsManager();
            this.TextureManager = new TextureManager();
            this.AssetsManager = new AssetsManager();
        }

    }
}
