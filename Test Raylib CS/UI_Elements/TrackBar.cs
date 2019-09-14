using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class TrackBar : UI_Element
    {
        private Tracker tracker;
        private float volume;
        
        public TrackBar(int x, int y, int w, int h, Color barColor, Color trackerColor) : base(x, y, w, h, barColor)
        {
            tracker = new Tracker(x + w, y - 2, w / 40, h + 4, trackerColor);
            volume = 0;
        }

        public override void DrawMe()
        {
            base.DrawMe();
            tracker.DrawMe();

            var vol = (int)(volume + 0.5);
            DrawText(vol.ToString(), (int)rect.x, (int)rect.y - 26, 25, Color.GOLD);
        }

        public override bool CheckIfClicked()
        {
            var clicked = base.CheckIfClicked();

            if (clicked)
            {
                tracker.XPos = (int)GetMousePosition().x;
            }

            return clicked;
        }

        public float DragTracker()
        {
            tracker.DragMe((int)rect.x, (int)(rect.x + rect.width));
            volume = (tracker.XPos - rect.x) / (rect.width / 100.0f);

            return volume;
        }
    }
}
