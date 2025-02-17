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
        public static string OptionNotFound = "Option Not Found.";

        // For testing the input is valid and from one of the current options available
        //public static string CurrentOptionsDisplayed = "";
        public static int[] CurrentOptionsDisplayedIntArray = { };

        public static string[] GeneralOptionsKeyBinds = { "D", "I", "C", "Enter" };
        public static string[] RoomExploreOptionsKeyBinds = { "1", "2", "3", "4" };

        private string currentOptionsConcatenation = OptionNotFound;
        ///
        private string[] GeneralOptionsArray = {
            $"Room Description [{GeneralOptionsKeyBinds[0]}]",
            $"Inventory [{GeneralOptionsKeyBinds[1]}]",
            $"Character Stats [{GeneralOptionsKeyBinds[2]}]",
            $"Use [{GeneralOptionsKeyBinds[3]}]"
        };
        private string[] RoomExploreOptionsArray = {
            $"Try Door [{RoomExploreOptionsKeyBinds[0]}]",
            $"Look On Table [{RoomExploreOptionsKeyBinds[1]}]",
            $"Look Under Table [{RoomExploreOptionsKeyBinds[2]}]",
            $"Try Chest [{RoomExploreOptionsKeyBinds[3]}]" };

        private string[] InventoryOptionsArray = {
            $"Select Item [1-8]",
            $"Equip Item [{GeneralOptionsKeyBinds[3]}]" };


        public Options()
        {
            currentOptionsConcatenation = "  ";
        }

        public string GetGeneralOptions(int[] options)
        {
            //CurrentOptionsDisplayed = "GeneralOptions";
            CurrentOptionsDisplayedIntArray = options;

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
            //CurrentOptionsDisplayed = "RoomExploreOptions";
            CurrentOptionsDisplayedIntArray = options;

            currentOptionsConcatenation = "  ";

            for (int i = 0; i < options.Length; i++)
            {
                Debug.Assert(options[i] < GeneralOptionsArray.Length, "Options to be fetched surpass the length of the specified array.");

                currentOptionsConcatenation = currentOptionsConcatenation + "\n   " + RoomExploreOptionsArray[options[i]];
            }

            return currentOptionsConcatenation;
        }

        public string GetInventoryOptions(int[] options)
        {
            //CurrentOptionsDisplayed = "InventoryOptions";
            CurrentOptionsDisplayedIntArray = options;

            currentOptionsConcatenation = "  ";

            for (int i = 0; i < options.Length; i++)
            {
                Debug.Assert(options[i] < GeneralOptionsArray.Length, "Options to be fetched surpass the length of the specified array.");

                currentOptionsConcatenation = currentOptionsConcatenation + "  " + InventoryOptionsArray[options[i]];
            }

            return currentOptionsConcatenation;
        }
    }
}
