using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer.Levels
{
    internal class Level_1_Displays
    {
        //private List<string> Rooms_Initial = new List<string>();
        //private List<string> Rooms_North = new List<string>();

        public List<string[]> R1_Interactables = new List<string[]> { 
            Environment_Interactables.Closed_DoorVertical, 
            Environment_Interactables.Closed_Chest, 
            Environment_Interactables.Puddle_1 };

        private List<string> Rooms1_List = new List<string>();
        private List<string> Descriptions_List = new List<string>();

        private int lastRoomFetched = 0;

        private readonly string r1_Desc = "A small rectangular room surrounds you, furnished with a singular wooden table and a small battered box against the right wall. You don't notice anything out of the ordinary.";
        private readonly string r2_Desc = "This hallway serves as a primary passageway for staff navigating the dungeon. While cells line the walls, they hold fewer occupants: this area is usuially used as an overflow quater. You look opposite you, a bit further up there seems to be a bump in the wall... Perhaps you should check it out.";
        private readonly string r3_Desc = "The hallway continues with empty cells appearing on your left as you progress. Nothing much to clean out here.";
        private readonly string r4_Desc = "";
        private readonly string r5_Desc = "";
        private readonly string r6_Desc = "";
        private readonly string r7_Desc = "The narrower hallway opens out into a chamber. The lights are out, making the room feel deceptively larger than usual. Besides the unlit room you find a puddle behind the partition in the hallway: there is usually some water seeping from the roof in this spot...";

        private readonly string[] r1_ExploreOptions = {
            "Use Door [1]",
            "Open Chest [2]",
            "Look On Table [3]",
            "Look Under Table [4]" };
        private readonly string[] r2_ExploreOptions = {
            "Use Cell Door [1]",
            "Investigate Wall [2]",
            "Go North [3]",
            "Go South [4]",
            "Stand Around [5]" };
        private readonly string[] r3_ExploreOptions = {
            "Try Doors [1]",
            "Go North [2]",
            "Go South [3]" };
        private readonly string[] r4_ExploreOptions = {
            "xxx [1]",
            "xxx [2]",
            "xxx [3]" };
        private readonly string[] r5_ExploreOptions = {
            "xxx [1]",
            "xxx [2]",
            "xxx [3]" };
        private readonly string[] r6_ExploreOptions = {
            "xxx [1]",
            "xxx [2]",
            "xxx [3]" };
        private readonly string[] r7_ExploreOptions = {
            "Go North [1]",
            "Inspect Puddle [2]",
            "Go Into the Chamber [3]" };

        public static List<string[]> L1Room_ExploreOptions = new List<string[]>();

        public Level_1_Displays()
        {
            
			RefreshRoomDisplays();

            Descriptions_List.Add(r1_Desc);
            Descriptions_List.Add(r2_Desc);
            Descriptions_List.Add(r3_Desc);
            Descriptions_List.Add(r4_Desc);
            Descriptions_List.Add(r5_Desc);
            Descriptions_List.Add(r6_Desc);
            Descriptions_List.Add(r7_Desc);

            L1Room_ExploreOptions.Add(r1_ExploreOptions);
            L1Room_ExploreOptions.Add(r2_ExploreOptions);
            L1Room_ExploreOptions.Add(r3_ExploreOptions);
            L1Room_ExploreOptions.Add(r4_ExploreOptions);
            L1Room_ExploreOptions.Add(r5_ExploreOptions);
            L1Room_ExploreOptions.Add(r6_ExploreOptions);
            L1Room_ExploreOptions.Add(r7_ExploreOptions);
        }

		private void RefreshRoomDisplays()
		{
            // Remove then add the rooms back to the list - meaning if an Environment Interactable has changed it will show
            
			Rooms1_List.Clear();

            string r1 = $@"
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

            string r2 = $@"
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
	║░│       {R1_Interactables[2][4]}                 	│░║
	║░│        {R1_Interactables[2][5]}               	│░║
	  │                             │
";

            string r3 = $@"
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
            
            string r4 = $@"";
            string r5 = $@"";
            string r6 = $@"";
            string r7 = $@"
	Level 1, South Hall & Chamber

                          │                             │
                        ║░│      {R1_Interactables[2][0]}                  │▒║
                        ║▒│     {R1_Interactables[2][1]}                │▒║
                        ║▒│  {R1_Interactables[2][2]}              │▒║
                        ║▒│       {R1_Interactables[2][3]}                 │▒║
                        ║▒│         {R1_Interactables[2][4]}                   │▒║
   ═════════════════════╝▒│                             │▒╚═══════════════════════
   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒│                             │▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
  ────────────────────────┘                             └──────────────────────────






    ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░   ░  
   ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░
    ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒ ▒
   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
";

            Rooms1_List.Add(r1);
            Rooms1_List.Add(r2);
            Rooms1_List.Add(r3);
            Rooms1_List.Add(r4);
            Rooms1_List.Add(r5);
            Rooms1_List.Add(r6);
            Rooms1_List.Add(r7);
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


