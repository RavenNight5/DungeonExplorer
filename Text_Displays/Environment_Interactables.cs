// Filename: Environment_Interactables.cs
using System;

namespace DungeonExplorer.Text_Displays
{
    internal class Environment_Interactables
    {
        /// <summary>
        /// Holds the interactable objects that can be found in the room displays.
        /// </summary>
        
        public static string[] Open_Chest = {
        @"/--╧--\",
        "|░   ░|"
        };
        public static string[] Closed_Chest = {
        @"/--╦--\",
        "|░░░░░|"
        };

        public static string[] Puddle_1 = {
        "░░  ░",
        "░░ ░▒▒░░",
        "░   ░▒░ ░   ░",
        "░   ░",
        "░",
        "▒░"
        };
        public static string[] Puddle_1_Clean = {
        "    ░",
        "        ",
        "░            ",
        "     ",
        " ",
        "  "
        };

        public static string[] Open_DoorVertical = { "╧" };
        public static string[] Closed_DoorVertical = { "┼" };

    }
}
