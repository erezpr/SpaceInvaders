using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menus.Abstract;
using SpaceInvaders.Menus.MainMenu;
using SpaceInvaders.Screens.MenusScreens.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Screens.MenusScreens
{
    public class MainMenuScreen : MenuScreen
    {
        private readonly MainScreen r_MainScreen;

        public MainMenuScreen(Game i_Game, MainScreen i_MainScreen) : base(i_Game)
        {
            r_MainScreen = i_MainScreen;
        }

        protected override void setMenuItems()
        {
            MenuItems = new List<MenuItem>
            {
                new PlayersInGame("Number Of Players", new ToggleUpNumberOfPlayersCommand(r_MainScreen), new ToggleDownNumberOfPlayersCommand(r_MainScreen)),
                new OpenSettingsMenu("Sound Settings", new OpenSettingsMenuCommand(ScreensManager as ScreensMananger, new SoundSettingsScreen(Game))),
                new OpenSettingsMenu("Screen Settings", new OpenSettingsMenuCommand(ScreensManager as ScreensMananger, new ScreenSettingsScreen(Game)))
            };
        }
    }
}
