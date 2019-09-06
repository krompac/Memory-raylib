using System;
using System.Linq;
using System.IO;
using NAudio.Wave;
using static Raylib.Raylib;

namespace Memory
{
    class Program
    {
        static string backslash = ((char)92).ToString();
        public static string ButtonSound { get; private set; }
        public static string ThemeSound { get; private set; }

        static void Main(string[] args)
        {
            InitWindow(640, 480, "Memory");
            ReadSoundPaths();

            SetWindowIcon(LoadImage(GetIconPath()));
            #region drawLetterTest
            //var imageText = ImageText("k", 10, Color.WHITE);
            //var rect = new Rectangle(100, 100, 5, 10);

            //var nekaj = LoadTextureFromImage(imageText);

            //while (!WindowShouldClose())
            //{
            //BeginDrawing();

            //ClearBackground(Color.BLACK);
            //    DrawRectangleRec(rect, Color.ORANGE);
            //    DrawText("k", 100, 100, 10, Color.WHITE);


            //EndDrawing();

            //    System.Threading.Thread.Sleep(10);
            //}
            #endregion

            var gameManager = new GameManager();
            gameManager.GameLoop();


            CloseWindow();

            Environment.Exit(1);
        }

        private static void ReadSoundPaths()
        {
            var sounds = Directory.GetFiles(Program.PathToSounds());
            ButtonSound = sounds[0];
            ThemeSound = sounds[1];
        }
        private static string GetIconPath()
        {
            var picturesPath = PathToPictures() + backslash + "icon";

            return Directory.GetFiles(picturesPath).FirstOrDefault();
        }

        public static string GetCardsPath()
        {
            return PathToPictures() + backslash + "willdabeast";
        }

        private static string PathToAssets()
        {
            var path = Directory.GetCurrentDirectory().Split(backslash[0]);
            Array.Resize(ref path, path.Length - 2);

            var newPath = string.Join(backslash.ToString(), path) + backslash + "Assets";

            return newPath;
        }

        public static string PathToPictures()
        {
            return PathToAssets() + backslash + "Pictures";
        }

        public static string PathToSounds()
        {
            return PathToAssets() + backslash + "Sounds";
        }
    }
}
