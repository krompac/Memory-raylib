using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Memory
{
    class UI_Element : IDrawable
    {
        protected Rectangle rect;
        protected Color color;

        public Color Color
        {
            set
            {
                color = value;
            }
        }

        public UI_Element()
        {
            rect = new Rectangle();
        }

        public UI_Element(int x, int y, int w, int h)
        {
            rect = new Rectangle(x, y, w, h);
            color = Color.Blue;
        }

        public UI_Element(int x, int y, int w, int h, Color color) : this(x, y, w, h)
        {
            rect.X = x;
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
            DrawMe();
            DrawRectangleLinesEx(rect, lineThicknes, lineColor);
        }
    }
}
