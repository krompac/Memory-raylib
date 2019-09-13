using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class TrackBar : UI_Element
    {
        private Tracker tracker;

        public TrackBar(int x, int y, int w, int h, Color barColor, Color trackerColor) : base(x, y, w, h, barColor)
        {
            tracker = new Tracker(x, y - 2, w / 40, h + 4, trackerColor);
        }

        public override void DrawMe()
        {
            base.DrawMe();
            tracker.DrawMe();
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

        public void DragTracker()
        {
            tracker.DragMe((int)rect.x, (int)(rect.x + rect.width));
        }
    }
}
