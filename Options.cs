using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    internal class Options
    {
        public static string[] GeneralOptionsKeyBinds = { "D", "C", "Tab" };
        public static List<string> RoomExploreOptionsKeyBinds = new List<string>();
        public static string[] InventoryOptionsKeyBinds = { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0", "Enter", "Tab" };  // D1 etc. is the name of the number keys the system recognises

        private string currentOptionsConcatenation;

        private string[] GeneralOptionsArray = {
            "Room Description [D]",
            "Character Stats [C]",
            "Inventory [Tab]" };
        private string[] InventoryOptionsArray = {
            "Select Item [0-9]",
            "Equip/Use Item [Enter]", 
            "Close Inventory [Tab]" };


        public Options()
        {
            currentOptionsConcatenation = "  ";
        }

        public string GetGeneralOptions()
        {
            currentOptionsConcatenation = "  ";

            for (int i = 0; i < GeneralOptionsArray.Length; i++)
            {
                currentOptionsConcatenation = currentOptionsConcatenation + "  " + GeneralOptionsArray[i];
            }

            return currentOptionsConcatenation;
        }
        public string GetRoomExploreOptions(string[] roomExploreOptions)
        {
            currentOptionsConcatenation = "   ";

            for (int i = 0; i < roomExploreOptions.Length; i++)
            {
                currentOptionsConcatenation = currentOptionsConcatenation + "\n   > " + roomExploreOptions[i];
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
