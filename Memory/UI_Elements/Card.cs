﻿using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Card : UI_Element
    {
        public static int NumberOfOpenCards = 0;

        private bool drawImage;
        private Texture2D image;
        private Vector2 position;
        public int CardID { get; private set; }
        public bool IsFound { get; set; }

        public Card(int x, int y, int w, int h, int cardId, string fileName = "") : base(x, y, w, h)
        {
            position = new Vector2(rect.x, y);

            if (fileName != "")
            {
                image = LoadTexture(fileName);

                //position.x += (w - image.width) / 2;
                //position.y += (h - image.height) / 2;
            }

            this.CardID = cardId;

            drawImage = false;
            IsFound = false;
        }

        public override void DrawMe()
        {
            if (!IsFound)
            {
                base.DrawMe();
            }

            if (drawImage)
            {
                DrawTextureEx(image, position, 0, 0.1f, Color.WHITE);
            }
        }

        public override bool CheckIfClicked()
        {
            bool isClicked = false;

            if (!IsFound)
            {
                isClicked = base.CheckIfClicked();

                if (isClicked)//&& !drawImage)
                {
                    drawImage = true;
                    NumberOfOpenCards++;
                }
            }

            return isClicked;
        }

        public void ResetMe()
        {
            if (!IsFound)
            {
                drawImage = false;
            }
        }
    }
}
