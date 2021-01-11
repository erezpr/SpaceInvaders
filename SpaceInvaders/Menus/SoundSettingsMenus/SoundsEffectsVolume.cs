using Infrastructure.ServiceInterfaces;
using SpaceInvaders.Menus.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Menus.SoundSettingsMenus
{
    public class SoundsEffectsVolume : ToggleableMenuItem
    {
        readonly ICommand r_SoundsEffectsVolumeUpCommand;
        readonly ICommand r_SoundsEffectsVolumeDownCommand;

        public SoundsEffectsVolume(string i_Text, ICommand i_SoundsEffectsVolumeUpCommand, ICommand i_SoundsEffectsVolumeDownCommand)
            : base(i_Text, new List<string> { "100", "0", "10", "20", "30", "40", "50", "60", "70", "80", "90" })
        {
            r_SoundsEffectsVolumeUpCommand = i_SoundsEffectsVolumeUpCommand;
            r_SoundsEffectsVolumeDownCommand = i_SoundsEffectsVolumeDownCommand;
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            r_SoundsEffectsVolumeUpCommand.Execute();
        }

        public override void ToggleDown()
        {
            base.ToggleDown();
            r_SoundsEffectsVolumeDownCommand.Execute();
        }
    }

    public class SoundsEffectsVolumeUpCommand : ICommand
    {
        private readonly ISoundManager r_SoundManager;

        public SoundsEffectsVolumeUpCommand(ISoundManager i_SoundManager)
        {
            r_SoundManager = i_SoundManager;
        }

        public void Execute()
        {
            float volume = r_SoundManager.SfxVolume;
            if (volume >= 1)
            {
                r_SoundManager.SetSfxVolume(0);
            }
            else
            {
                r_SoundManager.SetSfxVolume(volume + 0.1f);
            }
        }
    }

    public class SoundsEffectsVolumeDownCommand : ICommand
    {
        private readonly ISoundManager r_SoundManager;

        public SoundsEffectsVolumeDownCommand(ISoundManager i_SoundManager)
        {
            r_SoundManager = i_SoundManager;
        }

        public void Execute()
        {
            float volume = r_SoundManager.SfxVolume;
            if (volume <= 0)
            {
                r_SoundManager.SetSfxVolume(1);
            }
            else
            {
                r_SoundManager.SetSfxVolume(volume - 0.1f);
            }
        }
    }
}
