using NAudio.Wave;

namespace Memory
{
    class SoundManager
    {
        private readonly SoundPlayer buttonSoundPlayer = new SoundPlayer(Program.ButtonSound);
        private readonly MusicPlayer menuSoundPlayer = new MusicPlayer(Program.ThemeSound);
        private readonly MusicPlayer gameplaySoundPlayer = new MusicPlayer(Program.GameplaySound);

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
            buttonSoundPlayer.Init();
            menuSoundPlayer.Init();
            gameplaySoundPlayer.Init();
        }

        public void ResetMusic()
        {
            menuSoundPlayer.ResetPosition();
            gameplaySoundPlayer.ResetPosition();
        }

        public void ButtonClick()
        {
            buttonSoundPlayer.ResetPosition();
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

        private void PlayTheme(MusicPlayer mainPlayer, MusicPlayer otherPlayer)
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
