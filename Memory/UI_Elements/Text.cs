using Raylib;
using static Raylib.Raylib;


namespace Memory
{
    class Text : IDrawable
    {
        private int xPos;
        private int yPos;
        private readonly Color color;
        private readonly int fontSize;
        private readonly string text;

        public int UpdateXpos
        {
            set
            {
                xPos += value;
            }
        }

        public int YPos
        {
            set
            {
                yPos = value;
            }
        }

        public Text(int x, int y, int fontSize, string text, Color color)
        {
            xPos = x;
            yPos = y;
            this.fontSize = fontSize;
            this.text = text;
            this.color = color;
        }
        
        public void DrawMe()
        {
            DrawText(text, xPos, yPos, fontSize, color);
        }
    }
}
