using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer.Levels
{
    internal class Level_1_Displays
    {
        private string L1R1_Desc = Room.RoomDescriptionNotFound;
        private string L1R1_OptionDesc = Room.RoomOptionsDescriptionNotFound;
        private string L1R1 = Program.RoomDisplayNotFound;

        private string L1R2 = Program.RoomDisplayNotFound;
        private string L1R3 = Program.RoomDisplayNotFound;

        Environment_Interactables environmentInteractables;

        public Level_1_Displays()
        {
            environmentInteractables = new Environment_Interactables();

            L1R1_Desc = "A small rectangular room surrounds you, furnished with a singular wooden table and a small battered box against the right wall.";
            L1R1 = $@"                                              
  Level 1, The Cell.

║░┌─────────────────────────────────────────────────────┐░║
║░│                                                    ┌|  
║▒│     --────────---                                  │|  
║▒│    |░░░░░░░░░░░░░|                                 {Environment_Interactables.Closed_Door}|  
║▓│    ║░░░░░░░░░░░░░║                                 └|  
║▓│    ║░░░░░░░░░░░░░║                                  │░║
║▓│    |░░░░░░░░░░░░░|                                  │░║
║▒│     ---───────---                                   │▒║
║▒│                          {Environment_Interactables.Closed_Chest_0}                    │▒║
║░│                          {Environment_Interactables.Closed_Chest_1}                    │░║
║░└─────────────────────────────────────────────────────┘░║
												
	";
            L1R2 = $@"
    Level 1, Hallway    

    ║░│                              │░║
    ║░│                              │░║
    ║▒│                              |░║
    ║▒│                             //░║
    ║▒│                             |░░║
    ║░│                             |░░║
    ╗░│                             \\░║
    |┐                               |░║
    |│                               │░║
    |{Environment_Interactables.Open_Door}                               │▒║
    |┘                               │▒║
    ║░│                              │▒║
    ║░│                              │▒║
    ║▒│                              │▒║
    ║▒│                              │▒║
    ║▒│                              │▒║
    ╝▒│                              │▒║
    ║▒│                              │▒║
    ║▒│                              │▒║
    ║▒└────────────────┐             │▒║
    ║▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░│             │▒║
    ║▒┌────────────────┘             │▒║
    ║▒│                              │▒║
    ║▒│                              │░║
    ║░│                              │░║
    ║░│                              │░║

	";
        }

        public string GetRoom(int roomNumber)
        {
            if (roomNumber.Equals(1))
                return L1R1;
            if (roomNumber.Equals(2))
                return L1R2;
            if (roomNumber.Equals(3))
                return L1R3;

            else return Program.RoomDisplayNotFound;
        }

        public string GetDescription()
        {
            return "";
        }
    }
}
