using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Player
    {
        private int textXpos;
        private readonly int index;
        public int Score { get; set; }
        public string Name { get; private set; }
        public Color MyColor { get; private set; }

        public int TextXpos
        {
            set
            {
                textXpos = value;
            }
        }

        public Player(string name, Color color, int index)
        {
            this.Name = name;
            this.MyColor = color;
            this.index = index;
            Score = 0;
            textXpos = 500;
        }

        public void DrawMe(int currentPlayerIndex)
        {
            int increasedFont = index == currentPlayerIndex ? 5 : 0;
            int increasedSpace = index > currentPlayerIndex ? 5 : 0;

            DrawText(Name + ": " + Score.ToString(), textXpos, 20 + index * 30 + increasedSpace, 20 + increasedFont, MyColor);
        }
    }
}
