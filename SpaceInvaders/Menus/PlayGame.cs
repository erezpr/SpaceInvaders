using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceInvaders.Screens;

namespace SpaceInvaders.Menus
{
    public class PlayGame : MenuItem, IClickableMenuItem
    {
        private PlayGameCommand m_PlayGameCommand;
        public PlayGame(Game i_Game, string i_Text, MainMenuScreen i_MainMenuScreen) : base(i_Game, i_Text)
        {
            m_PlayGameCommand = new PlayGameCommand(i_MainMenuScreen);
        }

        public void Click()
        {
            m_PlayGameCommand.Execute();
        }
    }

    internal class PlayGameCommand : ICommand
    {
        private MainMenuScreen m_MainMenuScreen;
        public PlayGameCommand(MainMenuScreen i_MainMenuScreen)
        {
            m_MainMenuScreen = i_MainMenuScreen;
        }
        public void Execute()
        {
            m_MainMenuScreen.PlayGame();
        }
    }
}
