using NAudio.Wave;

namespace Memory
{
    class SoundPlayer
    {
        protected readonly WaveOutEvent player;
        protected readonly WaveFileReader fileReader;
        
        public SoundPlayer(string pathToSound)
        {
            player = new WaveOutEvent();
            fileReader = new WaveFileReader(pathToSound);
        }

        public virtual void Init()
        {
            player.Init(fileReader);
        }

        public void Play()
        {
            player.Play();
        }

        public void ResetPosition()
        {
            fileReader.Position = 0;
        }
    }
}
