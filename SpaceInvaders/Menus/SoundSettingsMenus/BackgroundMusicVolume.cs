using Infrastructure.ServiceInterfaces;
using SpaceInvaders.Menus.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Menus.SoundSettingsMenus
{
    public class BackgroundMusicVolume : ToggleableMenuItem
    {
        readonly ICommand r_BackgroundMusicVolumeUpCommand;
        readonly ICommand r_BackgroundMusicVolumeDownCommand;

        public BackgroundMusicVolume(string i_Text, ICommand i_BackgroundMusicVolumeUpCommand, ICommand i_BackgroundMusicVolumeDownCommand)
            : base(i_Text, new List<string> { "100", "0", "10", "20", "30", "40", "50", "60", "70", "80", "90" })
        {
            r_BackgroundMusicVolumeUpCommand = i_BackgroundMusicVolumeUpCommand;
            r_BackgroundMusicVolumeDownCommand = i_BackgroundMusicVolumeDownCommand;
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            r_BackgroundMusicVolumeUpCommand.Execute();
        }

        public override void ToggleDown()
        {
            base.ToggleDown();
            r_BackgroundMusicVolumeDownCommand.Execute();
        }
    }

    public class BackgroundMusicVolumeUpCommand : ICommand
    {
        private readonly ISoundManager r_SoundManager;

        public BackgroundMusicVolumeUpCommand(ISoundManager i_SoundManager)
        {
            r_SoundManager = i_SoundManager;
        }

        public void Execute()
        {
            float volume = r_SoundManager.MusicVolume;
 
            if (volume >= 1)
            {
                r_SoundManager.SetMusicVolume(0);
            }
            else
            {
                r_SoundManager.SetMusicVolume(volume + 0.1f);
            }
        }
    }

    public class BackgroundMusicVolumeDownCommand : ICommand
    {
        private readonly ISoundManager r_SoundManager;
    
        public BackgroundMusicVolumeDownCommand(ISoundManager i_SoundManager)
        {
            r_SoundManager = i_SoundManager;
        }

        public void Execute()
        {
            float volume = r_SoundManager.MusicVolume;
            if (volume <= 0)
            {
                r_SoundManager.SetMusicVolume(1);
            }
            else
            {
                r_SoundManager.SetMusicVolume(volume - 0.1f);
            }
        }
    }
}
