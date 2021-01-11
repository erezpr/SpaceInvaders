using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using System;

namespace SpaceInvaders.Managers
{
    public class PlayersManager : CompositeDrawableComponent<IGameComponent>
    {
        public event Action GameOver;
        public SpaceShip[] Players { get; }
        public ScoreManager ScoreManeger { get; }
        private readonly LivesManager r_LivesManager;
        private const int k_PlayerSize = 32;
        private const int k_Spacing = 10;

        public PlayersManager(Game i_Game, int i_NumberOfPlayers, MovementKeys[] i_MovementSchemes) : base(i_Game)
        {
            r_LivesManager = new LivesManager(i_Game, i_NumberOfPlayers);
            ScoreManeger = new ScoreManager(i_Game, i_NumberOfPlayers);
            Players = new SpaceShip[i_NumberOfPlayers];

            for (int i = 0; i < i_NumberOfPlayers; i++)
            {
                string assetName = string.Format(@"Sprites\Ship0{0}_32x32", i + 1);
                Players[i] = new SpaceShip(i_Game, assetName, i, i_MovementSchemes[i]);
                Players[i].ScoreChanged += playerScoreChanged;
                Players[i].LivesChanged += playerLifeChange;
                Players[i].outOfLives += outOfLives;
            }

            this.Add(ScoreManeger);
            this.Add(r_LivesManager);
        }

        public override void Initialize()
        {
            base.Initialize();

            for (int i = 0; i < Players.Length; i++)
            {
                this.Add(Players[i]);
                this.Add(Players[i].Gun);
                Players[i].Position = Players[i].InitialPosition = new Vector2(i * (k_Spacing + k_PlayerSize), Game.GraphicsDevice.Viewport.Height - k_PlayerSize - k_Spacing);
            }
        }

        public bool IsGameOver()
        {
            bool isGameOver = true;

            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i].Enabled == true)
                {
                    isGameOver = false;
                    break;
                }
            }

            return isGameOver;
        }

        private void playerLifeChange(SpaceShip i_Player)
        {
            r_LivesManager.LifeChanged(i_Player);
        }

        private void playerScoreChanged(SpaceShip i_Player)
        {
            ScoreManeger.ScoreChanged(i_Player);
        }

        private void outOfLives()
        {
            if (IsGameOver() == true)
            {
                GameOver?.Invoke();
            }
        }
    }
}
