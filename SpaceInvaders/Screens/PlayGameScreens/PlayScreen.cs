using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Managers;
using SpaceInvaders.Sprites;
using System;

namespace SpaceInvaders.Screens.PlayGameScreens
{
    public class PlayScreen : GameScreen
    {
        public event Action LevelOver;
        public int LevelNumber { get; private set; }
        public bool IsGameOver { get; private set; } = false;
        public ScoreManager ScoreManager { get => r_PlayersManager.ScoreManeger; }
        private readonly PlayersManager r_PlayersManager;
        private readonly PauseScreen r_PauseScreen;
        private readonly ISoundManager r_SoundManager;
        private Texture2D m_Background;
        private EnemyManager m_EnemyManager;
        private BarrierManager m_BarrierManager;

        public PlayScreen(Game i_Game, int i_NumberOfPlayers, MovementKeys[] i_MovementSchemes) : base(i_Game)
        {
            this.Add(new MotherShip(i_Game));
            r_PlayersManager = new PlayersManager(i_Game, i_NumberOfPlayers, i_MovementSchemes);
            this.Add(r_PlayersManager);
            r_PauseScreen = new PauseScreen(i_Game);
            r_SoundManager = Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            r_PlayersManager.GameOver += gameOver;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_BarrierManager = new BarrierManager(Game, LevelNumber % 5);
            m_EnemyManager = new EnemyManager(Game, LevelNumber % 5);
            m_EnemyManager.GameOver += gameOver;
            m_EnemyManager.LevelOver += levelOver;
            this.Add(m_BarrierManager);
            this.Add(m_EnemyManager);
        }

        private void gameOver()
        {
            IsGameOver = true;
            ExitScreen();
        }

        private void levelOver()
        {
            r_SoundManager.PlaySound("LevelWin");
            m_BarrierManager.Clear();
            m_EnemyManager.Clear();
            this.Remove(m_BarrierManager);
            this.Remove(m_EnemyManager);
            m_EnemyManager.GameOver -= gameOver;
            m_EnemyManager.LevelOver -= levelOver;
            clearAllPlayerBullets();
            LevelNumber++;
            this.LevelOver?.Invoke();
            ExitScreen();
        }

        private void clearAllPlayerBullets()
        {
            for (int i = 0; i < r_PlayersManager.Players.Length; i++)
            {
                r_PlayersManager.Players[i].Gun.ClearBullets();
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Background = this.Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.SetCurrentScreen(r_PauseScreen);
            }

            if (InputManager.KeyPressed(Keys.M))
            {
                r_SoundManager.ToggleMute();
            }
        }

        public override void Draw(GameTime i_GameTime)
        {
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(m_Background, Vector2.Zero, Color.White);
            this.SpriteBatch.End();
            base.Draw(i_GameTime);
        }
    }
}
