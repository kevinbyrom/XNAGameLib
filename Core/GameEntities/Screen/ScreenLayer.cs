using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace XNAGameLib2D
{
    public class ScreenLayer : ScreenEntity
    {

        #region Constructors

        public ScreenLayer(Vector2 screenPos, Rectangle screenSize) : base(screenPos, screenSize) {}

        #endregion


        #region Initialize()

        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion


        #region Draw Routines

        public override void Draw(GraphicsDevice graphics, GameTime gameTime)
        {
            
            // Store the current viewport

            Viewport prevViewport = graphics.Viewport;


            // Setup a new viewport based on the screen props

            Viewport thisViewport = graphics.Viewport;

            thisViewport.X      = (int)this.ScreenPos.X;
            thisViewport.Y      = (int)this.ScreenPos.Y;
            thisViewport.Width  = this.ScreenSize.Width;
            thisViewport.Height = this.ScreenSize.Height;

            graphics.Viewport = thisViewport;


            // Draw the entities

            DrawContent(graphics, gameTime);


            // Revert back to previous viewport

            graphics.Viewport = prevViewport;

        }


        public virtual void DrawContent(GraphicsDevice graphics, GameTime gameTime)
        {
        }

        #endregion

    }
}
