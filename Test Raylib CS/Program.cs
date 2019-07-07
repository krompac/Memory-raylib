using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

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

        protected Rectangle rect;
        protected string text;
        protected Color color;
        
    }

    class Card : UI_Element
    {
        public Card(int x, int y, int w, int h, string text, string fileName = "") : base(x, y, w, h, text)
        {
            if (fileName != "")
            {
                image = LoadTexture(fileName);
            }

            position = new Vector2(rect.x, y);
            drawImage = false;
        }

        public override void DrawMe()
        {
            base.DrawMe();

            if (drawImage)
                DrawTextureEx(image, position, 0, 0.1f, Color.WHITE);
        }

        public override bool CheckIfClicked()
        {
            if (base.CheckIfClicked())
            {
                drawImage = true;
            }


            return true;
        }

        bool drawImage;
        Texture2D image;
        Vector2 position;
    }

    class Button : UI_Element
    {
        public Button(int x, int y, int w, int h, string text, GameWindow window) : base(x, y, w, h, text)
        {
            this.Window = window;
        }

        public override void DrawMe()
        {
            base.DrawMe();

            DrawText(text, (int)rect.x + text.Length, (int)rect.y + ((int)rect.height / 3), (int)rect.height / 2, Color.RED);
        }

        public GameWindow Window { get; private set; }
    }

    class Program
    {
        static List<Button> menuItems;
        static List<Card> memCards;
        static Button toMenu;
        static GameWindow gameWindow;

        static void DrawMenuWindow()
        {
            menuItems.ForEach(rect => rect.DrawMe());

            foreach (var item in menuItems)
            {
                if (item.CheckIfClicked())
                {
                    gameWindow = item.Window;
                }
            }
        }

        static void DrawGameWindow()
        {
            memCards.ForEach(card => card.DrawMe());
            toMenu.DrawMe();
            if (toMenu.CheckIfClicked())
            {
                gameWindow = toMenu.Window;
            }

            memCards.ForEach(card => card.CheckIfClicked());
        }

        static void Main(string[] args)
        {
            InitWindow(640, 480, "Memory");
            var startRect = new Button(320, 215, 150, 50, "Start Game", GameWindow.Game);
            var optionsRect = new Button(320, 280, 150, 50, "Options", GameWindow.Options);
            var quitRect = new Button(320, 345, 150, 50, "Quit", GameWindow.Quit);
            toMenu = new Button(590, 460, 100, 20, "Back", GameWindow.Menu);

            menuItems = new List<Button> { startRect, optionsRect, quitRect };
            gameWindow = GameWindow.Menu;


            memCards = new List<Card>();

            int x = 95;
            int y = 20;

            var images = Directory.GetFiles("willdabeast").ToList();
            var imageCount = images.Count;

            for (int i = 0; i < imageCount; i++)
            {
                memCards.Add(new Card(x, y, 55, 55, "CARD", images[i]));
                x += 65;

                if ((i + 1) % 4 == 0)
                {
                    y += 65;
                    x = 95;
                }
            }

            while (!WindowShouldClose())
            {
                BeginDrawing();

                ClearBackground(Color.BLACK);

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
                        EndDrawing();
                        CloseWindow();
                        Environment.Exit(1);
                        break;
                }

                EndDrawing();

                System.Threading.Thread.Sleep(10);
            }

            CloseWindow();
        }
    }
}
