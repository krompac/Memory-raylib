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
        int resetTimer;
        int lastTimeFrame;
        private List<Button> menuItems;
        private List<Card> memCards;
        private Button toMenu;
        private Button startButton;
        private Button optionsButton;
        private Button quitButton;
        private GameWindow gameWindow;
        private Card firstOpenedCard;
        private Card lastOpenedCard;
        private UI_Element gameWonPanel;
        private Button gameWonButton;


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

                        if (memCards.Count(card => card.IsFound) == memCards.Count)
                        {
                            DrawGameWon();
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

            startButton = new Button(xPos, yPos, 150, 50, "Start Game", GameWindow.Game);
            optionsButton = new Button(xPos, yPos + yDiff, 150, 50, "Options", GameWindow.Options);
            quitButton = new Button(xPos, yPos + yDiff * 2, 150, 50, "Quit", GameWindow.Quit);
            toMenu = new Button(590, 460, 100, 20, "Back", GameWindow.Menu);

            gameWonPanel = new UI_Element(325, 110, 480, 220, "YOU WON");
            gameWonButton = new Button(280, 220, 50, 100, "Okay", GameWindow.Menu);

            menuItems = new List<Button> { startButton, optionsButton, quitButton };
            gameWindow = GameWindow.Menu;

            InitializeMainGame();
        }

        private void InitializeMainGame()
        {
            memCards = new List<Card>();

            int startinXPos = 110;

            int x = startinXPos;
            int y = 20;

            firstOpenedCard = null;
            lastOpenedCard = null;

            resetTimer = 0;
            lastTimeFrame = 0;

            Dictionary<string, int> images = new Dictionary<string, int>();

            var imageList = Directory.GetFiles("willdabeast").ToList();
            imageList.ForEach(name => images.Add(name, 0));
            var imageCount = imageList.Count - 2;

            int j = 0;
            Random random = new Random();

            while (memCards.Count != imageCount * 2)
            {
                j = random.Next(imageCount);
                Console.WriteLine(j);

                if (images[imageList[j]] != 2)
                {
                    memCards.Add(new Card(x, y, 55, 55, j, "CARD", imageList[j]));
                    x += 65;

                    images[imageList[j]]++;

                    if ((memCards.Count) % 6 == 0)
                    {
                        y += 65;
                        x = startinXPos;
                    }
                }
            }
        }

        private void DrawGameWon()
        {
            gameWonPanel.DrawMe();
            DrawText("YOU WON", 100, 110, 100, Color.BLACK);
            gameWonButton.DrawMeWithLines(3, Color.BLACK);

            if (gameWonButton.CheckIfClicked())
            {
                gameWindow = gameWonButton.Window;
                InitializeMainGame();
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

        private void ResetCounters()
        {
            resetTimer = 0;
            lastTimeFrame = 0;
        }

        private void ResetCards()
        {
            firstOpenedCard = null;
            lastOpenedCard = null;
        }

        private bool CheckMatchedCard()
        {
            return firstOpenedCard != null && lastOpenedCard != null && firstOpenedCard.CardID == lastOpenedCard.CardID && firstOpenedCard != lastOpenedCard;
        }

        private void DrawGameWindow()
        {
            memCards.ForEach(card => card.DrawMe());
            toMenu.DrawMe();
            if (toMenu.CheckIfClicked())
            {
                gameWindow = toMenu.Window;
                InitializeMainGame();
            }
            else
            {
                if (firstOpenedCard == null)
                {
                    firstOpenedCard = memCards.Where(card => card.CheckIfClicked()).FirstOrDefault();
                }
                else
                {
                    lastOpenedCard = memCards.Where(card => card.CheckIfClicked()).FirstOrDefault();

                    if (lastOpenedCard == firstOpenedCard)
                    {
                        lastOpenedCard = null;
                        Card.NumberOfOpenCards = 1;
                    }
                }

                if (Card.NumberOfOpenCards == 2)
                {
                    if (CheckMatchedCard())
                    {
                        memCards.Where(card => card == firstOpenedCard || card == lastOpenedCard).ToList().ForEach(card => card.IsFound = true);
                        ResetCounters();
                        ResetCards();
                        Card.NumberOfOpenCards = 0;
                    }
                    else
                    {
                        if (resetTimer == 3)
                        {
                            memCards.ForEach(card => card.ResetMe());
                            Card.NumberOfOpenCards = 0;
                            ResetCounters();
                            ResetCards();
                        }
                        else if (lastTimeFrame > 0 && DateTime.Now.Second - lastTimeFrame >= 1)
                        {
                            resetTimer++;
                            lastTimeFrame = DateTime.Now.Second;
                        }
                        else
                        {
                            lastTimeFrame = DateTime.Now.Second;
                        }
                    }
                }
                else if (lastOpenedCard != null && Card.NumberOfOpenCards > 2)
                {
                    memCards.Where(card => card != lastOpenedCard).ToList().ForEach(card => card.ResetMe());
                    Card.NumberOfOpenCards = 1;
                    ResetCounters();
                    firstOpenedCard = null;
                }
            }
        }
    }
}
