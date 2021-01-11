using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace Infrastructure.Managers
{
    public class SoundManager1
    {
        public List<SoundEffect> soundEffects = new List<SoundEffect>();

        public void Mute()
        {
            SoundEffect.MasterVolume = 0.0f;
        }

        public void UnMute()
        {
            SoundEffect.MasterVolume = 1.0f;
        }

        public void SetVolume(int i_Volume)
        {
            SoundEffect.MasterVolume = MathHelper.Clamp(i_Volume, 0, 1);
        }
    }
}
