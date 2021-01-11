using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpaceInvaders.Screens.PlayGameScreens
{
    public class PauseScreen : GameScreen
    {
        private SpriteFont m_Bauhaus93Font;
        private Vector2 m_MsgPosition = new Vector2(70, 300);

        public PauseScreen(Game i_Game) : base(i_Game)
        {
            this.IsModal = true;
            this.IsOverlayed = true;
            this.UseGradientBackground = true;
            this.BlackTintAlpha = 0.55f;
            this.UseFadeTransition = true;
            this.ActivationLength = TimeSpan.FromSeconds(0.5f);
            this.DeactivationLength = TimeSpan.FromSeconds(0.5f);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Bauhaus93Font = this.Game.Content.Load<SpriteFont>(@"Fonts\Bauhaus93");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.Begin();
            SpriteBatch.DrawString(m_Bauhaus93Font, @"R - Resume Game", m_MsgPosition, Color.White);
            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.R))
            {
                this.ExitScreen();
            }

            m_MsgPosition.X = (float)Math.Pow(70, TransitionPosition);
        }
    }
}
