using System;
using System.Collections.Generic;
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
            Level_1.L1_Displays = new Level_1_Displays();
            Level_1.L1_Actions = new Level_1_Actions();
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

                int indexOfResponse = Game.InputHandler.OptionsGetPlayerResponse_CustomOptions(Options.GeneralOptionsKeyBinds, newOptions);

                //Inventory.AddItemToInventory(Inventory_Items.Key1);
                //Inventory.AddItemToInventory(Inventory_Items.CupFull);
                //Inventory.AddItemToInventory(Inventory_Items.CupEmpty);

                if (indexOfResponse == 0)
                {
                    Console.WriteLine("Desc...");  // Desc box
                }
                else if (indexOfResponse == 1)
                {
                    Console.WriteLine("Get stats...");
                }
                else if (indexOfResponse == 2)
                {
                    Game.CurrentPlayer.DisplayInventory();
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
