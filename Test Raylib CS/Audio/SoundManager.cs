using NAudio.Wave;

namespace Memory
{
    class SoundManager
    {
        private readonly SoundPlayer buttonSoundPlayer;
        private readonly SoundPlayer cardClickSoundPlayer;
        private readonly SoundPlayer cardMatchedSoundPlayer;
        private readonly SoundPlayer gameWonSoundPlayer;
        private readonly MusicPlayer menuMusicPlayer;
        private readonly MusicPlayer gameplayMusicPlayer;

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

        private SoundManager()
        {
            var pathToSounds = System.IO.Directory.GetFiles(Program.PathToSounds());
            
            gameplayMusicPlayer = new MusicPlayer(pathToSounds[0]);

            buttonSoundPlayer = new SoundPlayer(pathToSounds[1]);
            cardClickSoundPlayer = new SoundPlayer(pathToSounds[2]);
            cardMatchedSoundPlayer = new SoundPlayer(pathToSounds[3]);
            gameWonSoundPlayer = new SoundPlayer(pathToSounds[4]);

            menuMusicPlayer = new MusicPlayer(pathToSounds[5]);
        }

        public void UpdateSound(float value)
        {
            value /= 100.0f;

            buttonSoundPlayer.UpdateVolume(value);
        }
        
        public void UpdateMusic(float value)
        {
            value /= 100.0f;

            menuMusicPlayer.UpdateVolume(value);
            gameplayMusicPlayer.UpdateVolume(value);
        }

        public void Init()
        {
            buttonSoundPlayer.Init();
            cardClickSoundPlayer.Init();
            cardMatchedSoundPlayer.Init();
            gameWonSoundPlayer.Init();
            menuMusicPlayer.Init();
            gameplayMusicPlayer.Init();
        }

        public void MuteSounds()
        {
            buttonSoundPlayer.Mute();
        }

        public void MuteMusic()
        {
            menuMusicPlayer.Mute();
            gameplayMusicPlayer.Mute();
        }

        public void MuteAll()
        {
            MuteSounds();
            MuteMusic();
        }

        public void UnMuteSounds()
        {
            buttonSoundPlayer.UnMute();
        }

        public void UnMuteMusic()
        {
            menuMusicPlayer.UnMute();
            gameplayMusicPlayer.UnMute();
        }

        public void UnMuteAll()
        {
            UnMuteSounds();
            UnMuteMusic();
        }

        public void ResetMusic()
        {
            menuMusicPlayer.ResetPosition();
            gameplayMusicPlayer.ResetPosition();
        }

        public void ButtonClick()
        {
            PlaySound(buttonSoundPlayer);
        }

        public void OpenCard()
        {
            PlaySound(cardClickSoundPlayer);    
        }

        public void MatchedCard()
        {
            PlaySound(cardMatchedSoundPlayer);
        }

        public void GameWon()
        {
            PlaySound(gameWonSoundPlayer);
        }

        private void PlaySound(SoundPlayer player)
        {
            player.ResetPosition();
            player.Play();
        }

        public void MenuTheme()
        {
            PlayTheme(menuMusicPlayer, gameplayMusicPlayer);
        }

        public void GameplayTheme()
        {
            PlayTheme(gameplayMusicPlayer, menuMusicPlayer);
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
