using NAudio.Wave;

namespace Memory
{
    class MusicPlayer : SoundPlayer
    {
        public MusicPlayer(string pathToMusic) : base(pathToMusic)
        {
        }

        public PlaybackState PlaybackState
        {
            get
            {
                return player.PlaybackState;
            }
        }

        public override void Init()
        {
            base.Init();
            player.PlaybackStopped += PlaybackStopped;
        }

        private void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            ResetPosition();
        }

        public void Pause()
        {
            player.Pause();
        }
    }
}
