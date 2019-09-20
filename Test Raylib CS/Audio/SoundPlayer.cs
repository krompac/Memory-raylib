using NAudio.Wave;

namespace Memory
{
    class SoundPlayer
    {
        protected readonly WaveOutEvent player;
        protected readonly WaveFileReader fileReader;
        protected bool muteMe;
        
        public SoundPlayer(string pathToSound)
        {
            player = new WaveOutEvent();
            fileReader = new WaveFileReader(pathToSound);
            muteMe = false;
        }

        public void UpdateVolume(float value)
        {
            player.Volume = value;
        }

        public virtual void Mute()
        {
            muteMe = true;
        }

        public virtual void UnMute()
        {
            muteMe = false;
        }

        public virtual void Init()
        {
            player.Init(fileReader);
        }

        public void Play()
        {
            if (!muteMe)
            {
                player.Play();
            }
        }

        public void ResetPosition()
        {
            fileReader.Position = 0;
        }
    }
}