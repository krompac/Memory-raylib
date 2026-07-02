using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Memory
{
    class Tracker : UI_Element
    {
        private bool isClicked = false;

        public int XPos
        {
            set
            {
                rect.X = value;
            }
            get
            {
                return (int)rect.X;
            }
        }

        public Tracker(int x, int y, int w, int h, Color color) : base(x, y, w, h, color)
        {
        }

        public override bool CheckIfClicked()
        {
            if (!isClicked || !IsMouseButtonDown(0))
            {
                isClicked = base.CheckIfClicked();
            }

            return isClicked;
        }

        public void DragMe(int minX, int maxX)
        {
            CheckIfClicked();

            var mouseXpos = (int)GetMousePosition().X;
            if (isClicked && IsMouseButtonDown(0) && mouseXpos >= minX && mouseXpos <= maxX)
            {
                XPos = mouseXpos;
            }
        }
    }
}
