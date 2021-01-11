using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Menus.Abstract;
using SpaceInvaders.Screens.MenusScreens;
using SpaceInvaders.Screens.PlayGameScreens;

namespace SpaceInvaders.Screens
{
    public class MainScreen : GameScreen
    {
        public int NumOfPlayers { get; private set; } = 1;
        public int MaxNumOfPlayers { get => k_MaxNumOfPlayers; }
        public SpriteFont m_Bauhaus93Font { get; private set; }
        private Texture2D m_Background;
        private IInputManager m_InputManager;
        private PlayScreen m_PlayScreen;
        private TransitionScreen m_TransitionScreen;
        private MainMenuScreen m_MainMenuScreen;
        private readonly Game m_Game;
        private readonly CircleToggle r_CircleToggle;
        private const int k_MaxNumOfPlayers = 2;

        public MainScreen(Game i_Game) : base(i_Game)
        {
            m_Game = i_Game;
            r_CircleToggle = new CircleToggle(k_MaxNumOfPlayers);
        }

        public void ToggleUpOfPlayers()
        {
            r_CircleToggle.ToggleUp();
            NumOfPlayers = r_CircleToggle.CurrentIndex + 1;
        }

        public void ToggleDownOfPlayers()
        {
            r_CircleToggle.ToggleDown();
            NumOfPlayers = r_CircleToggle.CurrentIndex + 1;
        }

        public override void Initialize()
        {
            base.Initialize();
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_Bauhaus93Font = this.Game.Content.Load<SpriteFont>(@"Fonts\Bauhaus93");
            m_Background = this.Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
        }

        public void StartGame()
        {
            m_PlayScreen = new PlayScreen(Game, NumOfPlayers, createMovementSchemes());
            m_TransitionScreen = new TransitionScreen(Game, m_PlayScreen);
            (ScreensManager as ScreensMananger).SetCurrentScreen(m_TransitionScreen);
        }

        public override void Update(GameTime i_GameTime)
        {
            if (m_InputManager.KeyPressed(Keys.Enter) == true)
            {
                StartGame();
            }

            if (m_InputManager.KeyPressed(Keys.Escape) == true)
            {
                Game.Exit();
            }

            if (m_InputManager.KeyPressed(Keys.S) == true)
            {
                if (m_MainMenuScreen == null)
                {
                    m_MainMenuScreen = new MainMenuScreen(Game, this);
                }

                ScreensManager.SetCurrentScreen(m_MainMenuScreen);
            }

            base.Update(i_GameTime);
        }

        private MovementKeys[] createMovementSchemes()
        {
            return new MovementKeys[]
            {
                new MovementKeys(i_LeftKey: Keys.Left, i_RightKey: Keys.Right, i_ShootingKey: Keys.Up),
                new MovementKeys(i_LeftKey: Keys.A, i_RightKey: Keys.D, i_ShootingKey: Keys.W)
            };
        }

        public override void Draw(GameTime i_GameTime)
        {
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(m_Background, Vector2.Zero, Color.White);
            
            if (m_PlayScreen != null && m_PlayScreen.IsGameOver == true)
            {
                for (int i = 0; i < m_PlayScreen.ScoreManager.PlayersScores.Length; i++)
                {
                    this.SpriteBatch.DrawString(m_Bauhaus93Font, string.Format("Player {0}: {1}", i + 1, m_PlayScreen.ScoreManager.PlayersScores[i]), new Vector2(105, 50 + i * 70), Color.White);
                }
            }
            float str1XLoction = m_Game.Window.ClientBounds.Width / 2 - m_Bauhaus93Font.MeasureString("Enter - start game").Length()/2;
            float str2XLoction = m_Game.Window.ClientBounds.Width / 2 - m_Bauhaus93Font.MeasureString("S - settings").Length() / 2;
            float str3XLoction = m_Game.Window.ClientBounds.Width / 2 - m_Bauhaus93Font.MeasureString("Esc - quit").Length() / 2;
            this.SpriteBatch.DrawString(m_Bauhaus93Font, "Enter - start game", new Vector2(str1XLoction, 205), Color.White);
            this.SpriteBatch.DrawString(m_Bauhaus93Font, "S - settings", new Vector2(str2XLoction, 275), Color.White);
            this.SpriteBatch.DrawString(m_Bauhaus93Font, "Esc - quit", new Vector2(str3XLoction, 345), Color.White);
            this.SpriteBatch.End();
            base.Draw(i_GameTime);
        }
    }
}
