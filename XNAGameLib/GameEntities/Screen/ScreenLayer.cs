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

        public ScreenLayer(Vector2 screenPos, Vector2 screenSize) : base(screenPos, screenSize) {}

        #endregion


        #region Initialize()

        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion


        #region Draw Routines

        public override void Draw(GameTime gameTime)
        {
            
            // Setup a new viewport based on the screen props

            Viewport thisViewport = GameEngine2D.Instance.GraphicsManager.CurrentViewport;

            thisViewport.X      = (int)this.ScreenPos.X;
            thisViewport.Y      = (int)this.ScreenPos.Y;
            thisViewport.Width  = (int)this.ScreenSize.X;
            thisViewport.Height = (int)this.ScreenSize.Y;

            GameEngine2D.Instance.GraphicsManager.PushViewport(thisViewport);


            // Draw the entities

            DrawContent(gameTime);


            // Revert back to previous viewport

            GameEngine2D.Instance.GraphicsManager.PopViewport();

        }


        public virtual void DrawContent(GameTime gameTime)
        {
        }


        public virtual void DrawSprite(Sprite sprite, SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, screenPos, SpriteRect, this.color, Rotation, Origin, Scale, Effects, Depth);
        }

        #endregion

    }
}
