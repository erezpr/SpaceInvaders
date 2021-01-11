using SpaceInvaders.Menus.Abstract;
using SpaceInvaders.Screens;
using System.Collections.Generic;

namespace SpaceInvaders.Menus.MainMenu
{
    public class PlayersInGame : ToggleableMenuItem
    {
        private readonly ICommand r_ToggleUpNumberOfPlayers;
        private readonly ICommand r_ToggleDownNumberOfPlayers;

        public PlayersInGame(string i_Text, ICommand i_ToggleUpNumberOfPlayers, ICommand i_ToggleDownNumberOfPlayers) 
            : base(i_Text, new List<string> { "One", "Two" })
        {
            r_ToggleUpNumberOfPlayers = i_ToggleUpNumberOfPlayers;
            r_ToggleDownNumberOfPlayers = i_ToggleDownNumberOfPlayers;
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            r_ToggleUpNumberOfPlayers.Execute();
        }

        public override void ToggleDown()
        {
            base.ToggleDown();
            r_ToggleDownNumberOfPlayers.Execute();
        }
    }

    public class ToggleUpNumberOfPlayersCommand : ICommand
    {
        private readonly MainScreen r_MainScreen;

        public ToggleUpNumberOfPlayersCommand(MainScreen i_MainScreen)
        {
            r_MainScreen = i_MainScreen;
        }

        public void Execute()
        {
            r_MainScreen.ToggleUpOfPlayers();
        }
    }

    public class ToggleDownNumberOfPlayersCommand : ICommand
    {
        private readonly MainScreen r_MainScreen;

        public ToggleDownNumberOfPlayersCommand(MainScreen i_MainScreen)
        {
            r_MainScreen = i_MainScreen;
        }

        public void Execute()
        {
            r_MainScreen.ToggleDownOfPlayers();
        }
    }
}
