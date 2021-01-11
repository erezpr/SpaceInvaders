using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Menus.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Screens.MenusScreens.Abstract
{
    public abstract class MenuScreen : GameScreen
    {
        protected List<MenuItem> MenuItems { get; set; }
        private CircleToggle m_CircleToggle;
        private MenuItem m_ActiveMenuItem;
        private SpriteFont m_Bauhaus93Font;
        private Texture2D m_Background;
        private const float k_Spacing = 70;
        private IInputManager m_InputManager;
        private ISoundManager m_SoundManager;
        private Game m_Game;

        public MenuScreen(Game i_Game) : base(i_Game)
        {
            m_Game = i_Game;
        }

        public override void Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            m_SoundManager = Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            setMenuItems();
            m_CircleToggle = new CircleToggle(MenuItems.Count);
            MenuItems[0].State = eMenuItemState.Active;
            m_ActiveMenuItem = MenuItems[0];
            m_Bauhaus93Font = this.Game.Content.Load<SpriteFont>(@"Fonts\Bauhaus93");
            m_Background = this.Game.Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
        }

        protected abstract void setMenuItems();

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (m_InputManager.KeyPressed(Keys.Enter) == true)
            {
                if (m_ActiveMenuItem is IClickableMenuItem menuItem)
                {
                    menuItem.Click();
                }
            }

            if (m_InputManager.KeyPressed(Keys.PageUp) == true ||
                m_InputManager.KeyPressed(Keys.Left) == true ||
                m_InputManager.KeyPressed(Keys.Tab) == true ||
                m_InputManager.KeyPressed(Keys.Space) == true ||
                m_InputManager.KeyPressed(Keys.Enter) == true)
            {
                if (m_ActiveMenuItem is IToggleableMenuItem menuItem)
                {
                    menuItem.ToggleUp();
                }
            }

            if (m_InputManager.KeyPressed(Keys.PageDown) == true ||
                m_InputManager.KeyPressed(Keys.Right) == true)
            {
                if (m_ActiveMenuItem is IToggleableMenuItem menuItem)
                {
                    menuItem.ToggleDown();
                }
            }

            if (m_InputManager.KeyPressed(Keys.Up) == true)
            {
                playSoundAndSetMenuItemToInactive();
                m_CircleToggle.ToggleDown();
                setCurrentMenuItemToActive();
            }

            if (m_InputManager.KeyPressed(Keys.Down) == true)
            {
                playSoundAndSetMenuItemToInactive();
                m_CircleToggle.ToggleUp();
                setCurrentMenuItemToActive();
            }

            if (m_InputManager.KeyPressed(Keys.Escape))
            {
                ExitScreen();
            }

        }

        private void setCurrentMenuItemToActive()
        {
            MenuItems[m_CircleToggle.CurrentIndex].State = eMenuItemState.Active;
            m_ActiveMenuItem = MenuItems[m_CircleToggle.CurrentIndex];
        }

        private void playSoundAndSetMenuItemToInactive()
        {
            m_SoundManager.PlaySound("MenuChange");
            MenuItems[m_CircleToggle.CurrentIndex].State = eMenuItemState.Inactive;
        }

        public void PlayGame()
        {
            this.ExitScreen();

            if (ScreensManager.ActiveScreen is MainScreen mainScreen)
            {
                mainScreen.StartGame();
            }
        }

        public override void Draw(GameTime i_GameTime)
        {
            int counter = 0;
            this.SpriteBatch.Begin();
            this.SpriteBatch.Draw(m_Background, Vector2.Zero, Color.White);


            foreach (MenuItem menuItem in MenuItems)
            {
                float strXLoction = m_Game.Window.ClientBounds.Width / 2 - m_Bauhaus93Font.MeasureString(menuItem.ToString()).Length() / 2;
                this.SpriteBatch.DrawString(m_Bauhaus93Font, menuItem.ToString(), new Vector2(strXLoction, 130 + k_Spacing * counter), menuItem.State == eMenuItemState.Active ? Color.Red : Color.White);
                counter++;
            }
            this.SpriteBatch.DrawString(m_Bauhaus93Font, "Esp - To Go back", new Vector2(15, 15), Color.White, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            this.SpriteBatch.End();
            base.Draw(i_GameTime);
        }
    }
}
