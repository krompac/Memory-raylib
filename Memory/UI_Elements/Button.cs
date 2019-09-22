using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Button : UI_Element
    {
        protected Text text;
        public GameWindow Window { get; private set; }

        public Button(int x, int y, int w, int h, string text, GameWindow window) : base(x, y, w, h)
        {
            this.Window = window;

            var fontSize = (int)rect.height / 2;
            var textSize = MeasureText(text, fontSize);

            while (w - textSize < 10)
            {
                fontSize--;
                textSize = MeasureText(text, fontSize);
            }

            var textIndention = (int)(w - textSize) / 2;

            this.text = new Text(x + textIndention, y + (h / 3), fontSize, text, Color.BLACK);
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
