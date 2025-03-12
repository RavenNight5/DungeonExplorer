// Filename: Options.cs
using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    internal class Options
    {
        /// <summary>
        /// Returns a string concatenated from the various options (based on the method called) and holds the keybinds for the main single-key options the player can take.
        /// </summary>
        private string _currentOptionsConcatenation;
        
        public static string[] GeneralOptionsKeyBinds = { "D", "C", "Tab" };
        public static string[] InventoryOptionsKeyBinds = { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0", "Enter", "Tab" };  // D1 etc. is the name of the number keys the system recognises
        
        public static List<string> RoomExploreOptionsKeyBinds = new List<string>();

        private readonly string[] _generalOptionsArray = {
            "Room Description [D]",
            "Character Stats [C]",
            "Inventory [Tab]" };
        private readonly string[] _inventoryOptionsArray = {
            "Select Item [0-9]",
            "Equip/Use Item [Enter]", 
            "Close Inventory [Tab]" };


        public Options()
        {
            _currentOptionsConcatenation = "  ";
        }

        public string GetGeneralOptions()
        {
            _currentOptionsConcatenation = "  ";

            for (int i = 0; i < _generalOptionsArray.Length; i++)
            {
                _currentOptionsConcatenation = _currentOptionsConcatenation + "  " + _generalOptionsArray[i];
            }

            return _currentOptionsConcatenation;
        }

        public string GetRoomExploreOptions(string[] roomExploreOptions)
        {
            _currentOptionsConcatenation = "   ";

            for (int i = 0; i < roomExploreOptions.Length; i++)
            {
                _currentOptionsConcatenation = _currentOptionsConcatenation + "\n   > " + roomExploreOptions[i];
            }

            return _currentOptionsConcatenation;
        }

        public string GetInventoryOptions()
        {
            _currentOptionsConcatenation = "   " + (string.Join("   ", _inventoryOptionsArray));

            return _currentOptionsConcatenation;
        }
    }
}
