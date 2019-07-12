﻿using Raylib; 
using static Raylib.Raylib;
using System.Linq;

namespace Memory
{
    class Program
    {
        static string backslash = ((char)92).ToString();

        static void Main(string[] args)
        {
            InitWindow(640, 480, "Memory");
            SetWindowIcon(LoadImage("icon.png"));

            #region drawLetterTest
            //var imageText = ImageText("k", 10, Color.WHITE);
            //var rect = new Rectangle(100, 100, 5, 10);

            //var nekaj = LoadTextureFromImage(imageText);

            //while (!WindowShouldClose())
            //{
            //    BeginDrawing();

            //    ClearBackground(Color.BLACK);
            //    DrawRectangleRec(rect, Color.ORANGE);
            //    DrawText("k", 100, 100, 10, Color.WHITE);

            //    EndDrawing();

            //    System.Threading.Thread.Sleep(10);
            //}
            #endregion

            var gameManager = new GameManager();
            gameManager.GameLoop();

            CloseWindow();
            System.Environment.Exit(1);
        }

        static string GetIconPath()
        {
            var picturesPath = PathToPictures() + backslash + "icon";

            return System.IO.Directory.GetFiles(picturesPath).FirstOrDefault();
        }

        public static string GetCardsPath()
        {
            return PathToPictures() + backslash + "willdabeast";
        }

        public static string PathToPictures()
        {
            var path = System.IO.Directory.GetCurrentDirectory().Split(backslash[0]);
            System.Array.Resize(ref path, path.Length - 2);

            var newPath = string.Join(backslash.ToString(), path) + backslash + "Assets" + backslash + "Pictures";

            return newPath;
        }
    }
}
