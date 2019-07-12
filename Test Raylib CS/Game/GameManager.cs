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
        private List<Button> playersButtons;
        private Button toMenu;
        private Players numberOfPlayers;
        private GameWindow gameWindow;
        private Gameplay gameplay;

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
                        DrawWindowWithButtons(menuItems);
                        break;
                    case GameWindow.PlayerSelect:
                        DrawPlayerSelectionMenu();
                        break;
                    case GameWindow.Game:
                        gameplay.DrawGameplay(ref gameWindow);
                        DrawToMenu(true);

                        if (gameplay.CheckIfWon())
                        {
                            gameplay.DrawGameWon(ref gameWindow);
                        }
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
            int xPos = 320;
            int yPos = 150;
            int yDiff = 65;

            var startButton = new Button(xPos, yPos, 150, 50, "Start Game", GameWindow.PlayerSelect);
            var optionsButton = new Button(xPos, yPos + yDiff, 150, 50, "Options", GameWindow.Options);
            var quitButton = new Button(xPos, yPos + yDiff * 2, 150, 50, "Quit", GameWindow.Quit);

            var onePlayerButton = new Button(xPos, yPos, 150, 50, "1 player", GameWindow.Game);
            var twoPlayersButton = new Button(xPos, yPos + yDiff, 150, 50, "2 players", GameWindow.Game);

            toMenu = new Button(590, 460, 100, 20, "Back", GameWindow.Menu);

            menuItems = new List<Button> { startButton, optionsButton, quitButton };
            playersButtons = new List<Button> { onePlayerButton, twoPlayersButton };
            gameWindow = GameWindow.Menu;
            numberOfPlayers = Players.OnePLayer;

            gameplay = new Gameplay();

            gameplay.InitializeMainGame();
        }

        private void DrawWindowWithButtons(List<Button> buttons)
        {
            buttons.ForEach(button => button.DrawMe());

            foreach (var item in buttons)
            {
                if (item.CheckIfClicked())
                {
                    gameWindow = item.Window;
                }
            }
        }

        private void DrawToMenu(bool reinitializeGameplay = false)
        {
            toMenu.DrawMe();
            if (toMenu.CheckIfClicked())
            {
                gameWindow = toMenu.Window;

                if (reinitializeGameplay)
                {
                    gameplay.InitializeMainGame();
                }
            }
        }

        private void DrawPlayerSelectionMenu()
        {
            DrawWindowWithButtons(playersButtons);
            DrawToMenu();
        }
    }
}
