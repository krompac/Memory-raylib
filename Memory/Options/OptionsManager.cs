namespace Memory
{
    class OptionsManager
    {
        private MuteOption muteAllOption;
        public SoundOption musicSounds;
        public SoundOption buttonSounds;

        public OptionsManager()
        {
            var xPos = 550;
            var yPos = 300;
            var text = new Text(xPos - 200, yPos + 7, 35, "Mute all?");
            muteAllOption = new MuteOption(SoundManager.Instance.MuteAll, SoundManager.Instance.UnMuteAll, text, xPos, yPos, 50, 50);

            buttonSounds = new SoundOption("Click sounds", 1, SoundManager.Instance.MuteSounds, SoundManager.Instance.UnMuteSounds);
            musicSounds = new SoundOption("Music sounds", 2, SoundManager.Instance.MuteMusic, SoundManager.Instance.UnMuteMusic);
        }

        public void HandleMe()
        {
            muteAllOption.DrawMe();
            muteAllOption.CheckIfClicked();

            musicSounds.DrawMe();
            buttonSounds.DrawMe();

            musicSounds.CheckIfClicked();
            buttonSounds.CheckIfClicked();

            SoundManager.Instance.UpdateMusic(musicSounds.UpdateVolume());
            //SoundManager.Instance.UpdateSound(buttonSounds.UpdateVolume());
        }
    }
}