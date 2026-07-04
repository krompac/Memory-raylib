using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Memory
{
    class GameWonPanel(GameWonPanel.PlaySound sound) : UI_Element()
    {
        public delegate void PlaySound();

        private readonly PlaySound sound = sound;

        private bool soundPlayed = false;

        private Button gameWonButton;
        private Text text = null;

        public GameWindow Window
        {
            get
            {
                return gameWonButton.Window;
            }
        }

        public void Init(Player winner)
        {
            if (text == null)
            {
                rect.Width = 350;
                rect.X = (GetScreenWidth() - rect.Width) / 2;
                rect.Y = GetScreenHeight() / 6;
                rect.Height = GetScreenHeight() / 3;

                var buttonXpos = (int)(rect.X + ((rect.Width - 100) / 2));
                var buttonYpos = (int)(rect.Y + rect.Height - 70);

                gameWonButton = new Button(buttonXpos, buttonYpos, 100, 50, "Okay", GameWindow.Menu)
                {
                    Color = winner.MyColor
                };

                color = winner.MyColor;

                var fontSize = 30;
                var caption = winner.Name.ToUpper() + " WINS";
                var textSize = MeasureText(caption, fontSize);
                var textXpos = (int)rect.X + (((int)rect.Width - textSize) / 2);

                text = new Text(textXpos, (int)rect.Y + 10, fontSize, caption, Color.Black);
            }
        }

        public override void DrawMeWithLines(int lineThicknes, Color lineColor)
        {
            base.DrawMeWithLines(lineThicknes, lineColor);

            if (!soundPlayed)
            {
                sound.Invoke();
                soundPlayed = true;
            }

            gameWonButton.DrawMeWithLines(3, Color.Black);
            text.DrawMe();
        }

        public override bool CheckIfClicked()
        {
            return gameWonButton.CheckIfClicked();
        }
    }
}
