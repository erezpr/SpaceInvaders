using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceInvaders.Screens;

namespace SpaceInvaders.Menus
{
    public class PlayersInGame : ToggleableMenuItem
    {
        private ToggleUpNumberOfPlayers m_ToggleUpNumberOfPlayers;
        private ToggleDownNumberOfPlayers m_ToggleDownNumberOfPlayers;
        public PlayersInGame(Game i_Game, string i_Text, MainScreen i_MainScreen) : base(i_Game, i_Text, new List<string> { "One", "Two" })
        {
            m_ToggleUpNumberOfPlayers = new ToggleUpNumberOfPlayers(i_MainScreen);
            m_ToggleDownNumberOfPlayers = new ToggleDownNumberOfPlayers(i_MainScreen);
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            m_ToggleUpNumberOfPlayers.Execute();
        }

        public override void ToggleDown()
        {
            base.ToggleDown();
            m_ToggleDownNumberOfPlayers.Execute();
        }
    }

    internal class ToggleUpNumberOfPlayers : ICommand
    {
        private readonly MainScreen m_MainScreen;

        public ToggleUpNumberOfPlayers(MainScreen i_MainScreen)
        {
            m_MainScreen = i_MainScreen;
        }

        public void Execute()
        {
            m_MainScreen.SetNumOfPlayers((m_MainScreen.NumOfPlayers + 1) % m_MainScreen.MaxNumOfPlayers);
        }
    }
    
    internal class ToggleDownNumberOfPlayers : ICommand
    {
        private readonly MainScreen m_MainScreen;

        public ToggleDownNumberOfPlayers(MainScreen i_MainScreen)
        {
            m_MainScreen = i_MainScreen;
        }

        public void Execute()
        {
            m_MainScreen.SetNumOfPlayers((m_MainScreen.NumOfPlayers - 1) % m_MainScreen.MaxNumOfPlayers);
        }
    }
}
