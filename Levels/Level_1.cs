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


        public Level_1()
        { 
            L1_Displays = new Level_1_Displays();
            L1_Actions = new Level_1_Actions();

            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Sponge);
            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_DustpanBrush);
            Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Mop);

            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Key1);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Longsword);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_Dagger);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupEmpty);
            //Game.CurrentPlayer.PickUpItem(Inventory_Items.II_CupFull);
        }

        public void Start()
        {
            DisplayRooms(1);
        }

        public void DisplayRooms(int roomToDisplay, string[] itemToEquip = null)
        {
            if (roomToDisplay == 1)
            {
                Console.Write(L1_Displays.GetRoom(roomToDisplay) + "\n");

                if (itemToEquip != null && itemToEquip != Player.emptySlot)
                {
                    //Display item in a box
                }

                int[] newOptions = { 0, 1, 2 };
                string concatenatedOptions = Game.OptionHandler.GetGeneralOptions(newOptions);

                Console.Write("\n" + concatenatedOptions + "\n\n");

                PlayerChoice();

                void PlayerChoice()
                {
                    string optionChosen = Game.InputHandler.OptionsGetPlayerResponse(Options.GeneralOptionsKeyBinds);

                    if (optionChosen != null)
                    {
                        try
                        {
                            if (optionChosen.Equals(Options.GeneralOptionsKeyBinds[0]))
                            {
                                Console.WriteLine("Desc...");  // Desc box
                            }
                            else if (optionChosen.Equals(Options.GeneralOptionsKeyBinds[1]))
                            {
                                Console.WriteLine("Get stats...");
                            }
                            else if (optionChosen.Equals(Options.GeneralOptionsKeyBinds[2]))
                            {
                                Game.CurrentPlayer.DisplayInventory();
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(optionChosen + " was not recognised as a string in this instance. \nException caught: " + e);
                        }
                    }
                    else
                    {
                        PlayerChoice();
                    }
                }
            }
        }

        public void Continue(string[] itemToEquip = null)
        {
            DisplayRooms(Room.currentRoom, itemToEquip);
        }

        public string GetDescription()
        {
            return "";
        }
    }
}
