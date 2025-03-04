using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer.Levels
{
    internal class Level_1_Displays
    {
        //private List<string> Rooms_Initial = new List<string>();
        //private List<string> Rooms_North = new List<string>();

        public List<string[]> R1_Interactables = new List<string[]> { Environment_Interactables.Closed_DoorVertical, Environment_Interactables.Closed_Chest };

        private List<string> Rooms1_List = new List<string>();
        private List<string> Descriptions_List = new List<string>();

        private int lastRoomFetched = 0;

        private string R1_Desc = "A small rectangular room surrounds you, furnished with a singular wooden table and a small battered box against the right wall. You don't notice anything out of the ordinary.";
        private string R2_Desc = "This hallway serves as a primary passageway for staff navigating the dungeon. While cells line the walls, they hold fewer occupants: this area is usuially used as an overflow quater. You look opposite you, a bit further up there seems to be a bump in the wall... Perhaps you should check it out.";
        private string R3_Desc = "The hallway continues with empty cells appearing on your left as you progress. Nothing much to clean out here.";
        //private string R4_Desc = "You walk south and find a puddle to clean.";

        private string[] R1_ExploreOptions = {
            "Use Door [1]",
            "Open Chest [2]",
            "Look On Table [3]",
            "Look Under Table [4]" };
        private string[] R2_ExploreOptions = {
            "Use Cell Door [1]",
            "Investigate Wall [2]",
            "Go North [3]",
            "Go South [4]",
            "Stand Around [5]" };
        private string[] R3_ExploreOptions = {
            "Try Doors [1]",
            "Go North [2]",
            "Go South [3]" };

        public List<string[]> Room_ExploreOptions = new List<string[]>();

        public Level_1_Displays()
        {
            
			RefreshRoomDisplays();

            Descriptions_List.Add(R1_Desc);
            Descriptions_List.Add(R2_Desc);
            Descriptions_List.Add(R3_Desc);

            Room_ExploreOptions.Add(R1_ExploreOptions);
            Room_ExploreOptions.Add(R2_ExploreOptions);
            Room_ExploreOptions.Add(R3_ExploreOptions);
        }

		private void RefreshRoomDisplays()
		{
            // Remove then add the rooms back to the list - meaning if an Environment Interactable has changed it will show
            
			Rooms1_List.Clear();

            string R1 = $@"
  Level 1, The Cell.

║░┌─────────────────────────────────────────────────────┐░║
║░│                                                 	┌|  
║▒│ 	 ---───────---                              	│|  
║▒│     |░░░░░░░░░░░░░|                             	{R1_Interactables[0][0]}|  
║▓│ 	║░░░░░░░░░░░░░║                             	└|  
║▓│ 	║░░░░░░░░░░░░░║                              	│░║
║▓│ 	|░░░░░░░░░░░░░|                              	│░║
║▒│ 	 ---───────---                               	│▒║
║▒│                      	{R1_Interactables[1][0]}                	│▒║
║░│                      	{R1_Interactables[1][1]}                	│░║
║░└─────────────────────────────────────────────────────┘░║
	";

            string R2 = $@"
 	Level 1, Hallway    
		
	  │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│
	║░│ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ │░║
	║░│  ░   ░   ░   ░   ░   ░   ░  │░║
	║▒│                             |░║
	║▒│                            //░│
	║▒│                            /░░|
	║░│                            |░░|
	╗░│                            \\░|
	|┐                           	|░│
	|│                           	│░║
	|╧                           	│▒║
	|┘                           	│▒║
	║░│                          	│▒║
	║░│                          	│▒║
	║▒│                          	│▒║
	║▒│                          	│▒║
	║▒│                          	│▒║
	╝▒│                          	│▒║
	║▒│                          	│▒║
	║▒│                          	│▒║
	║▒└────────────────┐         	│▒║
	║▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░│         	│▒║
	║▒┌────────────────┘         	│▒║
	║▒│                          	│▒║
	║▒│                          	│░║
	║░│       ░                 	│░║
	║░│        ▒░               	│░║
	  │                             │
";

            string R3 = $@"
 	Level 1, North Hallway    

      │                             │
	╗░│░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│▒║
	|┐░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ │▒║
	|│   ░   ░   ░   ░   ░   ░   ░  │▒║
	|{R1_Interactables[0][0]}                           	│▒║
	|┘                           	│▒║
	║░│                          	│▒║
	║░│                          	│▒║
	║▒│                          	│▒║
	║▒│                          	│▒║
	║▒│                          	│▒║
	╝▒│                          	│▒║
	║░│                          	│▒║
	╗░│                          	│▒║
	|┐                           	│▒║
	|│                           	│▒║
	|┼                           	│▒║
	|┘                           	│▒║
	║░│                          	│▒║
	║░│                          	│▒║
	║▒│                          	│▒║
	║▒│                          	│▒║
	║▒│                          	│▒║
	╝▒│                          	│▒║
	║░│                          	│▒║
	╗░│                          	│▒║
	|┐                           	│▒║
	|│                           	│▒║
	|┼                           	│▒║
	|┘                           	│▒║
	║░│                          	│▒║
	║░│                          	│▒║
	║▒│                          	│▒║
	║▒│                          	│▒║
	║▒│                          	│▒║
	╝▒│                          	│▒║
	║░│                          	│▒║
      │                             │
	";

            string R6 = $@"
	Level 1, South Hallway

                        ║░│      ░░  ░                  │▒║
                        ║▒│     ░░ ░▒▒░░                │▒║
                        ║▒│  ░   ░▒░ ░   ░              │▒║
                        ║▒│       ░   ░                 │▒║
                        ║▒│         ░                   │▒║
   ═════════════════════╝▒│                             │▒╚═══════════════════════
   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒│                             │▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
   ───────────────────────┘                             └─────────────────────────






   ▒ ▒ ▒ ▒ ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒   ▒ ▒ ▒ ▒
    ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒
   ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒
   ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒  ▒▒
   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
   ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
   ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
";

            Rooms1_List.Add(R1);
            Rooms1_List.Add(R2);
            Rooms1_List.Add(R3);

        }

        public string GetRoom(int roomNumber)
        {

			RefreshRoomDisplays();

            lastRoomFetched = roomNumber - 1;

			Debug.Assert(lastRoomFetched < Descriptions_List.Count(), "Room to be fetched does not exist.");

			Room.currentRoomDescription = Descriptions_List[lastRoomFetched];

            try
            {
                string roomDisplay = Rooms1_List[roomNumber - 1];

                return roomDisplay;
            }
            catch (Exception e)
            {
                Console.WriteLine("Room does not exist. Exception caught: " + e.Message);

                return "Room to be fetched does not exist.";
            }
        }

    }
}


