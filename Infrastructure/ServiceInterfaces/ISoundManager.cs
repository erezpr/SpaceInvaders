

namespace Infrastructure.ServiceInterfaces
{
    public interface ISoundManager
    {
        float SfxVolume { get; }
        float MusicVolume { get; }

        void PlaySound(string i_SoundName);
        void ToggleMute();
        void SetMusicVolume(float i_CurrentVolume);
        void SetSfxVolume(float i_CurrentVolume);
    }
}
