using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    public class Player
    {
        public static string Name { get; set; }
        public static string NamePlural { get; set; }
        public static int MaxHealth { get; set; }
        public static int Health { get; set; }

        private List<string[]> inventoryItem = new List<string[]>();
        private List<string[]> inventoryItem_Descriptions = new List<string[]>();

        private string[] itemSelected = new string[5];
        private string[] itemDescription = new string[4];

        public static string[] emptySlot = {
        "       ",
        "       ",
        "       ",
        "       ",
        "      "
        };

        public static string[] emptyDescription = {
        "",
        "",
        "",
        ""
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

        private string selectedSlotChar = "+";

        private string[] slotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };

        public Player()
        {
            MaxHealth = 6;
            Health = MaxHealth;

            inventoryItem.Add(slot_1);
            inventoryItem.Add(slot_2);
            inventoryItem.Add(slot_3);
            inventoryItem.Add(slot_4);
            inventoryItem.Add(slot_5);
            inventoryItem.Add(slot_6);
            inventoryItem.Add(slot_7);
            inventoryItem.Add(slot_8);
            inventoryItem.Add(slot_9);
            inventoryItem.Add(slot_0);

            for (int i = 0; i < inventoryItem.Count; i++)  //For each inventory slot array - initialise each string to the width of the inventory slot
            {
                inventoryItem[i][0] = emptyNormal;
                inventoryItem[i][1] = emptyNormal;
                inventoryItem[i][2] = emptyNormal;
                inventoryItem[i][3] = emptyNormal;
                inventoryItem[i][4] = emptyBottom;

                inventoryItem_Descriptions.Add(emptyDescription);
            }

            itemSelected = emptySlot;
        }

        public void PickUpItem(string[][] item)  //Passes the item to be added to the inventory - this is preset and passed from Inventory_Items (index 0 of the array is the item, index 1 is the description)
        {
            bool itemAdded = false;

            for (int i = 0; i < inventoryItem.Count; i++)
            {
                // Check if the current slot's middle line is equal to the empty slot string (as the middle line will always have text if an item is assigned to that slot)
                if (inventoryItem[i][2].Equals(emptyNormal) && itemAdded.Equals(false))
                {
                    itemAdded = true;

                    inventoryItem[i] = item[0];
                    inventoryItem_Descriptions[i] = item[1];
                }
            }
        }

        public void RemoveItemFromInventory(string[] item)
        {
            for (int i = 0; i < inventoryItem.Count; i++)
            {
                if (inventoryItem[i].Equals(item))
                {
                    inventoryItem[i] = emptySlot;
                    itemDescription = emptyDescription;
                    inventoryItem_Descriptions[i] = emptyDescription;
                }
            }

            slotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };

            Room.currentEquippedItem = emptySlot;
        }

        // Used to refresh the display after an item is selected etc.
        private string GetInventoryDisplay()
        {
            string inventoryDisplay = $@"
     Inventory:
    ---───══───═══════════════════───══───---  Description:
    │{inventoryItem[0][0]}│{inventoryItem[1][0]}│{inventoryItem[2][0]}│{inventoryItem[3][0]}│{inventoryItem[4][0]}│ ╔══════=──────────---
    │{inventoryItem[0][1]}│{inventoryItem[1][1]}│{inventoryItem[2][1]}│{inventoryItem[3][1]}│{inventoryItem[4][1]}│ ║ {itemDescription[0]}
    │{inventoryItem[0][2]}║{inventoryItem[1][2]}║{inventoryItem[2][2]}║{inventoryItem[3][2]}║{inventoryItem[4][2]}│ │ {itemDescription[1]}
    │{inventoryItem[0][3]}│{inventoryItem[1][3]}│{inventoryItem[2][3]}│{inventoryItem[3][3]}│{inventoryItem[4][3]}│ │ {itemDescription[2]}
    ║{slotNumbers[0]}{inventoryItem[0][4]}│{slotNumbers[1]}{inventoryItem[1][4]}│{slotNumbers[2]}{inventoryItem[2][4]}│{slotNumbers[3]}{inventoryItem[3][4]}│{slotNumbers[4]}{inventoryItem[4][4]}║ │ {itemDescription[3]}
    ║ ───────────────────────────────────── ║ ║ [Enter] to Equip/Use
    ║{inventoryItem[5][0]}│{inventoryItem[6][0]}│{inventoryItem[7][0]}│{inventoryItem[8][0]}│{inventoryItem[9][0]}║ ╚══════=──────────---
    │{inventoryItem[5][1]}│{inventoryItem[6][1]}│{inventoryItem[7][1]}│{inventoryItem[8][1]}│{inventoryItem[9][1]}│
    │{inventoryItem[5][2]}║{inventoryItem[6][2]}║{inventoryItem[7][2]}║{inventoryItem[8][2]}║{inventoryItem[9][2]}│
    │{inventoryItem[5][3]}│{inventoryItem[6][3]}│{inventoryItem[7][3]}│{inventoryItem[8][3]}│{inventoryItem[9][3]}│
    │{slotNumbers[5]}{inventoryItem[5][4]}│{slotNumbers[6]}{inventoryItem[6][4]}│{slotNumbers[7]}{inventoryItem[7][4]}│{slotNumbers[8]}{inventoryItem[8][4]}│{slotNumbers[9]}{inventoryItem[9][4]}│
    ---──────────═══════════════──────────---
        ";

            return inventoryDisplay;
        }

        public void DisplayInventory()
        {
            Console.Clear();

            Console.Write(GetInventoryDisplay()); Console.WriteLine("\n\n" + Game.OptionHandler.GetInventoryOptions() + "\n");

            PlayerChoiceInventory();

            void PlayerChoiceInventory()
            {
                string optionChosen = Game.InputHandler.OptionsGetPlayerResponse(Options.InventoryOptionsKeyBinds);

                if (optionChosen != null)
                {
                    try
                    {
                        if (optionChosen.Equals("Tab"))
                        {
                            Console.Clear();

                            Game.RoomHandler.ReturnToLevel();

                        }
                        else if (optionChosen.Equals("Enter"))
                        {
                            Console.Clear();

                            //To add:
                            //If selected and a useable item (health kit etc.) then use straight away and remove from inventory.

                            if (!itemDescription[0].Equals(""))
                            {
                                int itemIndex = inventoryItem_Descriptions.IndexOf(itemDescription);

                                Room.currentEquippedItem = inventoryItem[itemIndex];

                                Game.RoomHandler.ReturnToLevel();
                            }
                            else
                            {
                                Room.currentEquippedItem = emptySlot;

                                Game.RoomHandler.ReturnToLevel();
                            }

                        }
                        else  // Player has chosen an item
                        {
                            Console.Clear();

                            int slotChosen = Array.IndexOf(Options.InventoryOptionsKeyBinds, optionChosen);

                            if (slotNumbers[slotChosen].ToString() != selectedSlotChar)
                            {
                                itemDescription = inventoryItem_Descriptions[slotChosen];

                                slotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };

                                slotNumbers[slotChosen] = selectedSlotChar;
                            }
                            else
                            {
                                itemDescription = emptyDescription;

                                if (slotChosen.Equals(9)) slotNumbers[slotChosen] = "0";
                                else
                                {
                                    slotNumbers[slotChosen] = (slotChosen + 1).ToString();
                                }
                            }

                            Console.Write(GetInventoryDisplay()); Console.WriteLine("\n\n" + Game.OptionHandler.GetInventoryOptions() + "\n");

                            PlayerChoiceInventory();

                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(optionChosen + " was not recognised as a string in this instance. \nException caught: " + e);
                    }
                }
                else PlayerChoiceInventory();
            }
            
        }
    }
}