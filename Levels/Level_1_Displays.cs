// Filename: Level_1_Displays.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DungeonExplorer.Testing;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer.Levels
{
    internal class Level_1_Displays
    {
        /// <summary>
        /// Contains the room descriptions as well as their displays. These are added to lists so that the correct room can be automatically
        /// fetched with the current room number by using it as an index for the required list.
        /// </summary>
        
        public static List<string[]> R1_Interactables = new List<string[]> { 
            Environment_Interactables.Closed_DoorVertical, 
            Environment_Interactables.Closed_Chest, 
            Environment_Interactables.Puddle_1 };

        public static int _lastRoomFetched = 0;

        private readonly static string _r1_Desc = "A small rectangular room surrounds you, furnished with a singular wooden table and a small battered box against the right wall. You don't notice anything out of the ordinary.";
        private readonly static string _r2_Desc = "This hallway serves as a primary passageway for staff navigating the dungeon. While cells line the walls, they hold fewer occupants: this area is usuially used as an overflow quater. You look opposite you, a bit further up there seems to be a bump in the wall... Perhaps you should check it out.";
        private readonly static string _r3_Desc = "The hallway continues with empty cells appearing on your left as you progress. Nothing much to clean out here.";
        private readonly static string _r4_Desc = "";
        private readonly static string _r5_Desc = "";
        private readonly static string _r6_Desc = "";
        private readonly static string _r7_Desc = "The narrower hallway opens out into a chamber. The lights are out, making the room feel deceptively larger than usual. Besides the unlit room you find a puddle behind the partition in the hallway: there is usually some water seeping from the roof in this spot...";

        private readonly static string[] _r1_ExploreOptions = {
            "Use Door [1]",
            "Open Chest [2]",
            "Look On Table [3]",
            "Look Under Table [4]" };
        private readonly static string[] _r2_ExploreOptions = {
            "Use Cell Door [1]",
            "Investigate Wall [2]",
            "Go North [3]",
            "Go South [4]",
            "Stand Around [5]" };
        private readonly static string[] _r3_ExploreOptions = {
            "Try Doors [1]",
            "Go North [2]",
            "Go South [3]" };
        private readonly static string[] _r4_ExploreOptions = {
            "xxx [1]",
            "xxx [2]",
            "xxx [3]" };
        private readonly static string[] _r5_ExploreOptions = {
            "xxx [1]",
            "xxx [2]",
            "xxx [3]" };
        private readonly static string[] _r6_ExploreOptions = {
            "xxx [1]",
            "xxx [2]",
            "xxx [3]" };
        private readonly static string[] _r7_ExploreOptions = {
            "Go North [1]",
            "Inspect Puddle [2]",
            "Go Into the Chamber [3]" };

        public static List<string[]> L1Room_ExploreOptions = new List<string[]>()
        {
            _r1_ExploreOptions,
            _r2_ExploreOptions,
            _r3_ExploreOptions,
            _r4_ExploreOptions,
            _r5_ExploreOptions,
            _r6_ExploreOptions,
            _r7_ExploreOptions
        };

        public static List<string> _descriptions_List = new List<string>()
        {
            _r1_Desc,
            _r2_Desc,
            _r3_Desc,
            _r4_Desc,
            _r5_Desc,
            _r6_Desc,
            _r7_Desc
        };

        public static List<string> _rooms1_List = new List<string>();

        private static void RefreshRoomDisplays()
		{
            // Remove then add the rooms back to the list - meaning if an Environment Interactable has changed it will show
            
			_rooms1_List.Clear();

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

            _rooms1_List.Add(r1);
            _rooms1_List.Add(r2);
            _rooms1_List.Add(r3);
            _rooms1_List.Add(r4);
            _rooms1_List.Add(r5);
            _rooms1_List.Add(r6);
            _rooms1_List.Add(r7);
        }

        // Returns a room display string based on the room number provided
        public static string GetRoom(int roomNumber)
        {
			RefreshRoomDisplays();

            _lastRoomFetched = roomNumber - 1;

            Tests.CheckRoomDisplayExists(_lastRoomFetched, _descriptions_List.Count());

            Room.CurrentRoomDescription = _descriptions_List[_lastRoomFetched];

            try
            {
                string roomDisplay = _rooms1_List[roomNumber - 1];

                return roomDisplay;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Room does not exist. Exception caught: " + e.Message);

                return "Room to be fetched does not exist.";
            }
        }

    }
}


