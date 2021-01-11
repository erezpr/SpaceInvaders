using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Menus
{
    public class QuitGame : MenuItem, IClickableMenuItem
    {
        private QuitGameCommand m_QuitGameCommand;
        public QuitGame(Game i_Game, string i_Text) : base(i_Game, i_Text)
        {
            m_QuitGameCommand = new QuitGameCommand(i_Game);
        }

        public void Click()
        {
            m_QuitGameCommand.Execute();
        }
    }

    internal class QuitGameCommand : ICommand
    {
        private Game m_Game;
        public QuitGameCommand(Game i_Game)
        {
            m_Game = i_Game;
        }
        public void Execute()
        {
            m_Game.Exit();
        }
    }
}
