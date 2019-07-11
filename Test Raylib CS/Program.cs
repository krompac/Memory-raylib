using Raylib;
using static Raylib.Raylib;

namespace Memory
{
    class Program
    {
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
    }
}
