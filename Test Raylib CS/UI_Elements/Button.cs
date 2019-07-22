using Raylib;
using System.Linq;
using static Raylib.Raylib;

namespace Memory
{
    class Button : UI_Element
    {
        private string text;
        private Sound buttonSound;

        public Button(int x, int y, int w, int h, string text, GameWindow window) : base(x, y, w, h)
        {
            this.Window = window;
            this.text = text;
            var pathToSound = System.IO.Directory.GetFiles(Program.PathToSounds()).FirstOrDefault();
            buttonSound = LoadSound(pathToSound);
        }

        public override void DrawMe()
        {
            base.DrawMe();
            DrawText(text, (int)rect.x + text.Length, (int)rect.y + ((int)rect.height / 3), (int)rect.height / 2, Color.BLACK);
        }

        public override bool CheckIfClicked()
        {
            bool isClicked = base.CheckIfClicked();

            if (isClicked)
            {
                PlaySound(buttonSound);
            }

            return isClicked;
        }

        public GameWindow Window { get; private set; }
    }
}
