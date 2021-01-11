using Microsoft.Xna.Framework;
using SpaceInvaders.Menus.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Menus.ScreenSettings
{
    public class MouseVisibility : ToggleableMenuItem
    {
        private readonly ICommand r_ToggleMouseVisibility;

        public MouseVisibility(string i_Text, ICommand i_ToggleMouseVisibility) 
            : base(i_Text, new List<string> { "Visible", "Invisible" })
        {
            r_ToggleMouseVisibility = i_ToggleMouseVisibility;
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            r_ToggleMouseVisibility.Execute();
        }

        public override void ToggleDown()
        {
            base.ToggleDown();
            r_ToggleMouseVisibility.Execute();
        }
    }

    internal class ToggleMouseVisibilityCommand : ICommand
    {
        private readonly Game r_Game;

        public ToggleMouseVisibilityCommand(Game i_Game)
        {
            r_Game = i_Game;
        }

        public void Execute()
        {
            r_Game.IsMouseVisible = !r_Game.IsMouseVisible;
        }
    }
}
