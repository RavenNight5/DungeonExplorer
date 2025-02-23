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

        private List<bool> R1_ActionCompleted = new List<bool> { false, false, false, false };  //Door, Chest, OnTable, UnderTable
        private List<string[]> R1_ActionRequiresItem = new List<string[]> { Inventory_Items.II_Key1[0], null, null, null };
        
        private List<bool> Room_DescriptionShown = new List<bool> { false, false, false, false };

        public Level_1()
        { 
            L1_Displays = new Level_1_Displays();
            L1_Actions = new Level_1_Actions();

            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Sponge);
            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_DustpanBrush);
            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Mop);

            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Key1);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Longsword);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Dagger);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupEmpty);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupFull);

        }

        public void Start()
        {
            DisplayRooms(1);
        }

        public void DisplayRooms(int roomToDisplay)
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");

            Room.currentRoomDescription = L1_Displays.GetDescription();

            string concatenatedOptions = Game.OptionHandler.GetGeneralOptions();

            Console.Write(Room.GetCurrentItemsAndStats());
            Console.Write(L1_Displays.GetRoom(roomToDisplay)); Console.Write("\n" + concatenatedOptions + "\n\n");

            if (roomToDisplay == 1)
            {
                if (Room_DescriptionShown[roomToDisplay].Equals(false))
                {
                    // PlayerChoice ends when room description is shown and closed again
                    Room.PlayerChoice(Options.GeneralOptionsKeyBinds);

                    Room_DescriptionShown[roomToDisplay] = true;

                    DisplayRooms(roomToDisplay);

                }
                else
                {
                    concatenatedOptions = Game.OptionHandler.GetRoomExploreOptions();
                    Console.Write(concatenatedOptions + "\n\n");

                    int action = Room.PlayerChoice(Options.RoomExploreOptionsKeyBinds);

                    if (action <= R1_ActionCompleted.Count)  // A valid action (such as use door) is taken
                    {
                        if (R1_ActionCompleted[action].Equals(false))
                        {
                            try
                            {
                                if (R1_ActionRequiresItem[action] != null)
                                {
                                    if (Room.currentEquippedItem.Equals(R1_ActionRequiresItem[action]))
                                    {
                                        Game.CurrentPlayer.RemoveItemFromInventory(R1_ActionRequiresItem[action]);

                                        R1_ActionCompleted[action] = true;

                                        // Use door
                                        if (action.Equals(0))
                                        {
                                            L1_Displays.R1_Interactables[action] = Environment_Interactables.Open_DoorVertiacl;
                                        }

                                        L1_Actions.DoAction(action, false, 1);

                                    }
                                    else { L1_Actions.DoAction(action, true); }
                                }
                                else
                                {
                                    R1_ActionCompleted[action] = true;

                                    // Use Chest
                                    if (action.Equals(1))
                                    {
                                        L1_Displays.R1_Interactables[action] = Environment_Interactables.Open_Chest;
                                    }

                                    L1_Actions.DoAction(action, true);
                                }
                            }
                            catch { Debug.WriteLine("Selected option was out of bounds of the action options, and was caught."); }
                        }
                        else
                        {
                            // Use door
                            if (action.Equals(0))
                            {
                                Room.currentRoom++;

                                DisplayRooms(Room.currentRoom);
                            }

                        }
                    }

                    
                    // Open chest
                    else if (action.Equals(1))
                    {

                    }
                    // Look on table
                    else if (action.Equals(2))
                    {
                        Console.WriteLine("Tried door...");
                    }
                    // Look under table
                    else if (action.Equals(3))
                    {
                        Console.WriteLine("Tried door...");
                    }

                    DisplayRooms(roomToDisplay);
                }

            }
            else if (roomToDisplay == 2)
            {
                Console.WriteLine("222222222222222");
            }
        }

        public void Continue()
        {
            DisplayRooms(Room.currentRoom);
        }

    }
}
