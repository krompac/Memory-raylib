using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Memory
{
    class Button : UI_Element
    {
        protected Text text;
        public GameWindow Window { get; private set; }

        public Button(int x, int y, int w, int h, string text, GameWindow window) : base(x, y, w, h)
        {
            Window = window;

            int fontSize = (int)rect.Height / 2;
            int textSize = MeasureText(text, fontSize);

            while (w - textSize < 10)
            {
                fontSize--;
                textSize = MeasureText(text, fontSize);
            }

            int textIndention = (w - textSize) / 2;

            this.text = new Text(x + textIndention, y + (h / 3), fontSize, text, Color.Black);
        }

        public override void DrawMe()
        {
            base.DrawMe();
            text.DrawMe();
        }

        public override bool CheckIfClicked()
        {
            bool isClicked = base.CheckIfClicked();

            if (isClicked)
            {
                SoundManager.Instance.ButtonClick();
            }

            return isClicked;
        }
    }
}
