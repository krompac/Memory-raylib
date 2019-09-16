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
        private bool gameWonSound;
        private int resetTimer;
        private readonly int maxResetTime;
        private int turnTimer;
        private int currentTurnTime;
        private int currentTurnTimeFrame;
        private int lastTimeFrame;
        private int currentPlayerIndex;
        private List<Card> memCards;
        private Card firstOpenedCard;
        private Card lastOpenedCard;
        private UI_Element gameWonPanel;
        private Button gameWonButton;
        public List<Player> Players { private get; set; }

        public Gameplay(Difficulty difficulty)
        {
            gameWonPanel = new UI_Element(325, 110, 480, 220);
            gameWonButton = new Button(280, 220, 100, 50, "Okay", GameWindow.Menu);

            maxResetTime = 3 - (int)difficulty + 1;
            InitGameByDifficulty(difficulty);
        }

        public void InitializeMainGame()
        {
            gameWonSound = true;
            firstOpenedCard = null;
            lastOpenedCard = null;

            resetTimer = 0;
            lastTimeFrame = 0;

            InitializeCards();

            currentPlayerIndex = 0;
        }

        private void InitGameByDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    turnTimer = -1;
                    break;
                case Difficulty.Medium:
                    turnTimer = 3;
                    break;
                case Difficulty.Hard:
                    turnTimer = 1;
                    break;
            }

            currentTurnTime = 0;
        }

        private void InitializeCards()
        {
            memCards = new List<Card>();

            int startinXPos = 110;

            int x = startinXPos;
            int y = 20;

            Dictionary<string, int> images = new Dictionary<string, int>();

            var imageList = Directory.GetFiles(Program.GetCardsPath()).ToList();
            var smallList = new List<string>();

            Random random = new Random();
            int j = 0;
            string randomPicture = "";

            while (smallList.Count < 15)
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

                    if ((memCards.Count) % 6 == 0)
                    {
                        y += 65;
                        x = startinXPos;
                    }
                }
            }

            Card.NumberOfOpenCards = 0;
        }

        public bool CheckIfWon()
        {
            return memCards.Count(card => card.IsFound) == memCards.Count;
        }

        public void DrawGameWon(ref GameWindow gameWindow)
        {
            if (gameWonSound)
            {
                SoundManager.Instance.GameWon();
                gameWonSound = false;
            }

            gameWonPanel.DrawMeWithLines(5, Color.WHITE);
            var winner = Players.OrderBy(player => player.Score).FirstOrDefault();
            DrawText(winner.Name.ToUpper() + " WON", 95, 110, 100, Color.BLACK);
            gameWonButton.DrawMeWithLines(3, Color.BLACK);

            if (gameWonButton.CheckIfClicked())
            {
                gameWindow = gameWonButton.Window;
                //InitializeMainGame();
            }
        }

        private void ResetCounters()
        {
            resetTimer = 0;
            lastTimeFrame = 0;
            currentTurnTime = 0;
            currentTurnTimeFrame = 0;
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

            if (Players.Count > 1)
            {
                Players.ForEach(player => player.DrawMe(currentPlayerIndex));
            }

            CheckForOpenCards();
            HandleOpenCards();
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
                        ResetCounters();
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
            if (turnTimer > 0 && Card.NumberOfOpenCards > 0)
            {
                if (currentTurnTimeFrame == 0)
                {
                    currentTurnTimeFrame = (int)DateTime.Now.TimeOfDay.TotalSeconds;
                }
                else if ((int)DateTime.Now.TimeOfDay.TotalSeconds - currentTurnTimeFrame >= 1)
                {
                    currentTurnTime++;
                    currentTurnTimeFrame = (int)DateTime.Now.TimeOfDay.TotalSeconds;

                    if (currentTurnTime == turnTimer)
                    {
                        ResetForNextTurn();
                    }
                }
            }

            if (Card.NumberOfOpenCards == 2)
            {
                if (CheckMatchedCard())
                {
                    SoundManager.Instance.MatchedCard();
                    Players[currentPlayerIndex].Score++;
                    memCards.Where(card => card == firstOpenedCard || card == lastOpenedCard).ToList().ForEach(card => card.IsFound = true);
                    ResetCounters();
                    ResetCards();
                    Card.NumberOfOpenCards = 0;
                }
                else
                {
                    if (resetTimer == maxResetTime)
                    {
                        ResetForNextTurn();
                    }
                    else if (lastTimeFrame > 0 && (int)DateTime.Now.TimeOfDay.TotalSeconds - lastTimeFrame >= 1)
                    {
                        resetTimer++;
                        lastTimeFrame = (int)DateTime.Now.TimeOfDay.TotalSeconds;
                    }
                    else
                    {
                        lastTimeFrame = (int)DateTime.Now.TimeOfDay.TotalSeconds;
                    }
                }
            }
            else if (Players.Count == 1 && lastOpenedCard != null && Card.NumberOfOpenCards > 2)
            {
                memCards.Where(card => card != lastOpenedCard).ToList().ForEach(card => card.ResetMe());
                Card.NumberOfOpenCards = 1;
                ResetCounters();
                firstOpenedCard = lastOpenedCard;
                lastOpenedCard = null;
            }
        }

        private void ResetForNextTurn()
        {
            memCards.ForEach(card => card.ResetMe());
            Card.NumberOfOpenCards = 0;
            ResetCounters();
            ResetCards();

            if (Players.Count > 0)
            {
                currentPlayerIndex = currentPlayerIndex + 1 < Players.Count ? currentPlayerIndex + 1 : 0;
                memCards.ForEach(card => card.Color = Players[currentPlayerIndex].MyColor);
            }
        }
    }
}