using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Menus
{
    public abstract class MenuItem
    {
        string m_Text;
        private Game m_Game;

        public eMenuItemState State { get; set; }
        public string Text { get => m_Text; }

        public MenuItem(Game i_Game, string i_Text)
        {
            m_Game = i_Game;
            m_Text = i_Text;
        }

        public override string ToString()
        {
            return m_Text;
        }
    }
    public enum eMenuItemState
    {
        Active,
        Inactive
    }
    public interface IToggleableMenuItem
    {
        void ToggleUp();
        void ToggleDown();
    }

    public interface IClickableMenuItem
    {
        void Click();
    }

    public interface ICommand
    {
        void Execute();
    }
}
