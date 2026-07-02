using System.Numerics;

using Raylib_cs;
using static Raylib_cs.Raylib;

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
            imagePosition = new Vector2(rect.X, y);

            Initialize();
        }

        private void Initialize()
        {
            color = Color.Black;
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
            DrawRectangleLinesEx(rect, 1, Color.White);

            if (isChecked)
            {
                DrawTextureEx(checkMarkImage, imagePosition, 0, 0.4f, Color.White);
            }
        }
    }
}