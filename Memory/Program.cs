using System;
using System.Linq;
using System.IO;
using static Raylib_cs.Raylib;

namespace Memory
{
    class Program
    {
        private static readonly int windowWidth = 640;
        private static readonly int windowHeight = 480;

        static void Main(string[] args)
        {
            InitWindow(windowWidth, windowHeight, "Memory");
            SetWindowMinSize(windowWidth, windowHeight);
            InitAudioDevice();

            SetWindowIcon(LoadImage(GetIconPath()));

            var gameManager = new GameManager(windowWidth, windowHeight);
            gameManager.GameLoop();

            CloseAudioDevice();
            CloseWindow();

            Environment.Exit(1);
        }

        private static string GetIconPath()
        {
            var picturesPath = Path.Combine(PathToPictures(), "icon");

            return Directory.GetFiles(picturesPath).FirstOrDefault();
        }

        public static string GetCheckMarkPath()
        {
            return Directory.GetFiles(Path.Combine(PathToPictures(), "checkmark")).FirstOrDefault();
        }

        public static string GetCardsPath()
        {
            return Path.Combine(PathToPictures(), "willdabeast");
        }

        public static string GetTitlePath()
        {
            return Directory.GetFiles(Path.Combine(PathToPictures(), "title")).FirstOrDefault();
        }

        private static string PathToAssets()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Assets");
        }

        public static string PathToPictures()
        {
            return Path.Combine(PathToAssets(), "Pictures");
        }

        public static string PathToSounds()
        {
            return Path.Combine(PathToAssets(), "Sounds");
        }
    }
}