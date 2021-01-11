using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menus;
using SpaceInvaders.Menus.Abstract;
using SpaceInvaders.Menus.SoundSettingsMenus;
using SpaceInvaders.Screens.MenusScreens.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Screens.MenusScreens
{
    public class SoundSettingsScreen : MenuScreen
    {
        private ISoundManager m_SoundManager;

        public SoundSettingsScreen(Game i_Game) : base(i_Game)
        { }

        protected override void LoadContent()
        {
            m_SoundManager = Game.Services.GetService(typeof(ISoundManager)) as ISoundManager;
            base.LoadContent();
        }

        protected override void setMenuItems()
        {
            MenuItems = new List<MenuItem>
            {
                new BackgroundMusicVolume("Background Music Volume", new BackgroundMusicVolumeUpCommand(m_SoundManager), new BackgroundMusicVolumeDownCommand(m_SoundManager)),
                new SoundsEffectsVolume("Sounds Effects Volume", new SoundsEffectsVolumeUpCommand(m_SoundManager), new SoundsEffectsVolumeDownCommand(m_SoundManager)),
                new SoundOnOffSwitch("Mute", new SoundToggleCommand(m_SoundManager))
            };
        }
    }
}
