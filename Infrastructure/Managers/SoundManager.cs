using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Infrastructure.Managers
{
    public class SoundManager : GameService, ISoundManager
    {
        private AudioEngine m_AudioEngine;
        private SoundBank m_SoundBank;
        private WaveBank m_WaveBank;
        private bool m_IsMuted;

        public float SfxVolume { get; private set; }
        public float MusicVolume { get; private set; }

        public SoundManager(Game i_Game) : base(i_Game)
        {
            m_IsMuted = false;
            MusicVolume = 1.0f;
            SfxVolume = 1.0f;
        }

        public void LoadContent()
        {
            m_AudioEngine = new AudioEngine("Content/soundBank.xgs");
            m_SoundBank = new SoundBank(m_AudioEngine, "Content/Sound Bank.xsb");
            m_WaveBank = new WaveBank(m_AudioEngine, "Content/Wave Bank.xwb");
        }

        protected override void RegisterAsService()
        {
            this.Game.Services.AddService(typeof(ISoundManager), this);
        }


        public void PlaySound(string i_SoundName)
        {
            m_SoundBank.GetCue(i_SoundName).Play();
        }

        public void ToggleMute()
        {
            if (!m_IsMuted) 
            {
                m_AudioEngine.GetCategory("Music").SetVolume(0);
                m_AudioEngine.GetCategory("SoundEffects").SetVolume(0);
                m_IsMuted = true;
            }
            else
            {
                m_AudioEngine.GetCategory("Music").SetVolume(MusicVolume);
                m_AudioEngine.GetCategory("SoundEffects").SetVolume(SfxVolume);
                m_IsMuted = false;
            }
        }

        public void SetMusicVolume(float i_Volume)
        {
            MusicVolume = i_Volume;
            MusicVolume = MathHelper.Clamp(MusicVolume, 0, 1);
            AudioCategory category = m_AudioEngine.GetCategory("Music");
            category.SetVolume(MusicVolume);
        }

        public void SetSfxVolume(float i_CurrentVolume)
        {
            SfxVolume = i_CurrentVolume;
            SfxVolume = MathHelper.Clamp(SfxVolume, 0, 1);
            AudioCategory category = m_AudioEngine.GetCategory("SoundEffects");
            category.SetVolume(SfxVolume);
            PlaySound("MenuChange");
        }
    }
}