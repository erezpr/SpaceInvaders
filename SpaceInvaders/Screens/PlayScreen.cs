using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Managers;
using SpaceInvaders.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Screens
{
    public class PlayScreen : GameScreen
    {
        public Action LevelOver;
        private int m_LevelNumber;
        private bool m_IsGameOver = false;
        private EnemyManager m_EnemyManager;
        private PlayersManager m_PlayersManager;
        private Texture2D m_Background;
        private BarrierManager m_BarrierManager;
        private PauseScreen m_PauseScreen;
        private ISoundManager m_SoundManager;
        public int LevelNumber { get => m_LevelNumber; }
        public bool IsGameOver { get => m_IsGameOver; }

        public ScoreManager ScoreManager { get => m_PlayersManager.ScoreManeger; }

        public PlayScreen(Game i_Game, int i_NumberOfPlayers, MovementKeys[] i_MovementSchemes) : base(i_Game)
        {
            this.Add(new MotherShip(i_Game));
            m_PlayersManager = new PlayersManager(i_Game, i_NumberOfPlayers, i_MovementSchemes);
            this.Add(m_PlayersManager);
            m_PauseScreen = new PauseScreen(i_Game);
            m_PlayersManager.GameOver += gameOver;
            m_SoundManager = Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
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
            m_IsGameOver = true;
            ExitScreen();
        }

        private void levelOver()
        {
            m_SoundManager.PlaySound("LevelWin");
            m_BarrierManager.Clear();
            m_EnemyManager.Clear();
            this.Remove(m_BarrierManager);
            this.Remove(m_EnemyManager);
            m_EnemyManager.GameOver -= gameOver;
            m_EnemyManager.LevelOver -= levelOver;
            for (int i = 0; i < m_PlayersManager.Players.Length; i++)
            {
                m_PlayersManager.Players[i].Gun.ClearBullets();
            }
            m_LevelNumber++;
            this.LevelOver?.Invoke();
            ExitScreen();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Background = this.Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.SetCurrentScreen(m_PauseScreen);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(m_Background,Vector2.Zero, Color.White);
            this.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
