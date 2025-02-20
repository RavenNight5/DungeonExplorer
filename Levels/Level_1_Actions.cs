using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer.Levels
{
    internal class Level_1_Actions
    {
        //Room 1
        private string[] TryDoor =
            {
            "You walk up to the door and try to open it.",
            "It seems to be locked.",
            "You remember you fell asleep in the cell, someone must have not seen you and locked you in."
            };
        private string[] OpenDoor =
            {
            "With a hefty twist of the key you hear a ~kerplunk~ as the door unlocks and slowly creeks away from you."
            };
        private string[] LookOnTable =
            {
            "You look at the table you were clearly sleeping on.",
            "There is an empty cup, you take it."
            };
        private string[] LookUnderTable =
            {
            "Nothing here, no stains, no puddles to mop, no beast hiding.",
            "Just an ordinary underside to a table."
            };
        private string[] LookInChest =
            {
            "The box is unlocked. You wonder if it was there before you fell asleep.",
            "You open it and find a key. How convenient..."
            };

        //Room 2
        public string[] InspectWall =
            {
            "As you approach the cracked stone wall you realise something must have struck it from the other side for it to be pushed out like this.",
            "You should probably take a look at the other side later."
            };
        public string[] StandAround =
            {
            "Though you have had hours of sleep you feel the need to stand aimlessly.",
            "Amidst this standing you feel a sense of acomplishment.",
            "You don't understand why but you feel proud to have landed this job.",
            "After all, you could have been held captive in these walls rather than scrubbing them."
            };

        public List<string[]> Room1_Actions;
        public List<string[]> Room2_Actions;

        public List<List<string[]>> ALL_RoomActions;

        public Level_1_Actions()
        {
            Room1_Actions = new List<string[]> { TryDoor, OpenDoor, LookOnTable, LookUnderTable, LookInChest };
            Room2_Actions = new List<string[]> { InspectWall, StandAround};

            ALL_RoomActions = new List<List<string[]>> { Room1_Actions, Room2_Actions };
        }

        public void DoAction(int action)
        {
            try
            {
                string[] dialogue = ALL_RoomActions[Room.currentRoom][action];

                Description_Box.ArrayDescription(dialogue);

                Game.RoomHandler.ReturnToLevel();
            }
            catch
            {
                Debug.WriteLine("Action dialogue could not be found.");
            }
        }
    }
}
