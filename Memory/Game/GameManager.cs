using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class GameManager
    {
        private bool windowSizeChanged;
        private readonly int windowWidth;
        private readonly int windowHeight;
        private int numberOfPlayers;
        private List<Button> menuItems;
        private List<Button> playersButtons;
        private List<Button> difficultysButtons;
        private ToMenuButton toMenu;
        private GameWindow gameWindow;
        private Gameplay gameplay;
        private OptionsManager optionsManager;
        private Texture2D titlePicture;

        public GameManager(int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            windowSizeChanged = false;
            numberOfPlayers = 0;
            gameplay = null;
            optionsManager = new OptionsManager();
            InitializeGame();

            titlePicture = LoadTexture(Program.GetTitlePath());
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
                        
                        DrawTexture(titlePicture, (windowWidth - titlePicture.width) / 2, 20, Color.BLUE);
                        DrawWindowWithButtons(menuItems);
                        SoundManager.Instance.MenuTheme();
                        break;
                    case GameWindow.PlayerSelect:
                        DrawPlayerSelectionMenu();
                        break;
                    case GameWindow.DifficultySelect:
                        DrawDifficultySelectionMenu();
                        break;
                    case GameWindow.Game:
                        gameplay.DrawGameplay(ref gameWindow);
                        SoundManager.Instance.GameplayTheme();

                        if (gameplay.CheckIfWon())
                        {
                            gameplay.DrawGameWon(ref gameWindow);
                        }
                        break;
                    case GameWindow.Options:
                        optionsManager.HandleMe();
                        break;
                    case GameWindow.Quit:
                        ExitGame();
                        break;
                }

                if (gameWindow != GameWindow.Menu && gameWindow != GameWindow.Quit)
                {
                    DrawToMenu();
                }

                EndDrawing();
                Thread.Sleep(10);
            }
        }

        private void InitializeGame()
        {
            int xPos = 245;
            int yPos = 150;
            int yDiff = 65;

            var startButton = new Button(xPos, yPos, 150, 50, "Start Game", GameWindow.PlayerSelect);
            var optionsButton = new Button(xPos, yPos + yDiff, 150, 50, "Options", GameWindow.Options);
            var quitButton = new Button(xPos, yPos + yDiff * 2, 150, 50, "Quit", GameWindow.Quit);

            yPos = 100;

            var onePlayerButton = new Button(xPos, yPos, 150, 50, "1 player", GameWindow.DifficultySelect);
            var twoPlayersButton = new Button(xPos, yPos + yDiff, 150, 50, "2 players", GameWindow.DifficultySelect);
            var threePlayersButton = new Button(xPos, yPos + yDiff * 2, 150, 50, "3 players", GameWindow.DifficultySelect);
            var fourPlayersButton = new Button(xPos, yPos + yDiff * 3, 150, 50, "4 players", GameWindow.DifficultySelect);

            var easyButton = new Button(xPos, yPos, 150, 50, "Easy", GameWindow.Game);
            var mediumButton = new Button(xPos, yPos + yDiff, 150, 50, "Medium", GameWindow.Game);
            var hardButton = new Button(xPos, yPos + yDiff * 2, 150, 50, "Hard", GameWindow.Game);

            toMenu = new ToMenuButton(540, 460, 100, 20, "Back", GameWindow.Menu);

            menuItems = new List<Button> { startButton, optionsButton, quitButton };
            playersButtons = new List<Button> { onePlayerButton, twoPlayersButton, threePlayersButton, fourPlayersButton };
            difficultysButtons = new List<Button> { easyButton, mediumButton, hardButton };
            gameWindow = GameWindow.Menu;
        }

        private void ExitGame()
        {
            Thread.Sleep(500);
            EndDrawing();
            CloseWindow();
            Environment.Exit(1);
        }

        private void UpdateWindowSize()
        {
            if (windowSizeChanged)
            {
                var width = windowWidth;
                var height = windowHeight;

                switch(numberOfPlayers)
                {
                    case 3:
                        width += 60;
                        goto case 2;
                    case 2:
                        height += 60;
                        width += 60;
                        goto case 1;
                    case 1:
                        height += 60;
                        break;
                }

                SetWindowSize(width, height);
                toMenu.UpdatePosition(width, height);

                if (gameplay != null)
                {
                    gameplay.UpdatePlayersTextPosition(width);
                }

                windowSizeChanged = false;
            }
        }

        private void DrawWindowWithButtons(List<Button> buttons)
        {
            buttons.ForEach(button => button.DrawMe());
            buttons.ForEach(button =>
            {
                if (button.CheckIfClicked())
                {
                    gameWindow = button.Window;
                }
            });
        }

        private void DrawToMenu()
        {
            toMenu.DrawMe();
            if (toMenu.CheckIfClicked())
            {
                if (gameWindow == GameWindow.Game)
                {
                    SoundManager.Instance.ResetMusic();
                }

                if (numberOfPlayers > 0)
                {
                    windowSizeChanged = true;
                    numberOfPlayers = 0;
                    UpdateWindowSize();
                }

                gameWindow = toMenu.Window;
            }
        }

        private void InitializePlayers(int buttonIndex)
        {
            var players = new List<Player>();
            switch (buttonIndex)
            {
                case 3:
                    players.Add(new Player("Player4", Color.ORANGE, 3));
                    goto case 2;
                case 2:
                    players.Add(new Player("Player3", Color.GREEN, 2));
                    goto case 1;
                case 1:
                    windowSizeChanged = true;
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
                numberOfPlayers = playersButtons.IndexOf(pressedButton);
            }
        }

        private void DrawDifficultySelectionMenu()
        {
            difficultysButtons.ForEach(button => button.DrawMe());
            var pressedButton = difficultysButtons.Where(button => button.CheckIfClicked()).FirstOrDefault();

            if (pressedButton != null)
            {
                gameWindow = pressedButton.Window;
                var difficultyIndex = difficultysButtons.IndexOf(pressedButton);

                gameplay = new Gameplay((Difficulty)difficultyIndex);
                InitializePlayers(numberOfPlayers);
                gameplay.InitializeMainGame();

                Thread.Sleep(500);
                UpdateWindowSize();
            }
        }
    }
}