using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Menus
{
    public class ExitMenu : MenuItem, IClickableMenuItem
    {
        private ExitMenuCommand m_ExitMenuCommand;

        public ExitMenu(Game i_Game, string i_Text, Action i_ExitMenu) : base(i_Game, i_Text)
        {
            m_ExitMenuCommand = new ExitMenuCommand(i_ExitMenu);
        }

        public void Click()
        {
            m_ExitMenuCommand.Execute();
        }
    }

    internal class ExitMenuCommand : ICommand
    {
        private readonly Action m_ExitScreen;

        public ExitMenuCommand(Action i_ExitMenu)
        {
            m_ExitScreen = i_ExitMenu;
        }

        public void Execute()
        {
            m_ExitScreen.Invoke();
        }
    }
}
