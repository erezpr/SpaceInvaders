using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceInvaders.Screens.PlayGameScreens
{
    class TransitionScreen : GameScreen
    {
        public int LevelNumber { get; set; } = 1;
        private Texture2D m_Background;
        private SpriteFont m_Bauhaus93Font;
        private int m_SecondsLeft = 2;
        private TimeSpan m_TimeLeft = TimeSpan.FromSeconds(1);
        private readonly PlayScreen r_PlayScreen;

        public TransitionScreen(Game i_Game, PlayScreen i_PlayScreen) : base(i_Game)
        {
            r_PlayScreen = i_PlayScreen;
        }


        protected override void LoadContent()
        {
            base.LoadContent();
            m_Bauhaus93Font = this.Game.Content.Load<SpriteFont>(@"Fonts\Bauhaus93");
            m_Background = this.Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
        }

        public override void Update(GameTime i_GameTime)
        {
            if (r_PlayScreen.IsGameOver == false)
            {
                if (m_SecondsLeft > 0)
                {
                    m_TimeLeft -= i_GameTime.ElapsedGameTime;
                    if (m_TimeLeft.TotalSeconds < 0)
                    {
                        m_SecondsLeft--;
                        m_TimeLeft += TimeSpan.FromSeconds(1);
                    }
                }
                else
                {
                    resetTimer();
                    LevelNumber++;
                    ScreensManager.SetCurrentScreen(r_PlayScreen);
                }
            }
            else
            {
                ExitScreen();
            }

            base.Update(i_GameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(m_Background, Vector2.Zero, Color.White);
            this.SpriteBatch.DrawString(m_Bauhaus93Font,"Level Number: " + LevelNumber.ToString(), new Vector2(105, 105), Color.White);
            this.SpriteBatch.DrawString(m_Bauhaus93Font, m_SecondsLeft.ToString(), new Vector2(200, 275), Color.White);
            this.SpriteBatch.End();
            base.Draw(gameTime);
        }

        private void resetTimer()
        {
            m_SecondsLeft = 3;
        }
    }
}
