namespace Memory
{
    class ToMenuButton(int x, int y, int w, int h, string text, GameWindow window) : Button(x, y, w, h, text, window)
    {
        public void UpdatePosition(int windowWidth, int windowHeight)
        {
            var xDif = rect.X;

            rect.X = windowWidth - 100;
            rect.Y = windowHeight - 20;

            xDif -= rect.X;

            text.UpdateXpos = (int)xDif;
            text.YPos = (int)(rect.Y + (rect.Height / 3));
        }
    }
}
