using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Options
    {
        public static string[] GeneralOptionsKeyBinds = { "D", "C", "Tab" };
        public static string[] RoomExploreOptionsKeyBinds = { "D1", "D2", "D3", "D4",    "D", "C", "Tab" };  // D1 etc. is the name of the number keys the system recognises
        public static string[] InventoryOptionsKeyBinds = { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0", "Enter", "Tab" };

        private string currentOptionsConcatenation;
        ///
        private string[] GeneralOptionsArray = {
            "Room Description [D]",
            "Character Stats [C]",
            "Inventory [Tab]" };
        private string[] RoomExploreOptionsArray = {
            "Try Door [1]",
            "Look On Table [2]",
            "Look Under Table [3]",
            "Try Chest [4]" };
        private string[] InventoryOptionsArray = {
            "Select Item [0-9]",
            "Equip Item [Enter]",
            "Close Inventory [Tab]" };


        public Options()
        {
            currentOptionsConcatenation = "  ";
        }

        public string GetGeneralOptions(int[] options)
        {
            currentOptionsConcatenation = "  ";

            for (int i = 0; i < options.Length; i++)
            {
                // Check that the current iteration of the options array is not greater than the length of the array it is using
                try { currentOptionsConcatenation = currentOptionsConcatenation + "  " + GeneralOptionsArray[options[i]]; }
                catch { Debug.WriteLine("Options to be fetched surpass the length of the specified array."); }
            }

            return currentOptionsConcatenation;
        }
        public string GetRoomExploreOptions(int[] options)
        {
            currentOptionsConcatenation = "  ";

            for (int i = 0; i < options.Length; i++)
            {
                Debug.Assert(options[i] < GeneralOptionsArray.Length, "Options to be fetched surpass the length of the specified array.");

                currentOptionsConcatenation = currentOptionsConcatenation + "\n   " + RoomExploreOptionsArray[options[i]];
            }

            return currentOptionsConcatenation;
        }

        public string GetInventoryOptions()
        {
            currentOptionsConcatenation = "   " + (string.Join("   ", InventoryOptionsArray));

            return currentOptionsConcatenation;
        }
    }
}
