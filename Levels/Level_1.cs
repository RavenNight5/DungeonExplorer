using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer.Levels
{
    internal class Level_1
    {
        public static Level_1_Displays L1_Displays { get; private set; }
        public static Level_1_Actions L1_Actions { get; private set; }

        public bool[] R1_ActionCompleted = new bool[] { false, false, false, false };  //Door, Chest, LookOnTable, LookUnderTable
        public bool[] R2_ActionCompleted = new bool[] { false, false, false, false, false };  //Door, Wall, N, S, StandAround

        private bool[] Room_DescriptionShown = new bool[] { false, false, false, false };

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

        public void DisplayRooms()
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");

            string concatenatedOptions = Game.OptionHandler.GetGeneralOptions();

            Console.Write(Room.GetCurrentItemsAndStats());
            Console.Write(L1_Displays.GetRoom(Room.currentRoom)); Console.Write("\n" + concatenatedOptions + "\n\n");


            if (Room_DescriptionShown[Room.currentRoom].Equals(false))
            {
                // PlayerChoice ends when room description is shown and closed again
                Room.PlayerChoice(Options.GeneralOptionsKeyBinds);

                Room_DescriptionShown[Room.currentRoom] = true;

                DisplayRooms();

            }
            else  // Description has been shown so the player can choose what to explore
            {
                concatenatedOptions = Game.OptionHandler.GetRoomExploreOptions(L1_Displays.Room_ExploreOptions[Room.currentRoom - 1]);
                Console.Write(concatenatedOptions + "\n\n");

                string[] actions = GetActionKeybinds(L1_Displays.Room_ExploreOptions[Room.currentRoom - 1].Length);

                int action = Room.PlayerChoice(actions);

                if (Room.currentRoom == 1 && !(action <= -1))  // -1 is a different option is selected rather than an exploration action (such as D for description) then re-call DisplayRooms
                {
                    Debug.Assert(!(action <= -1), "An invalid explore action was erroneously passed through.");

                    if (action < R1_ActionCompleted.Length)  // A valid action is taken (not viewing the room's description or another general option)
                    {
                        string[] dialogue;

                        // Door
                        if (action.Equals(0))
                        {
                            if (R1_ActionCompleted[action] == false)
                            {
                                if (Room.currentEquippedItem == Inventory_Items.II_Key1[0])  // If the currently equipped item is the correct required item
                                {
                                    Game.CurrentPlayer.RemoveItemFromInventory(Inventory_Items.II_Key1[0]);

                                    L1_Displays.R1_Interactables[action] = Environment_Interactables.Open_DoorVertical;

                                    dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][action][1];
                                    Description_Box.ArrayDescription(dialogue, 32);

                                    R1_ActionCompleted[action] = true;
                                }
                                else
                                {
                                    dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][action][0];
                                    Description_Box.ArrayDescription(dialogue, 32);
                                }
                            }
                            else
                            {
                                Room.currentRoom += 1;
                            }
                        }
                        // Chest
                        else if (action.Equals(1))
                        {
                            L1_Displays.R1_Interactables[action] = Environment_Interactables.Open_Chest;

                            if (R1_ActionCompleted[action] == false)
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][action][0];

                                Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Key1);

                                R1_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][action][1];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // LookOnTable
                        else if (action.Equals(2))
                        {
                            if (R1_ActionCompleted[action] == false)
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][action][0];

                                Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupEmpty);

                                R1_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][action][1];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // LookUnderTable
                        else if (action.Equals(3))
                        {
                            if (R1_ActionCompleted[action] == false)
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][2][2];
                                R1_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][2][3];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        //

                    }
                }
                else if (Room.currentRoom == 2 && action != -1)
                {
                    Debug.Assert(!(action <= -1), "An invalid explore action was erroneously passed through.");

                    if (action < R2_ActionCompleted.Length)  // A valid action is taken (not viewing the room's description or another general option)
                    {
                        string[] dialogue;

                        // Door
                        if (action.Equals(0))
                        {
                            Room.currentRoom -= 1;

                            R2_ActionCompleted[action] = true;
                        }
                        // Wall / StandAround
                        else if (action.Equals(1) || action.Equals(4))
                        {
                            if (R2_ActionCompleted[action] == false)
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][action - 1][0];
                                R2_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = L1_Actions.L1_RoomActions[Room.currentRoom - 1][action - 1][1];
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // N
                        else if (action.Equals(2))
                        {
                            if (R2_ActionCompleted[action] == false)
                            {
                                dialogue = new string[] { "You have come to the end of this version.", "But do not fret, there will be plenty of cleaning next time!" };
                                Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupEmpty);

                                R2_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = new string[] { "You have come to the end of this version.", "But do not fret, there will be plenty of cleaning next time!" };
                            }

                            Description_Box.ArrayDescription(dialogue, 32);
                        }
                        // S
                        else if (action.Equals(3))
                        {
                            if (R2_ActionCompleted[action] == false)
                            {
                                dialogue = new string[] { "You have come to the end of this version.", "But do not fret, there will be plenty of cleaning next time!" };
                                R2_ActionCompleted[action] = true;
                            }
                            else
                            {
                                dialogue = new string[] { "You have come to the end of this version.", "But do not fret, there will be plenty of cleaning next time!" };
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
