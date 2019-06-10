using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using rl = Raylib.Raylib;

namespace Memory
{
    enum GameWindow
    {
        Menu,
        Game,
        Options,
        Quit
    }

    class UI_Element
    {
        public UI_Element(int x, int y, int w, int h, string text, GameWindow window)
        {
            rect = new Rectangle(x - (w / 2), y, w, h);
            this.text = text;
            color = Color.BLUE;
            this.window = window;
        }

        public void DrawMe()
        {
            rl.DrawRectangleRec(rect, color);
            rl.DrawText(text, (int)rect.x + text.Length, (int)rect.y + ((int)rect.height / 3), (int)rect.height / 2, Color.RED);
        }

        public void CheckIfClicked(ref GameWindow window)
        {
            if (rl.CheckCollisionPointRec(rl.GetMousePosition(), rect))
            {
                if (rl.IsMouseButtonPressed(0))
                {
                    window = this.window;
                }
            }
        }

        private Rectangle rect;
        private string text;
        private Color color;
        private GameWindow window;
    }

    class Program
    {
        static List<UI_Element> menuItems;
        static List<UI_Element> memCards;
        static UI_Element toMenu;
        static GameWindow gameWindow;

        static void DrawMenuWindow()
        {
            menuItems.ForEach(rect => rect.DrawMe());
            menuItems.ForEach(x => x.CheckIfClicked(ref gameWindow));
        }

        static void DrawGameWindow()
        {
            memCards.ForEach(card => card.DrawMe());
            toMenu.DrawMe();
            toMenu.CheckIfClicked(ref gameWindow);
        }

        static void Main(string[] args)
        {
            rl.InitWindow(640, 480, "Memory");
            UI_Element startRect = new UI_Element(320, 215, 150, 50, "Start Game", GameWindow.Game);
            UI_Element optionsRect = new UI_Element(320, 280, 150, 50, "Options", GameWindow.Options);
            UI_Element quitRect = new UI_Element(320, 345, 150, 50, "Quit", GameWindow.Quit);
            toMenu = new UI_Element(590, 460, 100, 20, "Back", GameWindow.Menu);
            
            menuItems = new List<UI_Element> { startRect, optionsRect, quitRect };
            gameWindow = GameWindow.Menu;

            memCards = new List<UI_Element>();
            int x = 95;
            int y = 20;

            for (int i = 0; i < 16; i++)
            {
                memCards.Add(new UI_Element(x, y, 140, 100, "CARD", GameWindow.Game));
                x += 150;

                if ((i + 1) % 4 == 0)
                {
                    y += 110;
                    x = 95;
                }
            }

            while (!rl.WindowShouldClose())
            {
                rl.BeginDrawing();

                rl.ClearBackground(Color.WHITE);

                switch (gameWindow)
                {
                    case GameWindow.Menu:
                        DrawMenuWindow();
                        break;
                    case GameWindow.Game:
                        DrawGameWindow();
                        break;
                    case GameWindow.Options:
                        break;
                    case GameWindow.Quit:
                        rl.EndDrawing();
                        rl.CloseWindow();
                        Environment.Exit(1);
                        break;
                }


                rl.EndDrawing();

                System.Threading.Thread.Sleep(10);
            }

            rl.CloseWindow();
        }
    }
}
