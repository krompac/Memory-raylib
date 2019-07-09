using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Card : UI_Element
    {
        public static int NumberOfOpenCards = 0;

        private bool drawImage;
        private Texture2D image;
        private Vector2 position;

        public Card(int x, int y, int w, int h, string text, string fileName = "") : base(x, y, w, h, text)
        {
            if (fileName != "")
            {
                image = LoadTexture(fileName);
            }

            position = new Vector2(rect.x, y);
            drawImage = false;
        }

        public override void DrawMe()
        {
            base.DrawMe();

            if (drawImage)
            {
                DrawTextureEx(image, position, 0, 0.1f, Color.WHITE);
            }
        }

        public override bool CheckIfClicked()
        {
            bool isClicked = base.CheckIfClicked();
            if (isClicked)//&& !drawImage)
            {
                drawImage = true;
                NumberOfOpenCards++;
            }

            return isClicked;
        }

        public void ResetMe()
        {
            drawImage = false;
            NumberOfOpenCards = 1;
        }
    }
}
