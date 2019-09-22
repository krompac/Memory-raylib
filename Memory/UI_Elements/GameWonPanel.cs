using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class GameWonPanel : UI_Element
    {
        public delegate void PlaySound();
        private PlaySound sound;
        private bool soundPlayed;

        private Button gameWonButton;
        private Text text = null;

        public GameWindow Window
        {
            get
            {
                return gameWonButton.Window;
            }
        }

        public GameWonPanel(PlaySound sound) : base()
        {
            this.sound = sound;
            soundPlayed = false;
        }

        public void Init(Player winner)
        {
            if (text == null)
            {
                this.rect.width = 350;
                this.rect.x = (GetScreenWidth() - rect.width) / 2;
                this.rect.y = GetScreenHeight() / 6;
                rect.height = GetScreenHeight() / 3;

                var buttonXpos = (int)(rect.x + ((rect.width - 100) / 2));
                var buttonYpos = (int)(rect.y + rect.height - 70);

                gameWonButton = new Button(buttonXpos, buttonYpos, 100, 50, "Okay", GameWindow.Menu);
                gameWonButton.Color = winner.MyColor;
                this.color = winner.MyColor;

                var fontSize = 30;
                var caption = winner.Name.ToUpper() + " WINS";
                var textSize = MeasureText(caption , fontSize);
                var textXpos = (int)rect.x + (((int)rect.width - textSize) / 2);

                text = new Text(textXpos, (int)rect.y + 10, fontSize, caption, Color.BLACK);
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

            gameWonButton.DrawMeWithLines(3, Color.BLACK);
            text.DrawMe();
        }

        public override bool CheckIfClicked()
        {
            return gameWonButton.CheckIfClicked();
        }
    }
}
