using Raylib;
using static Raylib.Raylib;
using System.Collections.Generic;

namespace Memory
{
    class Timer : IDrawable
    {
        public delegate void Action();
        private Action nextTurn;

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

            var reset = new Text(221, 12, 25, "Reset time");
            var turn = new Text(230, 12, 25, "Turn time");

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
                    resetTimer = 33;
                    break;
                case Difficulty.Medium:
                    turnTimer = resetTimer = 33;
                    break;
                case Difficulty.Hard:
                    turnTimer = resetTimer = 22;
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
            outerRect.x += value;
            progresTempXPos = outerRect.x + 5;
            progres.x = progresTempXPos;

            foreach (var item in timerText.Values)
            {
                item.UpdateXpos = value;
            }
        }

        public void DrawMe()
        {
            DrawRectangleRec(outerRect, OuterRectColor);

            if (GameState != GameState.NotOpened)
            {
                timerText[GameState].DrawMe();
                DrawRectangleRec(progres, Color.WHITE);
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

                        if (progres.width < widthToGetTo)
                        {
                            progres.width++;
                        }
                        else
                        {
                            nextTurn.Invoke();
                        }
                    }

                    break;
                case GameState.Reset:
                    if (time - resetTimer >= prevTime)
                    {
                        prevTime = time;

                        if (progres.x < progresTempXPos + widthToGetTo)
                        {
                            progres.x++;
                            progres.width--;
                        }
                        else
                        {
                            progres.x = progresTempXPos;
                            nextTurn.Invoke();
                        }
                    }
                    break;
            }
        }

        public void UpdateProgresBar()
        {
            progres.width = (GameState == GameState.Reset) ? 90 : 0;
            
            progres.x = progresTempXPos;
            prevTime = 0;
        }
    }
}
