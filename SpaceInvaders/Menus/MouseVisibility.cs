using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Menus
{
    public class MouseVisibility : ToggleableMenuItem
    {
        private ToggleMouseVisibility m_ToggleMouseVisibility;

        public MouseVisibility(Game i_Game, string i_Text) : base(i_Game, i_Text, new List<string> { "On", "Off" })
        {
            m_ToggleMouseVisibility = new ToggleMouseVisibility(i_Game);
        }


        public override void ToggleUp()
        {
            base.ToggleUp();
            m_ToggleMouseVisibility.Execute();
        }

        public override void ToggleDown()
        {
            base.ToggleDown();
            m_ToggleMouseVisibility.Execute();
        }
    }

    internal class ToggleMouseVisibility :ICommand
    {
       private readonly Game m_Game;

        public ToggleMouseVisibility(Game i_Game)
        {
            m_Game = i_Game;
        }

        public void Execute()
        {
            m_Game.IsMouseVisible = !m_Game.IsMouseVisible;
        }
    }
}
