using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Screens
{
    class TransitionScreen : GameScreen
    {
        private int m_LevelNumber = 1;
        public int LevelNumber { get => m_LevelNumber; set => m_LevelNumber = value; }

        private Texture2D m_Background;
        private IInputManager m_InputManager;
        private SpriteFont m_Bauhaus93Font;
        private int m_SecondsLeft = 3;
        private TimeSpan m_TimeLeft = TimeSpan.FromSeconds(1);
        private PlayScreen m_PlayScreen;

        public TransitionScreen(Game i_Game, PlayScreen i_PlayScreen) : base(i_Game)
        {
            m_PlayScreen = i_PlayScreen;
        }

        private void resetTimer()
        {
            m_SecondsLeft = 3;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Bauhaus93Font = this.Game.Content.Load<SpriteFont>(@"Fonts\Bauhaus93");
            m_Background = this.Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
        }

        public override void Update(GameTime i_GameTime)
        {
            if (m_PlayScreen.IsGameOver == false)
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
                    m_LevelNumber++;
                    ScreensManager.SetCurrentScreen(m_PlayScreen);
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
            this.SpriteBatch.DrawString(m_Bauhaus93Font, m_LevelNumber.ToString(), new Vector2(105, 105), Color.White);
            this.SpriteBatch.DrawString(m_Bauhaus93Font, m_SecondsLeft.ToString(), new Vector2(130, 175), Color.White);
            this.SpriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
