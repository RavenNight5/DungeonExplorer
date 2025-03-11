using System;
using System.Collections.Generic;

namespace DungeonExplorer.Levels
{
    internal class Level_1_Actions
    {
        //Room 1
        private readonly string[] tryDoor_1 =
            {
            "You walk up to the door and try to open it.",
            "It seems to be locked.",
            "You remember you fell asleep in the cell, someone must have not seen you and locked you in."
            };
        private readonly string[] openedDoor_1 =
            {
            "With a hefty twist of the key you hear a ~kerplunk~ as the door unlocks and slowly creeks away from you."
            };
        private readonly string[] openChest_1 =
            {
            "The box is unlocked. You wonder if it was there before you fell asleep.",
            "You open it and find a key. How convenient... [Tab] to access inventory and select item"
            };
        private readonly string[] openChest_1_Done =
            {
            "You have already looked inside the box. It is empty."
            };
        private readonly string[] lookOnTable_1 =
            {
            "You look at the table you were clearly sleeping on.",
            "There is an empty cup, you take it."
            };
        private readonly string[] lookOnTable_1_Done =
            {
            "You have already taken the cup from the table, there is nothing else on it."
            };
        private readonly string[] lookUnderTable_1 =
            {
            "Nothing here, no stains, no puddles to mop, no monsters hiding.",
            "Just an ordinary underside to a table."
            };
        private readonly string[] lookUnderTable_1_Done =
            {
            "You want to make doubly sure that there is nothing you missed under the table...",
            "So you crouch down to take a look under the table yet again.",
            "In actual fact, to your surprise you find~",
            "Nothing!!!",
            "So stop procrastinating - you have cleaning to do!"
            };

        //Room 2
        public string[] inspectWall_2 =
            {
            "As you approach the cracked stone wall you realise something must have struck it from the other side for it to be pushed out like this.",
            "You should probably take a look at the other side later."
            };
        public string[] inspectWall_2_Done =
            {
            "Something must have struck the wall from the other side for it to be pushed out like this.",
            "You should probably take a look at the other side later."
            };
        public string[] n_2 =
            {
            "North.",
            ""
            };
        public string[] s_2 =
            {
            "South.",
            ""
            };
        public string[] standAround_2 =
            {
            "Though you have had hours of sleep you feel the need to stand aimlessly.",
            "Amidst this standing you feel a sense of acomplishment.",
            "You don't understand why but you feel proud to have landed this job.",
            "After all, you could have been the one taken captive in these walls..."
            };
        public string[] standAround_2_Done =
            {
            "You have stood around for some time already.",
            "If this is all you are going to do then don't be surprised if the next time you wake up you do not have a job...",
            "At the very least your sponge will be confiscated!",
            "I suggest you continue your job before someone finds you slacking."
            };

        //Room 7
        public string[] n_7 =
            {
            "",
            ""
            };
        public string[] puddle_7_Observe =
           {
            "This particular spot seems to always collect water from the floor above.",
            "Taking a closer look, however, it doesn't seem to be water... A thicker red liquid is dripping from the ceiling.",
            "Probably will stain the stone floors if it's not cleaned up soon.",
            "[Equip an item from your inventory to change the action you take: having the Mop equipped will change the prompt to clean the puddle!]"
            };
        public string[] puddle_7_Mop =
           {
            "Using your mop you start to clean the puddle.",
            "After you are finished you realise your mop has been turned compleately red.",
            "Better the mop than the floor I suppose!",
            };
        public string[] puddle_7_Cup =
           {
            "You take your empty cup and hold it under the main area the liquid seems to be dripping from.",
            "For some unknown reason you think it will aid you in your quest for a clean dungeon.",
            "Covering the cup, you put it back into your bag.",
            };
        public string[] s_7 =
            {
            "",
            ""
            };
        

        private readonly List<List<string[]>> room1_Actions;
        private readonly List<string[]> door_1;
        private readonly List<string[]> chest_1; 
        private readonly List<string[]> table_1;


        private readonly List<List<string[]>> room2_Actions;
        private readonly List<string[]> wall_2;
        private readonly List<string[]> north_2;
        private readonly List<string[]> south_2;
        private readonly List<string[]> stand_2;

        //Rooms 3-6

        private readonly List<List<string[]>> room7_Actions;
        private readonly List<string[]> north_7;
        private readonly List<string[]> puddle_7;
        private readonly List<string[]> south_7;

        public List<List<List<string[]>>> L1_RoomActions;
        
        public Level_1_Actions()
        {
            // Initialise lists - meaning action can easily be referenced based on the current room
            // e.g. CurrentRoom is 1, all actions in that room can be found by L1_RoomActions[CurrentRoom - 1] (-1 to get the index) which points to Room1_Actions which contains
            // a list of string arrays containing all actions that can be taken in that room.
            door_1 = new List<string[]> { tryDoor_1, openedDoor_1 };
            chest_1 = new List<string[]> { openChest_1, openChest_1_Done };
            table_1 = new List<string[]> { lookOnTable_1, lookOnTable_1_Done, lookUnderTable_1, lookUnderTable_1_Done };
            room1_Actions = new List<List<string[]>> { door_1, chest_1, table_1 };

            wall_2 = new List<string[]> { inspectWall_2, inspectWall_2_Done };
            north_2 = new List<string[]> { n_2 };
            south_2 = new List<string[]> { s_2 };
            stand_2 = new List<string[]> { standAround_2, standAround_2_Done };
            room2_Actions = new List<List<string[]>> { wall_2, north_2, south_2, stand_2 };

            //Room actions 3-6

            north_7 = new List<string[]> { n_7 };
            puddle_7 = new List<string[]> { puddle_7_Observe, puddle_7_Mop, puddle_7_Cup };
            south_7 = new List<string[]> { s_7 };
            room7_Actions = new List<List<string[]>> { north_7, puddle_7, south_7 };

            L1_RoomActions = new List<List<List<string[]>>> { room1_Actions, room2_Actions, null, null, null, null, room7_Actions };
        }
    }
}
