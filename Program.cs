// Filename: Program.cs
using System;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    internal class Program
    {
        /// <summary>
        /// Handles the main flow of the program. If it terminates the program will wait for an input before closing the console.
        /// A commonly used static method CLEAR_CONSOLE() is implemented here and used throughout the program for ease of use.
        /// 
        /// Details:
        /// - Writes the welcome title
        /// - Initialises a new game object and waits for the player to start the game
        /// - Allows the player to input their name and sets it to the name attribute in the Player class (as only one player will ever be initialised per game)
        /// - Starts the game
        /// </summary>
        public static string VersionNumber = "v0.1";

        public static int NumOfLevels = 1;  // Change locally based on how many levels are implemented

        public static string NameTemp = "";

        static void Main(string[] args)
        {
            Welcome welcome = new Welcome();
            string title = welcome.GetWelcomeTitle();

            Game game = new Game();

            Console.WriteLine(title);
            Console.WriteLine("\nPress [Space] to play.\n");

            Game.InputHandler.WaitOnKey("Spacebar");

            EnterName();

            void EnterName()
            {
                CLEAR_CONSOLE();

                Console.WriteLine("Input your name, press [Enter] to confirm.\n");

                Game.InputHandler.SetName(2, 18);  // int min char, int max char

                Console.WriteLine($"Are you sure you want to set your name as {NameTemp}?\n\n > Yes [1]\n > No [2]");

                int option = Room.PlayerChoice(new string[] { "D1", "D2" });

                if (option == 0)  // Yes
                {
                    return;
                }
                else if (option == 1)  // No
                {
                    EnterName();
                }
            }

            if (NameTemp.EndsWith("s") || NameTemp.EndsWith("z"))
            {
                Player.NamePlural = $"{NameTemp}'";
            }
            else
            {
                Player.NamePlural = $"{NameTemp}'s";
            }

            game.Start();  // Start the game

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Clears the current console screen and the scrollback buffer (characters that may be out of view but still there when you scroll up)
        public static void CLEAR_CONSOLE()
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");
        }
    }
}
