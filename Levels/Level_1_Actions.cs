// Filename: Level_1_Actions.cs
using System;
using System.Collections.Generic;

namespace DungeonExplorer.Levels
{
    internal class Level_1_Actions
    {
        /// <summary>
        /// Holds the actions the player can take (that will be used in Description_Box) as string arrays. These actions are added to multiple 
        /// lists so they can easily be retrieved by index.
        /// </summary>

        public List<List<List<string[]>>> L1_RoomActions;

        //Room 1
        private readonly string[] _tryDoor_1 =
            {
            "You walk up to the door and try to open it.",
            "It seems to be locked.",
            "You remember you fell asleep in the cell, someone must have not seen you and locked you in."
            };
        private readonly string[] _openedDoor_1 =
            {
            "With a hefty twist of the key you hear a ~kerplunk~ as the door unlocks and slowly creeks away from you."
            };
        private readonly string[] _openChest_1 =
            {
            "The box is unlocked. You wonder if it was there before you fell asleep.",
            "You open it and find a key. How convenient... [Tab] to access inventory and select item"
            };
        private readonly string[] _openChest_1_Done =
            {
            "You have already looked inside the box. It is empty."
            };
        private readonly string[] _lookOnTable_1 =
            {
            "You look at the table you were clearly sleeping on.",
            "There is an empty cup, you take it."
            };
        private readonly string[] _lookOnTable_1_Done =
            {
            "You have already taken the cup from the table, there is nothing else on it."
            };
        private readonly string[] _lookUnderTable_1 =
            {
            "Nothing here, no stains, no puddles to mop, no monsters hiding.",
            "Just an ordinary underside to a table."
            };
        private readonly string[] _lookUnderTable_1_Done =
            {
            "You want to make doubly sure that there is nothing you missed under the table...",
            "So you crouch down to take a look under the table yet again.",
            "In actual fact, to your surprise you find~",
            "Nothing!!!",
            "So stop procrastinating - you have cleaning to do!"
            };

        //Room 2
        public string[] _inspectWall_2 =
            {
            "As you approach the cracked stone wall you realise something must have struck it from the other side for it to be pushed out like this.",
            "You should probably take a look at the other side later."
            };
        private readonly string[] _inspectWall_2_Done =
            {
            "Something must have struck the wall from the other side for it to be pushed out like this.",
            "You should probably take a look at the other side later."
            };
        private readonly string[] _n_2 =
            {
            "North.",
            ""
            };
        private readonly string[] _s_2 =
            {
            "South.",
            ""
            };
        private readonly string[] _standAround_2 =
            {
            "Though you have had hours of sleep you feel the need to stand aimlessly.",
            "Amidst this standing you feel a sense of acomplishment.",
            "You don't understand why but you feel proud to have landed this job.",
            "After all, you could have been the one taken captive in these walls..."
            };
        private readonly string[] _standAround_2_Done =
            {
            "You have stood around for some time already.",
            "If this is all you are going to do then don't be surprised if the next time you wake up you do not have a job...",
            "At the very least your sponge will be confiscated!",
            "I suggest you continue your job before someone finds you slacking."
            };

        //Room 7
        private readonly string[] _n_7 =
            {
            "",
            ""
            };
        private readonly string[] _puddle_7_Observe =
           {
            "This particular spot seems to always collect water from the floor above.",
            "Taking a closer look, however, it doesn't seem to be water... A thicker red liquid is dripping from the ceiling.",
            "Probably will stain the stone floors if it's not cleaned up soon.",
            "[Equip an item from your inventory to change the action you take: having the Mop equipped will change the prompt to clean the puddle!]"
            };
        private readonly string[] _puddle_7_Mop =
           {
            "Using your mop you start to clean the puddle.",
            "After you are finished you realise your mop has been turned compleately red.",
            "Better the mop than the floor I suppose!",
            };
        private readonly string[] _puddle_7_Cup =
           {
            "You take your empty cup and hold it under the main area the liquid seems to be dripping from.",
            "For some unknown reason you think it will aid you in your quest for a clean dungeon.",
            "Covering the cup, you put it back into your bag.",
            };
        private readonly string[] _s_7 =
            {
            "",
            ""
            };
        
        private readonly List<string[]> _door_1;
        private readonly List<string[]> _chest_1; 
        private readonly List<string[]> _table_1;
        private readonly List<List<string[]>> _room1_Actions;

        private readonly List<string[]> _wall_2;
        private readonly List<string[]> _north_2;
        private readonly List<string[]> _south_2;
        private readonly List<string[]> _stand_2;
        private readonly List<List<string[]>> _room2_Actions;

        //Rooms 3-6

        private readonly List<string[]> _north_7;
        private readonly List<string[]> _puddle_7;
        private readonly List<string[]> _south_7;
        private readonly List<List<string[]>> _room7_Actions;
        
        public Level_1_Actions()
        {
            // Initialise lists - meaning action can easily be referenced based on the current room
            // e.g. CurrentRoom is 1, all actions in that room can be found by L1_RoomActions[CurrentRoom - 1] (-1 to get the index) which points to Room1_Actions
            // which contains a list of string arrays containing all actions that can be taken in that room.
            _door_1 = new List<string[]> { _tryDoor_1, _openedDoor_1 };
            _chest_1 = new List<string[]> { _openChest_1, _openChest_1_Done };
            _table_1 = new List<string[]> { _lookOnTable_1, _lookOnTable_1_Done, _lookUnderTable_1, _lookUnderTable_1_Done };
            _room1_Actions = new List<List<string[]>> { _door_1, _chest_1, _table_1 };

            _wall_2 = new List<string[]> { _inspectWall_2, _inspectWall_2_Done };
            _north_2 = new List<string[]> { _n_2 };
            _south_2 = new List<string[]> { _s_2 };
            _stand_2 = new List<string[]> { _standAround_2, _standAround_2_Done };
            _room2_Actions = new List<List<string[]>> { _wall_2, _north_2, _south_2, _stand_2 };

            //Room actions 3-6

            _north_7 = new List<string[]> { _n_7 };
            _puddle_7 = new List<string[]> { _puddle_7_Observe, _puddle_7_Mop, _puddle_7_Cup };
            _south_7 = new List<string[]> { _s_7 };
            _room7_Actions = new List<List<string[]>> { _north_7, _puddle_7, _south_7 };

            L1_RoomActions = new List<List<List<string[]>>> { _room1_Actions, _room2_Actions, null, null, null, null, _room7_Actions };
        }
    }
}
