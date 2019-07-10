using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Button : UI_Element
    {
        public Button(int x, int y, int w, int h, string text, GameWindow window) : base(x, y, w, h, text)
        {
            this.Window = window;
        }

        public override void DrawMe()
        {
            base.DrawMe();
            DrawText(text, (int)rect.x + text.Length, (int)rect.y + ((int)rect.height / 3), (int)rect.height / 2, Color.BLACK);
        }

        public GameWindow Window { get; private set; }
    }
}
