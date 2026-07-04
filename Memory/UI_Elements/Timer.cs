using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Collections.Generic;

namespace Memory
{
    class Timer : IDrawable
    {
        public delegate void Action();
        private readonly Action nextTurn;

        public GameState GameState { get; set; }

        private Rectangle progres;
        private float progresTempXPos;
        private readonly float widthToGetTo;

        private Rectangle outerRect;
        public Color OuterRectColor { private get; set; }

        private int resetTimer;
        private int turnTimer;

        private int prevTime;

        private readonly Dictionary<GameState, Text> timerText;

        public Timer(Action nextTurn, Difficulty difficulty)
        {
            GameState = GameState.Opened;
            this.nextTurn = nextTurn;

            var posX = 360;
            var posY = 10;
            var width = 100;
            var heigth = 30;
            progresTempXPos = posX + 5;
            widthToGetTo = 90;

            outerRect = new Rectangle(posX, posY, width, heigth);
            progres = new Rectangle(progresTempXPos, posY + 5, 0, heigth - 10);

            var reset = new Text(221, 12, 25, "Reset time", Color.White);
            var turn = new Text(230, 12, 25, "Turn time", Color.White);

            timerText = new Dictionary<GameState, Text>
            {
                [GameState.Reset] = reset,
                [GameState.Opened] = turn
            };

            prevTime = 0;

            InitTimersByDifficulty(difficulty);
        }

        private void InitTimersByDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    turnTimer = 55;
                    resetTimer = 25;
                    break;
                case Difficulty.Medium:
                    turnTimer = 33;
                    resetTimer = 20;
                    break;
                case Difficulty.Hard:
                    turnTimer = 22;
                    resetTimer = 15;
                    break;
            }

            /*
             * 22 = 2.7 sec 
             * 33 = 3.6 sec 
             * 55 = 5.4 sec 
            */
        }

        /// <summary>
        /// Add value to the x position of timer
        /// </summary>
        /// <param name="value"></param>
        public void UpdatePosition(int value)
        {
            outerRect.X += value;
            progresTempXPos = outerRect.X + 5;
            progres.X = progresTempXPos;

            foreach (var item in timerText.Values)
            {
                item.UpdateXpos = value;
            }
        }

        public void DrawMe()
        {
            if (GameState != GameState.NotOpened)
            {
                DrawRectangleRec(outerRect, OuterRectColor);
                timerText[GameState].DrawMe();
                DrawRectangleRec(progres, Color.White);
            }
        }

        public void UpdateTimer(int time)
        {
            switch (GameState)
            {
                case GameState.NotOpened:
                    if (prevTime != 0)
                    {
                        prevTime = 0;
                    }
                    break;
                case GameState.Opened:
                    if (time - turnTimer >= prevTime)
                    {
                        prevTime = time;

                        if (progres.Width < widthToGetTo)
                        {
                            progres.Width++;
                        }
                        else
                        {
                            progres.Width = 0;
                            nextTurn.Invoke();
                        }
                    }

                    break;
                case GameState.Reset:
                    if (time - resetTimer >= prevTime)
                    {
                        prevTime = time;

                        if (progres.X < progresTempXPos + widthToGetTo)
                        {
                            progres.X++;
                            progres.Width--;
                        }
                        else
                        {
                            progres.X = progresTempXPos;
                            nextTurn.Invoke();
                        }
                    }
                    break;
            }
        }

        public void UpdateProgresBar()
        {
            progres.Width = (GameState == GameState.Reset) ? 90 : 0;

            progres.X = progresTempXPos;
            prevTime = 0;
        }
    }
}
