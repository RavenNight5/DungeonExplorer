using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string Open_Door = "┴";
        public static string Closed_Door = "┼";

        public Environment_Interactables() { }

        public void GetAllLevelInteractables()
        {
            // Use for testing they are not Room.InteractableDisplayNotFound
        }
    }
}
