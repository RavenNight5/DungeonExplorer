using System;
using System.Diagnostics;
using DungeonExplorer.Testing;
using DungeonExplorer.Text_Displays;
using static System.Collections.Specialized.BitVector32;

namespace DungeonExplorer.Levels
{
    internal class Level_1
    {
        public static Level_1_Displays L1_Displays { get; private set; }
        public static Level_1_Actions L1_Actions { get; private set; }

        public bool[] R1_ActionCompleted = new bool[] { false, false, false, false };  //Door, Chest, LookOnTable, LookUnderTable
        public bool[] R2_ActionCompleted = new bool[] { false, false, false, false, false };  //Door, Wall, N, S, StandAround
        public bool[] R3_ActionCompleted = new bool[] { false, false, false };
        public bool[] R4_ActionCompleted = new bool[] { false, false, false };
        public bool[] R5_ActionCompleted = new bool[] { false, false, false };
        public bool[] R6_ActionCompleted = new bool[] { false, false, false };
        public static bool[] R7_ActionCompleted = new bool[] { false, false, false };  //N, Puddle, S

        private readonly bool[] room_DescriptionShown = new bool[] { false, false, false, false, false, false, false };

        public Level_1()
        {
            L1_Displays = new Level_1_Displays();
            L1_Actions = new Level_1_Actions();

            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Sponge);
            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_DustpanBrush);
            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Mop);

            // For testing new items display in the inventory as intended
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Key1);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Longsword);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Dagger);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupEmpty);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupFull);
        }

        public void Start()
        {
            DisplayRooms();
        }

        public static void UpdateOptions()  // Called every time the stats of the player are re-written to the console - checks if the equipped item can be used in a current situation
        {
            if (Room.CurrentRoom == 7)
            {
                if (R7_ActionCompleted[1] == false)  // If the puddle has not already been mopped
                {
                    if (Room.currentEquippedItem == Inventory_Items.II_Mop[0])  // If the currently equipped item is the mop item
                    {
                        Level_1_Displays.L1Room_ExploreOptions[Room.CurrentRoom - 1][1] = "Mop Puddle [2]";
                    }
                    else if (Room.currentEquippedItem == Inventory_Items.II_CupEmpty[0])
                    {
                        Level_1_Displays.L1Room_ExploreOptions[Room.CurrentRoom - 1][1] = "Use Empty Cup [2]";
                    }
                    else
                    {
                        Level_1_Displays.L1Room_ExploreOptions[Room.CurrentRoom - 1][1] = "Inspect Puddle [2]";
                    }
                }
               
            }
        }

        public void DisplayRooms()
        {
            Program.CLEAR_CONSOLE();

            string concatenatedOptions = Game.OptionHandler.GetGeneralOptions();

            Console.Write(Room.GetCurrentItemsAndStats());
            Console.Write(L1_Displays.GetRoom(Room.CurrentRoom)); Console.Write("\n" + concatenatedOptions + "\n\n");

            if (room_DescriptionShown[Room.CurrentRoom - 1].Equals(false))
            {
                // PlayerChoice ends when room description is shown and closed again
                Room.PlayerChoice(Options.GeneralOptionsKeyBinds);

                room_DescriptionShown[Room.CurrentRoom - 1] = true;

                DisplayRooms();

            }
            else  // Description has been shown so the player can choose what to explore
            {
                concatenatedOptions = Game.OptionHandler.GetRoomExploreOptions(Level_1_Displays.L1Room_ExploreOptions[Room.CurrentRoom - 1]);
                Console.Write(concatenatedOptions + "\n\n");

                string[] actions = GetActionKeybinds(Level_1_Displays.L1Room_ExploreOptions[Room.CurrentRoom - 1].Length);

                int action = Room.PlayerChoice(actions);

                if (Room.CurrentRoom == 1 && !(action <= -1))  // -1 is a different option is selected rather than an exploration action (such as D for description) then re-call DisplayRooms
                {
                    Tests.CheckRoomActionExists(L1_Actions.L1_RoomActions);
                    Tests.CheckActionTakenIsValid(action);

                    if (action < R1_ActionCompleted.Length)  // A valid action is taken (not viewing the room's description or another general option)
                    {
                        string[] dialogue;

                        Tests.CheckRoomActionExists(L1_Actions.L1_RoomActions);

                        // Door
                        if (action.Equals(0))
                        {
                            if (R1_ActionCompleted[action] == false)
                            {
                                if (Room.currentEquippedItem == Inventory_Items.II_Key1[0])  // If the currently equipped item is the correct required item
                                {
                                    Game.CurrentPlayer.RemoveItemFromInventory(Inventory_Items.II_Key1[0]);

                                    L1_Displays.R1_Interactables[action] = Environment_Interactables.Open_DoorVertical;

                                    dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action][1];
                                    Description_Box.ArrayDescription(dialogue, 32);

                                    R1_ActionCompleted[action] = true;
                                }
                                else
                                {
                                    dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action][0];
                                    Description_Box.ArrayDescription(dialogue, 32);
                                }
                            }
                            else
                            {
                                Room.CurrentRoom += 1;
                            }
                        }
                        // Chest
                        else if (action.Equals(1))
                        {
                            L1_Displays.R1_Interactables[action] = Environment_Interactables.Open_Chest;

                            if (R1_ActionCompleted[action] == false)
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action][0];

                                Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Key1);

                                R1_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action][1];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // LookOnTable
                        else if (action.Equals(2))
                        {
                            if (R1_ActionCompleted[action] == false)
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action][0];

                                Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupEmpty);

                                R1_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action][1];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // LookUnderTable
                        else if (action.Equals(3))
                        {
                            if (R1_ActionCompleted[action] == false)
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][2][2];
                                R1_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][2][3];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        //

                    }
                }
                else if (Room.CurrentRoom == 2 && action != -1)
                {
                    Tests.CheckRoomActionExists(L1_Actions.L1_RoomActions);
                    Tests.CheckActionTakenIsValid(action);

                    if (action < R2_ActionCompleted.Length)
                    {
                        string[] dialogue;

                        // Door
                        if (action.Equals(0))
                        {
                            Room.CurrentRoom -= 1;

                            R2_ActionCompleted[action] = true;
                        }
                        // Wall / StandAround
                        else if (action.Equals(1) || action.Equals(4))
                        {
                            if (R2_ActionCompleted[action] == false)
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action - 1][0];
                                R2_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action - 1][1];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // N
                        else if (action.Equals(2))
                        {
                            if (R2_ActionCompleted[action] == false)
                            {
                                dialogue = new string[] { "It is too dark, you don't want to go down there right now.", "You should probably wait until you have a torch of some kind." };
                                
                                R2_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = new string[] { "You must really want to mop them floors, huh?", "Well how can you if you can't see what you're doing!" };
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

                            Room.CurrentRoom = 7;
                        }

                    }
                }
                //Rooms 3-6
                else if (Room.CurrentRoom == 7 && action != -1)
                {
                    Tests.CheckRoomActionExists(L1_Actions.L1_RoomActions);
                    Tests.CheckActionTakenIsValid(action);

                    if (action < R7_ActionCompleted.Length)
                    {
                        string[] dialogue;

                        // North
                        if (action.Equals(0))
                        {
                            Room.CurrentRoom = 2;

                            R7_ActionCompleted[action] = true;
                        }
                        // Puddle
                        else if (action.Equals(1))
                        {
                            if (R7_ActionCompleted[action] == false)
                            {
                                if (Room.currentEquippedItem == Inventory_Items.II_Mop[0])
                                {
                                    dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action][1];

                                    R7_ActionCompleted[action] = true;

                                    L1_Displays.R1_Interactables[2] = Environment_Interactables.Puddle_1_Clean;
                                    Level_1_Displays.L1Room_ExploreOptions[Room.CurrentRoom - 1][1] = "Puddle Mopped!";
                                }
                                else if (Room.currentEquippedItem == Inventory_Items.II_CupEmpty[0])
                                {
                                    dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action][2];

                                    Game.CurrentPlayer.RemoveItemFromInventory(Inventory_Items.II_CupEmpty[0]);
                                    Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupFull);
                                }
                                else
                                {
                                    dialogue = L1_Actions.L1_RoomActions[Room.CurrentRoom - 1][action][0];
                                }

                                Description_Box.ArrayDescription(dialogue, 32);
                            }
                        }
                        // S
                        else if (action.Equals(2))
                        {
                            if (R7_ActionCompleted[action] == false)
                            {
                                dialogue = new string[] { "You have come to the end of this version.", "But do not fret, there will be plenty more to clean next time!" };

                                R7_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = new string[] { "Do not fret, there will be plenty more to clean next time!", "Also it's pretty dark in there, it would be better to wait until they switched the lights on." };
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }

                    }
                }

                DisplayRooms();

            }
        }

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

            return actions;

        }
    }
}
