using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Menus
{
    public class OpenSettingsMenu : MenuItem, IClickableMenuItem
    {
        private OpenSettingsMenuCommand m_OpenSettingsMenuCommand;
        public OpenSettingsMenu(Game i_Game, string i_Text, ScreensMananger i_ScreenManager, GameScreen i_GameScreen) : base(i_Game, i_Text)
        {
            m_OpenSettingsMenuCommand = new OpenSettingsMenuCommand(i_ScreenManager, i_GameScreen);
        }

        public void Click()
        {
            m_OpenSettingsMenuCommand.Execute();
        }
    }

    internal class OpenSettingsMenuCommand : ICommand
    {
        private ScreensMananger m_ScreenManager;
        private GameScreen m_GameScreen;

        public OpenSettingsMenuCommand(ScreensMananger i_ScreenManager, GameScreen i_GameScreen)
        {
            m_ScreenManager = i_ScreenManager;
            m_GameScreen = i_GameScreen;
        }

        public void Execute()
        {
            m_ScreenManager.SetCurrentScreen(m_GameScreen);
        }
    }
}
