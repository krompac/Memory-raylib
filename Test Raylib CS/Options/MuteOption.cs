using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class MuteOption : UI_Element
    {
        public delegate void MuteAction();
        private readonly MuteAction mute;
        private readonly MuteAction unMute;

        private readonly Text text;
        private Texture2D checkMarkImage;
        private Vector2 imagePosition;
        private bool isChecked;

        public MuteOption(MuteAction mute, MuteAction unMute, Text text, int x, int y, int w, int h) : base(x, y, w, h)
        {
            this.mute = mute;
            this.unMute = unMute;
            this.text = text;
            imagePosition = new Vector2(rect.x, y);

            Initialize();
        }

        private void Initialize()
        {
            color = Color.BLACK;
            Console.WriteLine(Program.GetCheckMarkPath());
            checkMarkImage = LoadTexture(Program.GetCheckMarkPath());
            
            isChecked = false;
        }

        public override bool CheckIfClicked()
        {
            if (base.CheckIfClicked())
            {
                if (isChecked)
                {
                    unMute.Invoke();
                    isChecked = false;
                }
                else
                {
                    mute.Invoke();
                    isChecked = true;
                }
            }

            return true;
        }

        public override void DrawMe()
        {
            text.DrawMe();
            DrawRectangleLinesEx(rect, 1, Color.WHITE);

            if (isChecked)
            {
                DrawTextureEx(checkMarkImage, imagePosition, 0, 0.4f, Color.WHITE);
            }
        }
    }
}