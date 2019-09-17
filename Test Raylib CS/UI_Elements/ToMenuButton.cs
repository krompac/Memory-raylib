namespace Memory
{
    class ToMenuButton : Button
    {
        public ToMenuButton(int x, int y, int w, int h, string text, GameWindow window)
            : base(x, y, w, h, text, window)
        {
        }

        public void UpdatePosition(int windowWidth, int windowHeight)
        {
            rect.x = windowWidth - 100;
            rect.y = windowHeight - 20;
        }
    }
}
