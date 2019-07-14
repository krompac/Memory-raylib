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
                        DrawToMenu();
                        gameplay.DrawGameplay(ref gameWindow);

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

            yPos = 100;

            var onePlayerButton = new Button(xPos, yPos, 150, 50, "1 player", GameWindow.Game);
            var twoPlayersButton = new Button(xPos, yPos + yDiff, 150, 50, "2 players", GameWindow.Game);
            var threePlayersButton = new Button(xPos, yPos + yDiff * 2, 150, 50, "3 players", GameWindow.Game);
            var fourPlayersButton = new Button(xPos, yPos + yDiff * 3, 150, 50, "4 players", GameWindow.Game);

            toMenu = new Button(590, 460, 100, 20, "Back", GameWindow.Menu);

            menuItems = new List<Button> { startButton, optionsButton, quitButton };
            playersButtons = new List<Button> { onePlayerButton, twoPlayersButton, threePlayersButton, fourPlayersButton };
            gameWindow = GameWindow.Menu;

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

        private void DrawToMenu()
        {
            toMenu.DrawMe();
            if (toMenu.CheckIfClicked())
            {
                gameWindow = toMenu.Window;
            }
        }

        private void InitializePlayers(int buttonPosition)
        {
            var players = new List<Player>();
            switch (buttonPosition)
            {
                case 3:
                    players.Add(new Player("Player4", Color.ORANGE, 3));
                    goto case 2;
                case 2:
                    players.Add(new Player("Player3", Color.GREEN, 2));
                    goto case 1;
                case 1:
                    players.Add(new Player("Player2", Color.RED, 1));
                    goto case 0;
                case 0:
                    players.Add(new Player("Player1", Color.BLUE, 0));
                    break;
                default:
                    break;
            }

            players.Reverse();
            gameplay.Players = players;
        }

        private void DrawPlayerSelectionMenu()
        {
            playersButtons.ForEach(button => button.DrawMe());
            var pressedButton = playersButtons.Where(button => button.CheckIfClicked()).FirstOrDefault();

            if (pressedButton != null)
            {
                gameWindow = pressedButton.Window;
                int buttonPosition = playersButtons.IndexOf(pressedButton);
                gameplay.InitializeMainGame();
                InitializePlayers(buttonPosition);
                System.Threading.Thread.Sleep(500);
            }

            DrawToMenu();
        }
    }
}
