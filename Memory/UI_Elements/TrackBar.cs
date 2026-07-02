using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Memory
{
    class TrackBar(int x, int y, int w, int h, Color barColor, Color trackerColor) : UI_Element(x, y, w, h, barColor)
    {
        private readonly Tracker tracker = new Tracker(x + w, y - 2, w / 40, h + 4, trackerColor);
        private float volume = 0;

        public override void DrawMe()
        {
            base.DrawMe();
            tracker.DrawMe();

            var vol = (int)(volume + 0.5);
            DrawText(vol.ToString(), (int)rect.X, (int)rect.Y - 26, 25, Color.Gold);
        }

        public override bool CheckIfClicked()
        {
            var clicked = base.CheckIfClicked();

            if (clicked)
            {
                tracker.XPos = (int)GetMousePosition().X;
            }

            return clicked;
        }

        public float DragTracker()
        {
            tracker.DragMe((int)rect.X, (int)(rect.X + rect.Width));
            volume = (tracker.XPos - rect.X) / (rect.Width / 100.0f);

            return volume;
        }
    }
}
