using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer.Text_Displays
{
    internal class Environment_Interactables
    {
        public static string Open_Chest_0 = Room.InteractableDisplayNotFound;
        public static string Open_Chest_1 = Room.InteractableDisplayNotFound;
        public static string Closed_Chest_0 = Room.InteractableDisplayNotFound;
        public static string Closed_Chest_1 = Room.InteractableDisplayNotFound;

        public static string Open_Door = Room.InteractableDisplayNotFound;
        public static string Closed_Door = Room.InteractableDisplayNotFound;

        public Environment_Interactables()
        {
            Open_Chest_0 = @"/--╧--\";
            Open_Chest_1 = @"|░   ░|";
            Closed_Chest_0 = @"/--╦--\";
            Closed_Chest_1 = @"|░░░░░|";

            Open_Door = "┴";
            Closed_Door = "┼";
        }

        public void GetAllLevelInteractables()
        {
            // Use for testing they are not Room.InteractableDisplayNotFound
        }
    }
}
