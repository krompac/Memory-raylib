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

            InitializeMainGame();
        }

        private void InitializeMainGame()
        {
            memCards = new List<Card>();

            int x = 95;
            int y = 20;

            firstOpenedCard = null;
            lastOpenedCard = null;

            resetTimer = 0;
            lastTimeFrame = 0;

            Dictionary<string, int> images = new Dictionary<string, int>();

            var imageList = Directory.GetFiles("willdabeast").ToList();
            imageList.ForEach(name => images.Add(name, 0));
            var imageCount = 8;

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

                    if ((memCards.Count) % 4 == 0)
                    {
                        y += 65;
                        x = 95;
                    }
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
                        memCards.Remove(firstOpenedCard);
                        memCards.Remove(lastOpenedCard);
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
