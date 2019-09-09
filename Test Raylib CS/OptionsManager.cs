using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class OptionsManager
    {
        private MuteOption muteAllOption;

        public OptionsManager()
        {
            var xPos = 550;
            var yPos = 300;
            var text = new Text(xPos - 200, yPos + 7, 35, "Mute all?");
            muteAllOption = new MuteOption(SoundManager.Instance.MuteAll, SoundManager.Instance.UnMuteAll, text, xPos, yPos, 50, 50);
        }

        public void DrawMe()
        {
            //DrawOptionsText();
            muteAllOption.DrawMe();
            muteAllOption.CheckIfClicked();
        }

        public void DrawOptionsText()
        {
            int xPos = 100;
            int yPos = 50;
            int fontSize = 25;

            DrawText("Sound volume", xPos, yPos, fontSize, Color.RAYWHITE);
            DrawText("TEST", xPos, yPos - 25, fontSize, Color.RED);
        }
    }
}
