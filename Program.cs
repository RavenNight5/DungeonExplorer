using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer.Dialogue;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    internal class Program
    {
        public static int NumOfLevels = 1;  // Change locally based on how many levels are implemented

        public static string RoomDisplayNotFound = "RD Not Found.";

        static void Main(string[] args)
        {
            Welcome welcome = new Welcome();
            string title = welcome.GetWelcomeTitle();

            Input input = new Input();
            General_Info generalInfo = new General_Info();

            Console.WriteLine(title);


            Console.WriteLine("\nPress [Space] to play.\n");

            input.WaitOnKey("Spacebar");

            Console.Clear();

            Console.WriteLine("\nInput your name, press [Enter] to confirm.\n");

            input.SetName(3, 18);  // int min char, int max char

            Game game = new Game();  // Start game

            //DescriptionBox BasicInfo = new DescriptionBox("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", 54);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
