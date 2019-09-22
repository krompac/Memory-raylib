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
            var xDif = rect.x;

            rect.x = windowWidth - 100;
            rect.y = windowHeight - 20;

            xDif -= rect.x;

            text.UpdateXpos = (int)xDif;
            text.YPos = (int)(rect.y + (rect.height / 3));
        }
    }
}
