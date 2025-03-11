using System;

namespace DungeonExplorer.Text_Displays
{
    internal class Environment_Interactables
    {
        
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
