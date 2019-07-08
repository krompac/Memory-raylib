using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class GameManager
    {
        private List<Button> menuItems;
        private List<Card> memCards;
        private Button toMenu;
        private Button startButton;
        private Button optionsButton;
        private Button quitButton;

        private GameWindow gameWindow;

        public GameManager()
        {
            InitializeGame();
        }

        public void GameLoop()
        {
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
        }

        private void InitializeGame()
        {
            startButton = new Button(320, 215, 150, 50, "Start Game", GameWindow.Game);
            optionsButton = new Button(320, 280, 150, 50, "Options", GameWindow.Options);
            quitButton = new Button(320, 345, 150, 50, "Quit", GameWindow.Quit);
            toMenu = new Button(590, 460, 100, 20, "Back", GameWindow.Menu);

            menuItems = new List<Button> { startButton, optionsButton, quitButton };
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
        }

        private void DrawMenuWindow()
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

        private void DrawGameWindow()
        {
            memCards.ForEach(card => card.DrawMe());
            toMenu.DrawMe();
            if (toMenu.CheckIfClicked())
            {
                gameWindow = toMenu.Window;
            }

            memCards.ForEach(card => card.CheckIfClicked());
        }
    }
}
