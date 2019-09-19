using Raylib;
using static Raylib.Raylib;
using static Memory.MuteOption;

namespace Memory
{
    class SoundOption : IDrawable
    {
        private Text label;
        private TrackBar trackBar;
        private MuteOption muteOption;

        public SoundOption(string labelText, int index, MuteAction mute, MuteAction unMute)
        {
            int xPos = 200;
            int yPos = 100 * index;
            int width = 300;
            int height = 7;
            int fontSize = 25;

            trackBar = new TrackBar(xPos, yPos, width, height, Color.ORANGE, Color.MAGENTA);
            label = new Text(xPos - 10 - MeasureText(labelText, fontSize), yPos - fontSize / 2, fontSize, labelText);

            var muteText = new Text(xPos + width + 25, yPos - 45, fontSize, "Mute?");
            muteOption = new MuteOption(mute, unMute, muteText, xPos + width + 50, yPos - 20, 50, 50);
        }

        public void DrawMe()
        {
            label.DrawMe();
            trackBar.DrawMe();
            muteOption.DrawMe();
        }

        public void CheckIfClicked()
        {
            trackBar.CheckIfClicked();
            muteOption.CheckIfClicked();
        }

        public float UpdateVolume()
        {
            return trackBar.DragTracker();
        }
    }
}
