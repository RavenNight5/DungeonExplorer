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
        private string[] OpenedDoor =
            {
            "With a hefty twist of the key you hear a ~kerplunk~ as the door unlocks and slowly creeks away from you."
            };
        private string[] OpenChest =
            {
            "The box is unlocked. You wonder if it was there before you fell asleep.",
            "You open it and find a key. How convenient... [Tab] to access inventory and select item"
            };
        private string[] OpenChest_Done =
            {
            "You have already looked inside the box. It is empty."
            };
        private string[] LookOnTable =
            {
            "You look at the table you were clearly sleeping on.",
            "There is an empty cup, you take it."
            };
        private string[] LookOnTable_Done =
            {
            "You have already taken the cup from the table, there is nothing else on it."
            };
        private string[] LookUnderTable =
            {
            "Nothing here, no stains, no puddles to mop, no beast hiding.",
            "Just an ordinary underside to a table."
            };
        private string[] LookUnderTable_Done =
            {
            "You want to make doubly sure that there is nothing you missed under the table...",
            "So you crouch down to take a look under the table yet again.",
            "In actual fact, to your surprise you find~",
            "Nothing!!!",
            "So stop procrastinating - you have cleaning to do!"
            };

        //Room 2
        public string[] InspectWall =
            {
            "As you approach the cracked stone wall you realise something must have struck it from the other side for it to be pushed out like this.",
            "You should probably take a look at the other side later."
            };
        public string[] InspectWall_Done =
            {
            "Something must have struck the wall from the other side for it to be pushed out like this.",
            "You should probably take a look at the other side later."
            };
        public string[] N =
            {
            "North.",
            ""
            };
        public string[] S =
            {
            "South.",
            ""
            };
        public string[] StandAround =
            {
            "Though you have had hours of sleep you feel the need to stand aimlessly.",
            "Amidst this standing you feel a sense of acomplishment.",
            "You don't understand why but you feel proud to have landed this job.",
            "After all, you could have been the one taken captive in these walls..."
            };
        public string[] StandAround_Done =
            {
            "You have stood around for some time already.",
            "If this is all you are going to do then don't be surprised if the next time you wake up you do not have a job...",
            "At the very least your sponge will be confiscated!",
            "I suggest you continue your job before someone finds you slacking."
            };

        private List<List<string[]>> Room1_Actions;
        private List<string[]> Door;
        private List<string[]> Chest; 
        private List<string[]> Table;


        private List<List<string[]>> Room2_Actions;
        private List<string[]> Wall;
        private List<string[]> North;
        private List<string[]> South;
        private List<string[]> Stand;

        public List<List<List<string[]>>> L1_RoomActions;
        
        public Level_1_Actions()
        {
            // Initialise lists - meaning action can easily be referenced based on the current room
            // e.g. currentRoom is 1, all actions in that room can be found by L1_RoomActions[currentRoom - 1] (-1 to get the index) which points to Room1_Actions which contains
            // a list of string arrays containing all actions that can be taken in that room.
            Door = new List<string[]> { TryDoor, OpenedDoor };
            Chest = new List<string[]> { OpenChest, OpenChest_Done };
            Table = new List<string[]> { LookOnTable, LookOnTable_Done, LookUnderTable, LookUnderTable_Done };
            Room1_Actions = new List<List<string[]>> { Door, Chest, Table };

            Wall = new List<string[]> { InspectWall, InspectWall_Done };
            North = new List<string[]> { N };
            South = new List<string[]> { S };
            Stand = new List<string[]> { StandAround, StandAround_Done };
            Room2_Actions = new List<List<string[]>> { Wall, North, South, Stand };

            L1_RoomActions = new List<List<List<string[]>>> { Room1_Actions, Room2_Actions };
        }
    }
}
