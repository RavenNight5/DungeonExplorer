using System;
using System.Collections.Generic;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    public class Player
    {
        public static string Name { get; set; }
        public int Health { get; set; }

        private List<string[]> inventorySlots = new List<string[]>();

        private string[] itemSelected = new string[5];
        private string[] itemDescription = new string[4];

        public static string[] emptySlot = {
        "       ",
        "       ",
        "       ",
        "       ",
        "      "
        };

        private string emptyNormal = "       ";
        private string emptyBottom = "      ";  // As there is an inventory slot number so have one less space

        private string[] slot_1 = new string[5];  // Each string in the array represents the horizontal line in that inventory slot
        private string[] slot_2 = new string[5];
        private string[] slot_3 = new string[5];
        private string[] slot_4 = new string[5];
        private string[] slot_5 = new string[5];
        private string[] slot_6 = new string[5];
        private string[] slot_7 = new string[5];
        private string[] slot_8 = new string[5];
        private string[] slot_9 = new string[5];
        private string[] slot_0 = new string[5];


        public Player()
        {
            Health = 6;

            inventorySlots.Add(slot_1);
            inventorySlots.Add(slot_2);
            inventorySlots.Add(slot_3);
            inventorySlots.Add(slot_4);
            inventorySlots.Add(slot_5);
            inventorySlots.Add(slot_6);
            inventorySlots.Add(slot_7);
            inventorySlots.Add(slot_8);
            inventorySlots.Add(slot_9);
            inventorySlots.Add(slot_0);

            for (int i = 0; i < inventorySlots.Count; i++)  //For each inventory slot array - initialise each string to the width of the inventory slot
            {
                inventorySlots[i][0] = emptyNormal;
                inventorySlots[i][1] = emptyNormal;
                inventorySlots[i][2] = emptyNormal;
                inventorySlots[i][3] = emptyNormal;
                inventorySlots[i][4] = emptyBottom;
            }

            itemSelected = emptySlot;
        }

        public void PickUpItem(string[] item)  //Passes the item to be added to the inventory - this is preset and passed from Inventory_Items
        {
            bool itemAdded = false;

            for (int i = 0; i < inventorySlots.Count; i++)
            {
                // Check if the current slot's middle line is equal to the empty slot string (as the middle line will always have text if an item is assigned to that slot)
                if (inventorySlots[i][2].Equals(emptyNormal) && itemAdded.Equals(false))
                {
                    itemAdded = true;
                    inventorySlots[i] = item;
                }
            }
        }

        public void RemoveItemFromInventory(string[] item)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].Equals(item))
                {
                    inventorySlots[i] = emptySlot;
                }
            }
        }

        public void DisplayInventory()
        {
            Console.Clear();

            string inventoryDisplay = $@"
      Inventory:

    ---───══───═══════════════════───══───---  Description:
    │{inventorySlots[0][0]}│{inventorySlots[1][0]}│{inventorySlots[2][0]}│{inventorySlots[3][0]}│{inventorySlots[4][0]}│ ╔══════=──────────---
    │{inventorySlots[0][1]}│{inventorySlots[1][1]}│{inventorySlots[2][1]}│{inventorySlots[3][1]}│{inventorySlots[4][1]}│ ║ {itemDescription[0]}
    │{inventorySlots[0][2]}║{inventorySlots[1][2]}║{inventorySlots[2][2]}║{inventorySlots[3][2]}║{inventorySlots[4][2]}│ │ {itemDescription[1]}
    │{inventorySlots[0][3]}│{inventorySlots[1][3]}│{inventorySlots[2][3]}│{inventorySlots[3][3]}│{inventorySlots[4][3]}│ │ {itemDescription[2]}
    ║1{inventorySlots[0][4]}│2{inventorySlots[1][4]}│3{inventorySlots[2][4]}│4{inventorySlots[3][4]}│5{inventorySlots[4][4]}║ │ {itemDescription[3]}
    ║  -----------------------------------  ║ ║[enter] to equip/use
    ║{inventorySlots[5][0]}│{inventorySlots[6][0]}│{inventorySlots[7][0]}│{inventorySlots[8][0]}│{inventorySlots[9][0]}║ ╚══════=──────────---
    │{inventorySlots[5][1]}│{inventorySlots[6][1]}│{inventorySlots[7][1]}│{inventorySlots[8][1]}│{inventorySlots[9][1]}│
    │{inventorySlots[5][2]}║{inventorySlots[6][2]}║{inventorySlots[7][2]}║{inventorySlots[8][2]}║{inventorySlots[9][2]}│
    │{inventorySlots[5][3]}│{inventorySlots[6][3]}│{inventorySlots[7][3]}│{inventorySlots[8][3]}│{inventorySlots[9][3]}│
    │6{inventorySlots[5][4]}│7{inventorySlots[6][4]}│8{inventorySlots[7][4]}│9{inventorySlots[8][4]}│0{inventorySlots[9][4]}│
    ---──────────═══════════════──────────---
        ";


            Console.Write(inventoryDisplay); Console.WriteLine("\n\n" + Game.OptionHandler.GetInventoryOptions() + "\n");

            string optionChosen = Game.InputHandler.OptionsGetPlayerResponse(Options.InventoryOptionsKeyBinds);

            if (optionChosen.Equals("Tab"))
            {
                Console.Clear();

                Game.RoomHandler.ReturnToLevel();
                //Display room again
            }
            else if (optionChosen.Equals("Enter"))
            {
                Console.Clear();

                //If nothing selected then say - nothing sleceted
                //If selected and a useable item (health kit etc) then use straight away and remove from inventory
                //If selected and an EQUIpable item then display room and the item bottom right or somebtigh
                //Display room again + equipped /////////////

                Game.RoomHandler.ReturnToLevel(itemSelected);
            }
            else  // Player has chosen an item
            {
                //Change to display item description /////////////
                Console.Clear();

                Console.Write(inventoryDisplay); Console.WriteLine("\n\n" + Game.OptionHandler.GetInventoryOptions() + "\n");
            }
        }
    }
}