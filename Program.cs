// Filename: Program.cs
using System;
using System.Diagnostics;
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
        /// - Initialises a new tests object if the player wants to use the Testing Menu (then handled by the Tests class)
        /// - Allows the player to input their name and sets it to the name attribute in the Player class (as only one player will ever be initialised per game)
        /// - Starts the game
        /// - Handles the game over screen if the player loses a battle
        /// </summary>
        public static string VersionNumber = "v0.2";

        public static int NumOfLevels = 1;  // Change locally based on how many levels are implemented

        public static string NameTemp = "";
        public static string TempPlural = "";

        public static Game game { get; set; }
        public static Tests tests { get; private set; }

        static void Main(string[] args)
        {
            Welcome welcome = new Welcome();
            string title = welcome.GetWelcomeTitle();

            game = new Game();

            Console.WriteLine(title);
            Console.WriteLine("\n Press [Space] to play.\n ---\n Press [T] for the Testing Menu.\n\n");

            string playerInput = Input.WaitOnKey("Spacebar", "T");

            if (playerInput == "Spacebar")
            {
                EnterName();

                game.Start();  // Start the game
            }
            else
            {
                EnterName();

                tests = new Tests();
                
                game = null;  // Reset the game object as a new one will be instantiated in Tests

                tests.TestingMenu();  // Brings up the testing menu
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
                    if (NameTemp.EndsWith("s") || NameTemp.EndsWith("z"))
                    {
                        TempPlural = $"'";
                    }
                    else
                    {
                        TempPlural = $"'s";
                    }

                    return;
                }
                else if (option == 1)  // No
                {
                    EnterName();
                }
            }

            Console.WriteLine("\nPress [any key] to exit application...");
            Console.ReadKey();
        }


        // If the player has lost the battle
        public static void GameOver()
        {
            CLEAR_CONSOLE();

            Console.WriteLine(@"                                    
 ▄▀  ▄▀▄ █▄ ▄█ ██▀   ▄▀▄ █ █ ██▀ █▀▄
 ▀▄▓ ▓▀▓ ▒ ▀ ▒ ▒▄▄   ▒▄▀ ▀▄▒ ▓▄▄ ▓▀▄


 Restart Application [Space]
 ---
 Quit Application [Q]
");

            string playerInput = Input.WaitOnKey("Spacebar", "Q");
            
            if (playerInput == "Spacebar")
            {
                Process.Start(AppDomain.CurrentDomain.FriendlyName);

                Environment.Exit(0);
            }
            else
            {
                Environment.Exit(0);
            }
        }

        // Clears the current console screen and the scrollback buffer (characters that may be out of view but still there when you scroll up)
        public static void CLEAR_CONSOLE()
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");
        }

    }
}