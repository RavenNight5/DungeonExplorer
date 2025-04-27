// Filename: Program.cs
using System;
using DungeonExplorer.Testing;
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
        public static string VersionNumber = "v0.2";

        public static int NumOfLevels = 1;  // Change locally based on how many levels are implemented

        public static string NameTemp = "";

        public static Game game { get; private set; }

        static void Main(string[] args)
        {
            Welcome welcome = new Welcome();
            string title = welcome.GetWelcomeTitle();

            game = new Game();

            Console.WriteLine(title);
            Console.WriteLine("\nPress [Space] to play.\nPress [T] for the Testing Menu.\n\n");

            string playerInput = Game.InputHandler.WaitOnKey("Spacebar", "T");

            if (playerInput == "Spacebar")
            {
                EnterName();

                game.Start();  // Start the game
            }
            else
            {
                EnterName();

                Tests testingMenu = new Tests();

                testingMenu.TestingMenu();  // Brings up the testing menu
            }

            void EnterName()
            {
                CLEAR_CONSOLE();

                Console.WriteLine("Input your name, press [Enter] to confirm.\n");

                Game.InputHandler.SetName(2, 18);  // int min char, int max char

                Console.WriteLine($"Are you sure you want to set your name as {NameTemp}?\n\n > Yes [1]\n > No [2]\n");

                int option = Room.PlayerChoice(new string[] { "D1", "D2" });

                if (option == 0)  // Yes
                {
                    return;
                }
                else if (option == 1)  // No
                {
                    EnterName();
                }

                if (NameTemp.EndsWith("s") || NameTemp.EndsWith("z"))
                {
                    Player.NamePlural = $"{NameTemp}'";
                }
                else
                {
                    Player.NamePlural = $"{NameTemp}'s";
                }
            }

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
