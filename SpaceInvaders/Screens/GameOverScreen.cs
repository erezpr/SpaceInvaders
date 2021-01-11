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
    public class GameOverScreen : GameScreen
    {
        private Texture2D m_Background;
        private IInputManager m_InputManager;
        private SpriteFont m_Bauhaus93Font;

        public GameOverScreen(Game i_Game) : base(i_Game)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Bauhaus93Font = this.Game.Content.Load<SpriteFont>(@"Fonts\Bauhaus93");
            m_Background = this.Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
        }

        public override void Update(GameTime gameTime)
        {
            if (m_InputManager.KeyPressed(Keys.Enter) == true)
            {
                this.ExitScreen();
            }

            if (m_InputManager.KeyPressed(Keys.Escape) == true)
            {
                Game.Exit();
            }
            
            if (m_InputManager.KeyPressed(Keys.M) == true)
            {
                Game.Exit();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            string enterString = "Press Enter to start";
            string escString = "Press Esc to quit";
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(m_Background, Vector2.Zero, Color.White);
            this.SpriteBatch.DrawString(m_Bauhaus93Font, enterString, new Vector2(105, 105), Color.White);
            this.SpriteBatch.DrawString(m_Bauhaus93Font, escString, new Vector2(130, 175), Color.White);
            this.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
