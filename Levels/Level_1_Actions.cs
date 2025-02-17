using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer.Levels
{
    internal class Level_1_Actions
    {
        //Room 1
        public string[] TryDoor =
            {
            "You walk up to the door and try to open it.",
            "It seems to be locked.",
            "You remember you fell asleep in the cell, someone must have not seen you and locked you in."
            };
        public string[] OpenDoor =
            {
            "With a hefty twist of the key you hear a ~kerplunk~ as the door unlocks and slowly creeks away from you."
            };
        public string[] LookOnTable =
            {
            "You look at the table you were clearly sleeping on.",
            "There is an empty cup, you take it."
            };
        public string[] LookUnderTable =
            {
            "Nothing here, no stains, no puddles to mop, no beast hiding.",
            "Just an ordinary underside to a table."
            };
        public string[] LookInChest =
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
    }
}
