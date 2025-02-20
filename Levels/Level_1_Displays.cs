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

		private Environment_Interactables environment_Interactables = new Environment_Interactables();

        private List<string> RoomsList = new List<string>();
        private List<string> RoomDescriptionsList = new List<string>();

        private int lastRoomFetched = 1;

        private string R1_Desc = "A small rectangular room surrounds you, furnished with a singular wooden table and a small battered box against the right wall.";
        private string R2_Desc = "Hallway///";
        private string R3_Desc = "North///";

        private string R1 = $@"
  Level 1, The Cell.

║░┌─────────────────────────────────────────────────────┐░║
║░│                                                 	┌|  
║▒│ 	 ---───────---                              	│|  
║▒│     |░░░░░░░░░░░░░|                             	{Environment_Interactables.Closed_Door}|  
║▓│ 	║░░░░░░░░░░░░░║                             	└|  
║▓│ 	║░░░░░░░░░░░░░║                              	│░║
║▓│ 	|░░░░░░░░░░░░░|                              	│░║
║▒│ 	 ---───────---                               	│▒║
║▒│                      	{Environment_Interactables.Closed_Chest[0]}                	│▒║
║░│                      	{Environment_Interactables.Closed_Chest[1]}                	│░║
║░└─────────────────────────────────────────────────────┘░║
                                           	 
	";
        private string R2 = $@"
 	Level 1, Hallway    

	║░│░░ ░░░ ░░░ ░░░ ░░░ ░░░ ░░░ ░░│░║
	║░│ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ │░║
	║▒│  ░   ░   ░   ░   ░   ░   ░  |░║
	║▒│                        	   //░║
	║▒│                            /░░║
	║░│                            |░░║
	╗░│                            \\░║
	|┐                           	|░║
	|│                           	│░║
	|{Environment_Interactables.Open_Door}                           	│▒║
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
        private string R3 = $@"
 	Level 1, North Hallway    

	╗░│░░ ░░░ ░░░ ░░░ ░░░ ░░░ ░░░ ░░│▒║
	|┐░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ ░ │▒║
	|│   ░   ░   ░   ░   ░   ░   ░  │▒║
	|{Environment_Interactables.Open_Door}                           	│▒║
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




		private string RX = $@"
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

        public Level_1_Displays()
        {
            RoomsList.Add(R1);
            RoomsList.Add(R2);
            RoomsList.Add(R3);

			RoomDescriptionsList.Add(R1_Desc);
            RoomDescriptionsList.Add(R2_Desc);
            RoomDescriptionsList.Add(R3_Desc);

			//R1.Replace("{Environment_Interactables.Closed_Chest[0]}", Environment_Interactables.Open_Chest[0]);
        }

        public string GetRoom(int roomNumber)
        {
            lastRoomFetched = roomNumber - 1;

            try
            {
                string roomDisplay = RoomsList[roomNumber - 1];

                return roomDisplay;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return "Room does not exist.";
            }
        }

        public string GetDescription()
        {
            return RoomDescriptionsList[lastRoomFetched];
        }
    }
}


