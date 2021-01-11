using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Screens;

namespace SpaceInvaders
{
    public class SpaceInvaders : GameBase
    {
        private readonly ScreensMananger r_ScreensMananger;
        private readonly SoundManager r_SoundManager;

        public SpaceInvaders()
        {
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 600;
            _ = new RandomManager(this);
            _ = new InputManager(this);
            _ = new CollisionsManager(this);
            _ = new PixelCollisionManager(this);
            r_SoundManager = new SoundManager(this);
            r_ScreensMananger = new ScreensMananger(this);
            r_ScreensMananger.SetCurrentScreen(new MainScreen(this));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            r_SoundManager.LoadContent();
            r_SoundManager.PlaySound("BackgroundMusic");
        }

        protected override void Initialize()
        {
            this.Window.Title = "Space Invaders";
            base.Initialize();
        }

        protected override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
        }

        protected override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(i_GameTime);
        }
    }
}
