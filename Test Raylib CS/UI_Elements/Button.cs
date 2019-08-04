using Raylib;
using System.Media;
using static Raylib.Raylib;

namespace Memory
{
    class Button : UI_Element
    {
        private string text;
        private static readonly SoundPlayer player = new SoundPlayer(Program.ButtonSound);

        public Button(int x, int y, int w, int h, string text, GameWindow window) : base(x, y, w, h)
        {
            this.Window = window;
            this.text = text;
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
                player.Play();
            }

            return isClicked;
        }

        public GameWindow Window { get; private set; }
    }
}
