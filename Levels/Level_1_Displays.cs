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
        //private List<string> Rooms_Initial = new List<string>();
        //private List<string> Rooms_North = new List<string>();

        public List<string[]> R1_Interactables = new List<string[]> { Environment_Interactables.Closed_DoorVertiacl, Environment_Interactables.Closed_Chest };

        private List<string> Rooms1_List = new List<string>();
        private List<string> Room1_Descriptions_List = new List<string>();

        private int lastRoomFetched = 0;

        private string R1_Desc = "A small rectangular room surrounds you, furnished with a singular wooden table and a small battered box against the right wall. You don't notice anything out of the ordinary.";
        private string R2_Desc = "This hallway serves as a primary passageway for staff navigating the dungeon. While cells line the walls, they hold fewer occupants since this area acts as an overflow quater. You look opposite you, a bit further up there seems to be a bump in the wall: perhaps you should check it out.";
        private string R3_Desc = "The hallway continues with empty cells appearing on your left as you progress. Nothing much to clean out here.";

        public Level_1_Displays()
        {
            
			RefreshRoomDisplays();

            Room1_Descriptions_List.Add(R1_Desc);
            Room1_Descriptions_List.Add(R2_Desc);
            Room1_Descriptions_List.Add(R3_Desc);

			//R1.Replace("{Environment_Interactables.Closed_Chest[0]}", Environment_Interactables.Open_Chest[0]);
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

	║░│░░ ░░░ ░░░ ░░░ ░░░ ░░░ ░░░ ░░│░║
	║░│ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ │░║
	║▒│  ░   ░   ░   ░   ░   ░   ░  |░║
	║▒│                        	   //░│
	║▒│                            /░░|
	║░│                            |░░|
	╗░│                            \\░|
	|┐                           	|░│
	|│                           	│░║
	|{R1_Interactables[0][0]}                           	│▒║
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
	║░│        ░░               	│░║
	";

            string R3 = $@"
 	Level 1, North Hallway    

	╗░│░░ ░░░ ░░░ ░░░ ░░░ ░░░ ░░░ ░░│▒║
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
	";

            string R6 = $@"
	Level 1, South Hallway

                        ║░│      ░░  ░                  │▒║
                        ║▒│     ░░ ░░░░░                │▒║
                        ║▒│  ░   ░░░ ░   ░              │▒║
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

            try
            {
                string roomDisplay = Rooms1_List[roomNumber - 1];

                return roomDisplay;
            }
            catch (Exception e)
            {
                Console.WriteLine("Room does not exist. Exception caught: " + e.Message);

                return "Room does not exist.";
            }
        }

        public string GetDescription()
        {
            return Room1_Descriptions_List[lastRoomFetched];
        }
    }
}


