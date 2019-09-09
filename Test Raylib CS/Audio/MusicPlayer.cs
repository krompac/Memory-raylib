using NAudio.Wave;

namespace Memory
{
    class MusicPlayer : SoundPlayer
    {
        private bool wasPlaying;

        public MusicPlayer(string pathToMusic) : base(pathToMusic)
        {
            wasPlaying = false;
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

        public override void Mute()
        {
            base.Mute();

            if (player.PlaybackState == PlaybackState.Playing)
            {
                player.Pause();
                wasPlaying = true;
            }
        }

        public override void UnMute()
        {
            base.UnMute();

            if (wasPlaying)
            {
                Play();
                wasPlaying = false;
            }
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
