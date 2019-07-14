using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Gameplay
    {
        private bool playerChanged;
        private int resetTimer;
        private int lastTimeFrame;
        private List<Card> memCards;
        private Card firstOpenedCard;
        private Card lastOpenedCard;
        private UI_Element gameWonPanel;
        private Button gameWonButton;
        public List<Player> Players { private get; set; }
        int currentPlayerIndex;

        public Gameplay()
        {
            gameWonPanel = new UI_Element(325, 110, 480, 220);
            gameWonButton = new Button(280, 220, 100, 50, "Okay", GameWindow.Menu);
        }

        public void InitializeMainGame()
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

            var imageList = Directory.GetFiles(Program.GetCardsPath()).ToList();
            imageList.ForEach(name => images.Add(name, 0));
            var imageCount = imageList.Count - 2;

            int j = 0;
            Random random = new Random();

            while (memCards.Count != imageCount * 2)
            {
                j = random.Next(imageCount);

                if (images[imageList[j]] != 2)
                {
                    memCards.Add(new Card(x, y, 55, 55, j, imageList[j]));
                    x += 65;

                    images[imageList[j]]++;

                    if ((memCards.Count) % 6 == 0)
                    {
                        y += 65;
                        x = startinXPos;
                    }
                }
            }

            currentPlayerIndex = 0;
            playerChanged = false;
        }

        public bool CheckIfWon()
        {
            return memCards.Count(card => card.IsFound) == memCards.Count;
        }

        public void DrawGameWon(ref GameWindow gameWindow)
        {
            gameWonPanel.DrawMeWithLines(5, Color.WHITE);
            var winner = Players.OrderBy(player => player.Score).FirstOrDefault();
            DrawText(winner.Name.ToUpper() + " WON", 95, 110, 100, Color.BLACK);
            gameWonButton.DrawMeWithLines(3, Color.BLACK);

            if (gameWonButton.CheckIfClicked())
            {
                gameWindow = gameWonButton.Window;
                InitializeMainGame();
            }
        }

        private void ResetCounters()
        {
            resetTimer = 0;
            lastTimeFrame = 0;
            playerChanged = false;
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

        public void DrawGameplay(ref GameWindow gameWindow)
        {
            memCards.ForEach(card => card.DrawMe());

            if (Players.Count > 0)
            {
                Players.ForEach(player => player.DrawMe(currentPlayerIndex));
            }

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
                    memCards.Where(card => card != firstOpenedCard).ToList().ForEach(card => card.ResetMe());
                    Card.NumberOfOpenCards = 1;
                    ResetCounters();
                }
            }

            if (Card.NumberOfOpenCards == 2)
            {
                if (CheckMatchedCard())
                {
                    Players[currentPlayerIndex].Score++;
                    memCards.Where(card => card == firstOpenedCard || card == lastOpenedCard).ToList().ForEach(card => card.IsFound = true);
                    ResetCounters();
                    ResetCards();
                    Card.NumberOfOpenCards = 0;
                }
                else
                {
                    if (!playerChanged && Players.Count > 0)
                    {
                        currentPlayerIndex = currentPlayerIndex + 1 < Players.Count ? currentPlayerIndex + 1 : 0;
                        memCards.ForEach(card => card.SetColor(Players[currentPlayerIndex].MyColor));
                        playerChanged = true;
                    }

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
                firstOpenedCard = lastOpenedCard;
                lastOpenedCard = null;
            }
        }
    }
}
