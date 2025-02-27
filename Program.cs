using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DungeonExplorer.Dialogue;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{

    //////////////////////////////////////////////////////////////////////////Example of summary
    // Summary:
    //     Concatenates the members of a constructed System.Collections.Generic.IEnumerable`1
    //     collection of type System.String, using the specified separator between each
    //     member.
    //
    // Parameters:
    //   separator:
    //     The string to use as a separator.separator is included in the returned string
    //     only if values has more than one element.
    //
    //   values:
    //     A collection that contains the strings to concatenate.
    //
    // Returns:
    //     A string that consists of the members of values delimited by the separator string.
    //     If values has no members, the method returns System.String.Empty.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     values is null.
    internal class Program
    {
        public static int NumOfLevels = 1;  // Change locally based on how many levels are implemented

        public static string RoomDisplayNotFound = "RD Not Found.";

        static void Main(string[] args)
        {
            Welcome welcome = new Welcome();
            string title = welcome.GetWelcomeTitle();

            Game game = new Game();

            Console.WriteLine(title);

            Console.WriteLine("\nPress [Space] to play.\n");

            Game.InputHandler.WaitOnKey("Spacebar");

            Console.Clear();

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

            //Description_Box BasicInfo = new Description_Box("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 54);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
