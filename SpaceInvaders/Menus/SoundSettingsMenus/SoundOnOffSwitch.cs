using Infrastructure.ServiceInterfaces;
using SpaceInvaders.Menus.Abstract;
using System.Collections.Generic;

namespace SpaceInvaders.Menus.SoundSettingsMenus
{
    public class SoundOnOffSwitch : ToggleableMenuItem
    {
        private readonly ICommand r_SoundToggle;

        public SoundOnOffSwitch(string i_Text, ICommand i_SoundToggle) 
            : base(i_Text, new List<string> { "Off", "On" })
        {
            r_SoundToggle = i_SoundToggle;
        }

        public override void ToggleUp()
        {
            base.ToggleUp();
            r_SoundToggle.Execute();
        }

        public override void ToggleDown()
        {
            base.ToggleDown();
            r_SoundToggle.Execute();
        }
    }

    public class SoundToggleCommand : ICommand
    {
        private readonly ISoundManager r_SoundManager;
      
        public SoundToggleCommand(ISoundManager i_SoundManager)
        {
            r_SoundManager = i_SoundManager;
        }

        public void Execute()
        {
            r_SoundManager.ToggleMute();
        }
    }
}
