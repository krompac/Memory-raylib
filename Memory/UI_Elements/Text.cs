using Raylib;
using static Raylib.Raylib;


namespace Memory
{
    class Text
    {
        private int xPos;
        private readonly int yPos;
        private readonly int fontSize;
        private readonly string text;

        public int UpdateXpos
        {
            set
            {
                xPos += value;
            }
        }

        public Text(int x, int y, int fontSize, string text)
        {
            xPos = x;
            yPos = y;
            this.fontSize = fontSize;
            this.text = text;
        }

        public void DrawMe()
        {
            DrawText(text, xPos, yPos, fontSize, Color.RAYWHITE);
        }
    }
}
