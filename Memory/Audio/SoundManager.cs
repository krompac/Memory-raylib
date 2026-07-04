namespace Memory
{
    enum AudioFiles
    {
        gameplay_theme,
        buttonsound,
        card_click,
        card_succes,
        game_won,
        menu_theme
    }

    class SoundManager
    {
        private readonly SoundPlayer buttonSoundPlayer;
        private readonly SoundPlayer gameWonSoundPlayer;
        private readonly SoundPlayer cardClickSoundPlayer;
        private readonly SoundPlayer cardMatchedSoundPlayer;

        private readonly MusicPlayer menuMusicPlayer;
        private readonly MusicPlayer gameplayMusicPlayer;

        private static SoundManager instance = null;

        public static SoundManager Instance
        {
            get
            {
                instance ??= new SoundManager();

                return instance;
            }
        }

        private SoundManager()
        {
            var pathToSounds = Directory.GetFiles(Program.PathToSounds()).Aggregate(
            new Dictionary<string, string>(),
            (acc, pathToSound) =>
            {
                var key = pathToSound.Split('/')[^1].Split('.')[0];
                var next = new Dictionary<string, string>(acc)
                {
                    [key] = pathToSound
                };

                return next;
            });

            gameplayMusicPlayer = new MusicPlayer(pathToSounds[nameof(AudioFiles.gameplay_theme)]);

            buttonSoundPlayer = new SoundPlayer(pathToSounds[nameof(AudioFiles.buttonsound)]);
            cardClickSoundPlayer = new SoundPlayer(pathToSounds[nameof(AudioFiles.card_click)]);
            cardMatchedSoundPlayer = new SoundPlayer(pathToSounds[nameof(AudioFiles.card_succes)]);
            gameWonSoundPlayer = new SoundPlayer(pathToSounds[nameof(AudioFiles.game_won)]);

            menuMusicPlayer = new MusicPlayer(pathToSounds[nameof(AudioFiles.menu_theme)]);
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

        // NEW: raylib streams music from the main thread instead of a background
        // audio thread, so this needs to be called once per frame from GameLoop().
        public void Update()
        {
            menuMusicPlayer.Update();
            gameplayMusicPlayer.Update();
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