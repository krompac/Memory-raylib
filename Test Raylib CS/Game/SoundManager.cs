using NAudio.Wave;

namespace Memory
{
    class SoundManager
    {
        private readonly WaveOutEvent buttonSoundPlayer = new WaveOutEvent();
        private readonly WaveOutEvent menuSoundPlayer = new WaveOutEvent();
        private readonly WaveOutEvent gameplaySoundPlayer = new WaveOutEvent();
        private readonly WaveFileReader buttonFileReader = new WaveFileReader(Program.ButtonSound);
        private readonly WaveFileReader menuFileReader = new WaveFileReader(Program.ThemeSound);
        private readonly WaveFileReader gameplayFileReader = new WaveFileReader(Program.GameplaySound);

        private static SoundManager instance = null;
        
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                    instance.Init();
                }

                return instance;
            }
        }

        public void Init()
        {
            buttonSoundPlayer.Init(buttonFileReader);
            menuSoundPlayer.Init(menuFileReader);
            gameplaySoundPlayer.Init(gameplayFileReader);

            menuSoundPlayer.PlaybackStopped += SoundPlayerPlaybackStopped; 
            gameplaySoundPlayer.PlaybackStopped += SoundPlayerPlaybackStopped;
        }

        private void SoundPlayerPlaybackStopped(object sender, StoppedEventArgs e)
        {
            ResetReadersPosition();
        }

        public void ResetReadersPosition()
        {
            menuFileReader.Position = 0;
            gameplayFileReader.Position = 0;
        }

        public void ButtonClick()
        {
            buttonFileReader.Position = 0;
            buttonSoundPlayer.Play();
        }

        public void MenuTheme()
        {
            PlayTheme(menuSoundPlayer, gameplaySoundPlayer);
        }

        public void GameplayTheme()
        {
            PlayTheme(gameplaySoundPlayer, menuSoundPlayer);
        }

        private void PlayTheme(WaveOutEvent mainPlayer, WaveOutEvent otherPlayer)
        {
            if (otherPlayer.PlaybackState == PlaybackState.Playing)
            {
                otherPlayer.Pause();
            }

            if (mainPlayer.PlaybackState != PlaybackState.Playing)
            {
                mainPlayer.Play();
            }
        }
    }
}
