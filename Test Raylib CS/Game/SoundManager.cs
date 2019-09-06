using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Memory
{
    class SoundManager
    {
        private readonly WaveOutEvent buttonSoundPlayer = new WaveOutEvent();
        private readonly WaveOutEvent themeSoundPlayer = new WaveOutEvent();
        private readonly WaveFileReader buttonFileReader = new WaveFileReader(Program.ButtonSound);
        private readonly WaveFileReader themeFileReader = new WaveFileReader(Program.ThemeSound);


        public static SoundManager instance = null;

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
            themeSoundPlayer.Init(new WaveFileReader(Program.ThemeSound));
        }

        public void ButtonClick()
        {
            buttonSoundPlayer.Play();
            buttonFileReader.Position = 0;
        }

        public void PlayTheme()
        {
            if (themeSoundPlayer.PlaybackState != PlaybackState.Playing)
            {
                themeSoundPlayer.Play();
            }
        }
    }
}
