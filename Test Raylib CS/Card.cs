using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Card : UI_Element
    {
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
                DrawTextureEx(image, position, 0, 0.1f, Color.WHITE);
        }

        public override bool CheckIfClicked()
        {
            if (base.CheckIfClicked())
            {
                drawImage = true;
            }


            return true;
        }

        bool drawImage;
        Texture2D image;
        Vector2 position;
    }
}
