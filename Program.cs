using System;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    internal class Program
    {
        public static int NumOfLevels = 1;  // Change locally based on how many levels are implemented

        static void Main(string[] args)
        {
            Welcome welcome = new Welcome();
            string title = welcome.GetWelcomeTitle();

            Game game = new Game();

            Console.WriteLine(title);
            Console.WriteLine("\nPress [Space] to play.\n");

            Game.InputHandler.WaitOnKey("Spacebar");

            CLEAR_CONSOLE();

            Console.WriteLine("\nInput your name, press [Enter] to confirm.\n");

            Game.InputHandler.SetName(3, 18);  // int min char, int max char
            
            if (Player.Name.EndsWith("s") || Player.Name.EndsWith("z"))
            {
                Player.NamePlural = $"{Player.Name}'";
            }
            else
            {
                Player.NamePlural = $"{Player.Name}'s";
            }

            game.Start();  // Start the game

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        public static void CLEAR_CONSOLE()
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");  // Clears the current console screen and the scrollback buffer (characters that may be out of view but still there when you scroll up)
        }
    }
}
