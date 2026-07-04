using Raylib_cs;

namespace Memory
{
    class SoundPlayer
    {
        protected Sound sound;
        protected bool muteMe;
        protected float volume;

        public SoundPlayer(string pathToSound)
        {
            sound = Raylib.LoadSound(pathToSound);
            muteMe = false;
            volume = 1.0f;
        }

        public void UpdateVolume(float value)
        {
            volume = value;
            Raylib.SetSoundVolume(sound, muteMe ? 0f : volume);
        }

        public virtual void Mute()
        {
            muteMe = true;
            Raylib.SetSoundVolume(sound, 0f);
        }

        public virtual void UnMute()
        {
            muteMe = false;
            Raylib.SetSoundVolume(sound, volume);
        }

        public void Play()
        {
            if (!muteMe)
            {
                Raylib.PlaySound(sound);
            }
        }

        public void ResetPosition()
        {
            // raylib's PlaySound always restarts a short one-shot sound from the
            // beginning, so there's no separate "reset" step needed here.
        }

        public void Unload()
        {
            Raylib.UnloadSound(sound);
        }
    }
}