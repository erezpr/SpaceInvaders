using Microsoft.Xna.Framework;
using SpaceInvaders.Menus;
using SpaceInvaders.Menus.Abstract;
using SpaceInvaders.Menus.ScreenSettings;
using SpaceInvaders.Screens.MenusScreens.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Screens.MenusScreens
{
    public class ScreenSettingsScreen : MenuScreen
    {
        public ScreenSettingsScreen(Game i_Game) : base(i_Game) { }

        protected override void setMenuItems()
        {
            MenuItems = new List<MenuItem>
            {
                new FullScreenMode("Full Screen Mode", new ScreenToggleCommand((Game as GameBase).Graphics)),
                new MouseVisibility("Mouse Visibility", new ToggleMouseVisibilityCommand(Game)),
                new WindowResizing("Allow Window Resizing", new WindowResizingToggleCommand(Game))
            };


        }
    }
}
