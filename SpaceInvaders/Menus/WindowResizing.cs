using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using System.Collections.Generic;

namespace SpaceInvaders.Menus
{
    public class WindowResizing : ToggleableMenuItem
    {
        private WindowResizingToggle m_WindowResizingToggle;

        public WindowResizing(Game i_Game, string i_Text) : base(i_Game, i_Text, new List<string> { "Off", "On" })
        {
            m_WindowResizingToggle = new WindowResizingToggle(i_Game);
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            m_WindowResizingToggle.Execute();
        }
        
        public override void ToggleDown()
        {
            base.ToggleDown();
            m_WindowResizingToggle.Execute();
        }

    }
    internal class WindowResizingToggle : ICommand
    {
        private readonly Game m_Game;

        public WindowResizingToggle(Game i_Game)
        {
            m_Game = i_Game;
        }

        public void Execute()
        {
            m_Game.Window.AllowUserResizing = !m_Game.Window.AllowUserResizing;
        }
    }
}
