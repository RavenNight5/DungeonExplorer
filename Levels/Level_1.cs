using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer.Levels
{
    internal class Level_1
    {
        private Options options = new Options();
        private Input input = new Input();

        private Inventory inventory = new Inventory();

        //private Level_1_Actions actions = new Level_1_Actions();
        private Level_1_Displays displays = new Level_1_Displays();

        public Level_1() { }

        public void Start()
        {
            int[] newOptions = { 0, 1, 2 };

            string concatenatedOptions = options.GetGeneralOptions(newOptions);

            Console.Write(displays.GetRoom(1) + "\n" + concatenatedOptions + "\n\n");

            int indexOfResponse = input.GeneralOptionsGetPlayerResponse(Options.GeneralOptionsKeyBinds, newOptions);

            inventory.AddItemToInventory(Inventory_Items.Key1);
            inventory.AddItemToInventory(Inventory_Items.CupFull);
            inventory.AddItemToInventory(Inventory_Items.CupEmpty);

            if (indexOfResponse == 0)
            {
                Console.WriteLine("Desc...");  // Desc box
            }
            else if (indexOfResponse == 1)
            {
                string inv = inventory.GetInventory(); Console.Clear(); Console.Write(inv);
            }
            else if (indexOfResponse == 2)
            {
                Console.WriteLine("Get stats...");
            }
        }

        public string GetDescription()
        {
            return "";
        }
    }
}
