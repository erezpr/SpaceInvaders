using Microsoft.Xna.Framework;
using SpaceInvaders.Menus.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Menus.ScreenSettings
{
    public class WindowResizing : ToggleableMenuItem
    {
        private readonly ICommand r_WindowResizingToggle;

        public WindowResizing(string i_Text, ICommand i_WindowResizingToggle) 
            : base(i_Text, new List<string> { "Off", "On" })
        {
            r_WindowResizingToggle = i_WindowResizingToggle;
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            r_WindowResizingToggle.Execute();
        }

        public override void ToggleDown()
        {
            base.ToggleDown();
            r_WindowResizingToggle.Execute();
        }
    }

    public class WindowResizingToggleCommand : ICommand
    {
        private readonly Game r_Game;

        public WindowResizingToggleCommand(Game i_Game)
        {
            r_Game = i_Game;
        }

        public void Execute()
        {
            r_Game.Window.AllowUserResizing = !r_Game.Window.AllowUserResizing;
        }
    }
}
