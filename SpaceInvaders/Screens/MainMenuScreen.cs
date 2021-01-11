using Infrastructure.Managers;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Screens
{
    public class MainMenuScreen : GameScreen
    {
        private List<MenuItem> m_MenuItems;
        private SpriteFont m_Bauhaus93Font;
        private const float k_Spacing = 70; 

        public MainMenuScreen(Game i_Game, MainScreen i_MainScreen) : base(i_Game)
        {
            m_MenuItems = new List<MenuItem>
            {
                new PlayersInGame(i_Game, "Players", i_MainScreen),
                new OpenSettingsMenu(i_Game, "Sound Settings", ScreensManager as ScreensMananger, null),
                new OpenSettingsMenu(i_Game, "Screen Settings", ScreensManager as ScreensMananger, null),
                new PlayGame(i_Game, "Play", this),
                new QuitGame(i_Game, "Quit")
            };
            m_Bauhaus93Font = i_MainScreen.Bauhaus93Font;
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void PlayGame()
        {
            this.ExitScreen();
            if(ScreensManager.ActiveScreen is MainScreen mainScreen)
            {
                mainScreen.StartGame();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            int counter = 0;
            this.SpriteBatch.Begin();
            foreach(MenuItem menuItem in m_MenuItems)
            {
                this.SpriteBatch.DrawString(m_Bauhaus93Font, menuItem.ToString(), new Vector2(105, 145 + k_Spacing * counter), Color.White);
                counter++;
            }
            this.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
