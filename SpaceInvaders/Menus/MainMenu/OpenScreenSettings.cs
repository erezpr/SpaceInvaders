using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Menus.Abstract;

namespace SpaceInvaders.Menus.MainMenu
{
    public class OpenSettingsMenu : MenuItem, IClickableMenuItem
    {
        private readonly ICommand m_OpenSettingsMenuCommand;

        public OpenSettingsMenu(string i_Text, ICommand i_OpenSettingsMenuCommand) : base(i_Text)
        {
            m_OpenSettingsMenuCommand = i_OpenSettingsMenuCommand;
        }

        public void Click()
        {
            m_OpenSettingsMenuCommand.Execute();
        }
    }

    public class OpenSettingsMenuCommand : ICommand
    {
        private readonly ScreensMananger r_ScreenManager;
        private readonly GameScreen r_GameScreen;

        public OpenSettingsMenuCommand(ScreensMananger i_ScreenManager, GameScreen i_GameScreen)
        {
            r_ScreenManager = i_ScreenManager;
            r_GameScreen = i_GameScreen;
        }

        public void Execute()
        {
            r_ScreenManager.SetCurrentScreen(r_GameScreen);
        }
    }
}
