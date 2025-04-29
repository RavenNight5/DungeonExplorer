// Filename: Level_1.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DungeonExplorer.Item_Types;
using DungeonExplorer.Testing;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer.Levels
{
    internal class Level_1
    {
        /// <summary>
        /// Handles the main gameplay of level 1 and all of its rooms including:
        ///     - Items that can be interacted with
        ///     - Adding items to inventory (using Player class methods)
        ///     - Removing items from inventory (using Player class methods)
        ///     - Changing rooms
        ///     - Showing dialogue (using Description_Box class)
        /// </summary>
        public static bool[] R1_ActionCompleted = new bool[] { false, false, false, false };  //Door, Chest, LookOnTable, LookUnderTable
        public static bool[] R2_ActionCompleted = new bool[] { false, false, false, false, false };  //Door, Wall, N, S, StandAround
        public static bool[] R3_ActionCompleted = new bool[] { false, false, false };
        public static bool[] R4_ActionCompleted = new bool[] { false, false, false };
        public static bool[] R5_ActionCompleted = new bool[] { false, false, false };
        public static bool[] R6_ActionCompleted = new bool[] { false, false, false };

        public static bool CompletedBattle = false;

        public static bool[] _room_DescriptionShown = new bool[] { false, false, false, false, false, false, false };

        public static bool[] R7_ActionCompleted = new bool[] { false, false, false };  //N, Puddle, S

        public Level_1()
        {

            Game.CurrentPlayer.PickUpItem("Sponge");
            Game.CurrentPlayer.PickUpItem("Mop");
            Game.CurrentPlayer.PickUpItem("Dustpan");

            Game.CurrentPlayer.PickUpItem("Dagger");

        }

        // When the game starts room will call this function to display the first room
        public void Start()
        {
            DisplayRooms();
        }

        // Called every time the quick-stats of the player are re-written to the console and checks if the equipped item can be used in a current situation
        public static void UpdateOptions()
        {
            if (Game_Map.CurrentRoom == 7)
            {
                if (R7_ActionCompleted[1] == false)  // If the puddle has not already been mopped
                {
                    if (Room.CurrentEquippedItem == "Mop")  // If the currently equipped item is the mop item
                    {
                        Level_1_Displays.L1Room_ExploreOptions[Game_Map.CurrentRoom - 1][1] = "Mop Puddle [2]";
                    }
                    else if (Room.CurrentEquippedItem == "Empty Cup")
                    {
                        Level_1_Displays.L1Room_ExploreOptions[Game_Map.CurrentRoom - 1][1] = "Use Empty Cup [2]";
                    }
                    else
                    {
                        Level_1_Displays.L1Room_ExploreOptions[Game_Map.CurrentRoom - 1][1] = "Inspect Puddle [2]";
                    }
                }
               
            }
        }

        //Handles the main gameplay of level 1 and all of its rooms
        public void DisplayRooms()
        {
            Program.CLEAR_CONSOLE();

            string concatenatedOptions = Game.OptionHandler.GetGeneralOptions();

            Console.Write(Room.GetCurrentItemsAndStats());
            Console.Write(Level_1_Displays.GetRoom(Game_Map.CurrentRoom)); Console.Write("\n" + concatenatedOptions + "\n\n");

            if (_room_DescriptionShown[Game_Map.CurrentRoom - 1].Equals(false))
            {
                // PlayerChoice ends when room description is shown and closed again
                Room.PlayerChoice(Options.GeneralOptionsKeyBinds);

                _room_DescriptionShown[Game_Map.CurrentRoom - 1] = true;

                DisplayRooms();

            }
            else  // Description has been shown so the player can choose what to explore
            {
                concatenatedOptions = Game.OptionHandler.GetRoomExploreOptions(Level_1_Displays.L1Room_ExploreOptions[Game_Map.CurrentRoom - 1]);
                Console.Write(concatenatedOptions + "\n\n");

                string[] actions = GetActionKeybinds(Level_1_Displays.L1Room_ExploreOptions[Game_Map.CurrentRoom - 1].Length);

                int action = Room.PlayerChoice(actions);

                if (Game_Map.CurrentRoom == 1 && !(action <= -1))  // -1 is a different option is selected rather than an exploration action (such as D for description) then re-call DisplayRooms
                {
                    Tests.CheckRoomActionExists(Level_1_Actions.L1_RoomActions);
                    Tests.CheckActionTakenIsValid(action);

                    if (action < R1_ActionCompleted.Length)  // A valid action is taken (not viewing the room's description or another general option)
                    {
                        string[] dialogue;

                        Tests.CheckRoomActionExists(Level_1_Actions.L1_RoomActions);

                        // Door
                        if (action.Equals(0))
                        {
                            if (R1_ActionCompleted[action] == false)
                            {
                                if (Room.CurrentEquippedItem == "Rusty Key")  // If the currently equipped item is the correct required item
                                {
                                    Game.CurrentPlayer.RemoveItemFromInventory("Rusty Key");

                                    Level_1_Displays.R1_Interactables[action] = Environment_Interactables.Open_DoorVertical;

                                    dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action][1];
                                    Description_Box.ArrayDescription(dialogue, 32);

                                    R1_ActionCompleted[action] = true;
                                }
                                else
                                {
                                    dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action][0];
                                    Description_Box.ArrayDescription(dialogue, 32);
                                }
                            }
                            else
                            {
                                Game_Map.CurrentRoom += 1;
                            }
                        }
                        // Chest
                        else if (action.Equals(1))
                        {
                            Level_1_Displays.R1_Interactables[action] = Environment_Interactables.Open_Chest;

                            if (R1_ActionCompleted[action] == false)
                            {
                                dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action][0];

                                Game.CurrentPlayer.PickUpItem("Rusty Key");

                                R1_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action][1];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // LookOnTable
                        else if (action.Equals(2))
                        {
                            if (R1_ActionCompleted[action] == false)
                            {
                                dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action][0];

                                Game.CurrentPlayer.PickUpItem("Empty Cup");

                                R1_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action][1];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // LookUnderTable
                        else if (action.Equals(3))
                        {
                            if (R1_ActionCompleted[action] == false)
                            {
                                dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][2][2];
                                R1_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][2][3];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        //

                    }
                }
                else if (Game_Map.CurrentRoom == 2 && action != -1)
                {
                    Tests.CheckRoomActionExists(Level_1_Actions.L1_RoomActions);
                    Tests.CheckActionTakenIsValid(action);

                    if (action < R2_ActionCompleted.Length)
                    {
                        string[] dialogue;

                        // Door
                        if (action.Equals(0))
                        {
                            Game_Map.CurrentRoom -= 1;

                            R2_ActionCompleted[action] = true;
                        }
                        // Wall / Stand Around
                        else if (action.Equals(1) || action.Equals(4))
                        {
                            if (R2_ActionCompleted[action] == false)
                            {
                                dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action - 1][0];

                                if (action.Equals(1))
                                {
                                    Game.CurrentPlayer.PickUpItem("Longsword");
                                }
                                
                                R2_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action - 1][1];
                                
                                if (action.Equals(1))
                                {
                                    Game.CurrentPlayer.PickUpItem("Beefy Sponge");
                                }
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // N
                        else if (action.Equals(2))
                        {
                            if (R2_ActionCompleted[action] == false)
                            {
                                if (Player.InventoryItems.Contains("Longsword"))
                                {
                                    if (!CompletedBattle)
                                    {
                                        dialogue = new string[] { "Maybe I should try out this new weapon somewhere before continuing-", "You keep headding South to find a suitable spot to test the weapon." };
                                    }
                                    else  // Only starts Gnome battle when the player has the longsword and has battled the dragon
                                    {
                                        dialogue = new string[] { "With your weapon tested, approved, and ready to go you begin to walk down the unlit hallway...", "A strange smell fills the air?", "Similar to... Pottery? Or a plant pot~", "No.", "It's THE GARDENER" };

                                        Description_Box.ArrayDescription(dialogue, 32);

                                        Program.game.StartCombat(1);  // Start combat with monster 'The Gardener'

                                        R2_ActionCompleted[action] = true;
                                    }
                                    
                                }
                                else
                                {
                                    //dialogue = new string[] { "You must really want to mop them floors, huh?", "Well how can you if you can't see what you're doing!" };

                                    dialogue = new string[] { "It is too dark, I don't want to go up there right now.", "I'd feel safer if I had a better defense weapon... I should look around some more." };
                                }
                            }
                            else
                            {
                                dialogue = new string[] { "The Gardener. What a guy... Shame I had to take him down.", "I should get a pay rise.", "Or at the very least a nametag showing off my 'Monster Cleaner' status." };
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // S
                        else if (action.Equals(3))
                        {
                            if (R2_ActionCompleted[action] == false)
                            {
                                R2_ActionCompleted[action] = true;

                                dialogue = new string[] { "You head south down the long hallway.", "A subtle dripping noise can be heard, it seems to echo around the chamber up ahead." };

                                Description_Box.ArrayDescription(dialogue, 32);
                            }
                            //dialogue = new string[] { "You have come to the end of this version.", "But do not fret, there will be plenty of cleaning next time!" };

                            Game_Map.CurrentRoom = 7;
                        }
                        
                    }
                }
                //Rooms 3-6
                else if (Game_Map.CurrentRoom == 7 && action != -1)
                {
                    Tests.CheckRoomActionExists(Level_1_Actions.L1_RoomActions);
                    Tests.CheckActionTakenIsValid(action);

                    if (action < R7_ActionCompleted.Length)
                    {
                        string[] dialogue;

                        // North
                        if (action.Equals(0))
                        {
                            Game_Map.CurrentRoom = 2;

                            R7_ActionCompleted[action] = true;
                        }
                        // Puddle
                        else if (action.Equals(1))
                        {
                            if (R7_ActionCompleted[action] == false)
                            {
                                if (Room.CurrentEquippedItem == "Mop")
                                {
                                    dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action][1];

                                    R7_ActionCompleted[action] = true;

                                    Level_1_Displays.R1_Interactables[2] = Environment_Interactables.Puddle_1_Clean;

                                    Level_1_Displays.L1Room_ExploreOptions[Game_Map.CurrentRoom - 1][1] = "Puddle Mopped! (+8 Coins)";
                                    Player.GoldCoins += 8;
                                }
                                else if (Room.CurrentEquippedItem == "Empty Cup")
                                {
                                    dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action][2];

                                    Game.CurrentPlayer.RemoveItemFromInventory("Empty Cup");
                                    Game.CurrentPlayer.PickUpItem("Blood Cup");
                                }
                                else
                                {
                                    dialogue = Level_1_Actions.L1_RoomActions[Game_Map.CurrentRoom - 1][action][0];
                                }

                                Description_Box.ArrayDescription(dialogue, 32);
                            }
                        }
                        // S
                        else if (action.Equals(2))
                        {
                            if (R7_ActionCompleted[action] == false)
                            {
                                dialogue = new string[] { "You walk down into the large chamber.", "A piercing roar fills the room and you are now standing before a beast..." };
                                
                                Description_Box.ArrayDescription(dialogue, 32);

                                R7_ActionCompleted[action] = true;

                                Program.game.StartCombat(0);  // Enter combat with the Dragon Monster
                            }
                            else
                            {
                                dialogue = new string[] { "The memories from that battle stain your mind like the dungeon puddles..." };

                                Description_Box.ArrayDescription(dialogue, 32);
                            }
                        }

                    }
                }

                DisplayRooms();

            }
        }

        // Returns all the keybinds the player can take when exploring a room (adds on the default option keys such as Tab for inventoy)
        private string[] GetActionKeybinds(int numOfActions)
        {
            string[] actions = new string[numOfActions + 3];  // Would be -1 for the indexing, however I add 3 more keybinds outside the for loop

            for (int i = 0; i < numOfActions; i++)
            {
                actions[i] = ("D" + (i + 1).ToString());  // As "D1" refers to the keyboard key 1
            }

            // Add the other general option the player can do when exploring a room
            actions[numOfActions] = "D";
            actions[numOfActions + 1] = "C";
            actions[numOfActions + 2] = "Tab";

            //Console.WriteLine(string.Join(", ", actions));
            //Debug.WriteLine(Game_Map.CurrentRoom);

            return actions;

        }
    }
}
