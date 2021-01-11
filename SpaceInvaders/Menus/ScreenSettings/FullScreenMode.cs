using Microsoft.Xna.Framework;
using SpaceInvaders.Menus.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Menus.ScreenSettings
{
    public class FullScreenMode : ToggleableMenuItem
    {
        private readonly ICommand r_ScreenToggle;

        public FullScreenMode(string i_Text, ICommand i_ScreenToggle)
            : base(i_Text, new List<string> { "Off", "On" })
        {
            r_ScreenToggle = i_ScreenToggle;
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            r_ScreenToggle.Execute();
        }

        public override void ToggleDown()
        {
            base.ToggleDown();
            r_ScreenToggle.Execute();
        }
    }

    public class ScreenToggleCommand : ICommand
    {
        private readonly GraphicsDeviceManager r_Graphics;

        public ScreenToggleCommand(GraphicsDeviceManager i_Graphics)
        {
            r_Graphics = i_Graphics;
        }

        public void Execute()
        {
            r_Graphics.ToggleFullScreen();
        }
    }
}
