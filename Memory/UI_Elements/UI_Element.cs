using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class UI_Element : IDrawable
    {
        protected Rectangle rect;
        protected Color color;

        public UI_Element(int x, int y, int w, int h)
        {
            rect = new Rectangle(x - (w / 2), y, w, h);
            color = Color.BLUE;
        }

        public UI_Element(int x, int y, int w, int h, Color color) : this(x, y, w, h)
        {
            rect.x = x;
            this.color = color;
        }

        public virtual void DrawMe()
        {
            DrawRectangleRec(rect, color);
        }

        public virtual bool CheckIfClicked()
        {
            return CheckCollisionPointRec(GetMousePosition(), rect) && IsMouseButtonPressed(0);
        }

        public virtual void DrawMeWithLines(int lineThicknes, Color lineColor)
        {
            this.DrawMe();
            DrawRectangleLinesEx(rect, lineThicknes, lineColor);
        }
    }
}
