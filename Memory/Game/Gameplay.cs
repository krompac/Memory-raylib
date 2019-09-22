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
        private GameWonPanel gameWonPanel;
        private int currentPlayerIndex;
        private Timer timer;
        private List<Card> memCards;
        private Card firstOpenedCard;
        private Card lastOpenedCard;
        public List<Player> Players { private get; set; }

        public Gameplay(Difficulty difficulty)
        {
            timer = new Timer(ResetForNextTurn, difficulty);

            gameWonPanel = new GameWonPanel(SoundManager.Instance.GameWon);
        }

        public void InitializeMainGame()
        {
            timer.GameState = GameState.NotOpened;
            timer.OuterRectColor = Color.BLUE;

            firstOpenedCard = null;
            lastOpenedCard = null;

            InitializeCards();

            currentPlayerIndex = 0;
        }

        private void InitializeCards()
        {
            int pictureCount = 1;
            int rowWidth = 6;

            switch (Players.Count)
            {
                case 4:
                    timer.UpdatePosition(65);
                    rowWidth++;
                    pictureCount += 4;
                    goto case 3;
                case 3:
                    timer.UpdatePosition(65);
                    pictureCount += 7;
                    rowWidth++;
                    goto case 2;
                case 2:
                    pictureCount += 6;
                    break;
            }

            memCards = new List<Card>();

            int startinXPos = 110;

            int x = startinXPos;
            int y = 60;

            Dictionary<string, int> images = new Dictionary<string, int>();

            var imageList = Directory.GetFiles(Program.GetCardsPath()).ToList();
            var smallList = new List<string>();

            Random random = new Random();
            int j = 0;
            string randomPicture = "";

            while (smallList.Count < pictureCount)
            {
                j = random.Next(imageList.Count);
                randomPicture = imageList[j];
                smallList.Add(randomPicture);
                imageList.Remove(randomPicture);
            }

            smallList.ForEach(name => images.Add(name, 0));

            while (memCards.Count != smallList.Count * 2)
            {
                j = random.Next(smallList.Count);

                if (images[smallList[j]] != 2)
                {
                    memCards.Add(new Card(x, y, 55, 55, j, smallList[j]));
                    x += 65;

                    images[smallList[j]]++;

                    if ((memCards.Count) % rowWidth == 0)
                    {
                        y += 65;
                        x = startinXPos;
                    }
                }
            }

            Card.NumberOfOpenCards = 0;
        }

        public void UpdatePlayersTextPosition(int width)
        {
            Players.ForEach(player => player.TextXpos = width - 140);
        }

        public bool CheckIfWon()
        {
            var isWon = memCards.Count(card => card.IsFound) == memCards.Count;

            if (isWon)
            {
                var winner = Players.OrderBy(player => player.Score).FirstOrDefault();

                gameWonPanel.Init(winner);
            }

            return isWon;
        }

        public void DrawGameWon(ref GameWindow gameWindow)
        {
            gameWonPanel.DrawMeWithLines(3, Color.WHITE);

            if (gameWonPanel.CheckIfClicked())
            {
                gameWindow = gameWonPanel.Window;
            }
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
            timer.DrawMe();
            memCards.ForEach(card => card.DrawMe());

            if (Players.Count > 1)
            {
                Players.ForEach(player => player.DrawMe(currentPlayerIndex));
            }

            CheckForOpenCards();
            HandleOpenCards();

            timer.UpdateTimer((int)DateTime.Now.TimeOfDay.TotalMilliseconds);
        }

        private void CheckForOpenCards()
        {
            if (Card.NumberOfOpenCards < 2 || Players.Count == 1)
            {
                if (firstOpenedCard == null)
                {
                    firstOpenedCard = memCards.Where(card => card.CheckIfClicked()).FirstOrDefault();

                    if (firstOpenedCard != null)
                    {
                        SoundManager.Instance.OpenCard();
                    }
                }
                else
                {
                    lastOpenedCard = memCards.Where(card => card.CheckIfClicked()).FirstOrDefault();

                    if (lastOpenedCard == firstOpenedCard)
                    {
                        lastOpenedCard = null;
                        memCards.Where(card => card != firstOpenedCard).ToList().ForEach(card => card.ResetMe());
                        Card.NumberOfOpenCards = 1;
                    }

                    if (lastOpenedCard != null)
                    {
                        SoundManager.Instance.OpenCard();
                    }
                }
            }
        }

        private void HandleOpenCards()
        {
            if (Card.NumberOfOpenCards > 0 && timer.GameState == GameState.NotOpened)
            {
                timer.GameState = GameState.Opened;
            }

            if (Card.NumberOfOpenCards == 2)
            {
                if (CheckMatchedCard())
                {
                    SoundManager.Instance.MatchedCard();
                    Players[currentPlayerIndex].Score++;
                    memCards.Where(card => card == firstOpenedCard || card == lastOpenedCard).ToList().ForEach(card => card.IsFound = true);
                    ResetCards();
                    Card.NumberOfOpenCards = 0;
                    timer.GameState = GameState.NotOpened;
                    timer.UpdateProgresBar();
                }
                else
                {
                    if (timer.GameState == GameState.Opened)
                    {
                        timer.GameState = GameState.Reset;
                        timer.UpdateProgresBar();
                    }
                }
            }
            else if (Players.Count == 1 && lastOpenedCard != null && Card.NumberOfOpenCards > 2)
            {
                timer.GameState = GameState.Opened;
                timer.UpdateProgresBar();
                memCards.Where(card => card != lastOpenedCard).ToList().ForEach(card => card.ResetMe());
                Card.NumberOfOpenCards = 1;
                firstOpenedCard = lastOpenedCard;
                lastOpenedCard = null;
            }
        }

        private void ResetForNextTurn()
        {
            memCards.ForEach(card => card.ResetMe());
            Card.NumberOfOpenCards = 0;
            timer.GameState = GameState.NotOpened;
            ResetCards();

            if (Players.Count > 0)
            {
                currentPlayerIndex = currentPlayerIndex + 1 < Players.Count ? currentPlayerIndex + 1 : 0;
                memCards.ForEach(card => card.Color = Players[currentPlayerIndex].MyColor);
                timer.OuterRectColor = Players[currentPlayerIndex].MyColor;
            }
        }
    }
}