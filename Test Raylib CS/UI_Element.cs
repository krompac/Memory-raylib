using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class UI_Element
    {
        public UI_Element(int x, int y, int w, int h, string text)
        {
            rect = new Rectangle(x - (w / 2), y, w, h);
            this.text = text;
            color = Color.BLUE;
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

        protected Rectangle rect;
        protected string text;
        protected Color color;
    }
}
