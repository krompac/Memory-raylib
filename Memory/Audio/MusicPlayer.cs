using Raylib_cs;

namespace Memory
{
    public enum PlaybackState
    {
        Stopped,
        Playing,
        Paused
    }

    class MusicPlayer(string pathToMusic)
    {
        private Music music = Raylib.LoadMusicStream(pathToMusic);
        private bool muteMe = false;
        private float volume = 1.0f;
        private PlaybackState state = PlaybackState.Stopped;

        public PlaybackState PlaybackState => state;

        public void UpdateVolume(float value)
        {
            volume = value;
            Raylib.SetMusicVolume(music, muteMe ? 0f : volume);
        }

        public void Mute()
        {
            muteMe = true;
            Raylib.SetMusicVolume(music, 0f);
        }

        public void UnMute()
        {
            muteMe = false;
            Raylib.SetMusicVolume(music, volume);

            if (state == PlaybackState.Paused)
            {
                Play();
            }
        }

        public void Play()
        {
            if (muteMe)
            {
                return;
            }

            if (state == PlaybackState.Stopped)
            {
                Raylib.PlayMusicStream(music);
            }
            else if (state == PlaybackState.Paused)
            {
                Raylib.ResumeMusicStream(music);
            }

            state = PlaybackState.Playing;
        }

        public void Pause()
        {
            Raylib.PauseMusicStream(music);
            state = PlaybackState.Paused;
        }

        public void ResetPosition()
        {
            Raylib.SeekMusicStream(music, 0f);
            state = PlaybackState.Stopped;
        }

        // IMPORTANT: unlike NAudio, raylib streams music off the game loop rather
        // than a background thread — this must be called once per frame for
        // whichever music track is currently playing, or you'll get silence/stutter.
        public void Update()
        {
            if (state == PlaybackState.Playing)
            {
                Raylib.UpdateMusicStream(music);
            }
        }

        public void Unload()
        {
            Raylib.UnloadMusicStream(music);
        }
    }
}