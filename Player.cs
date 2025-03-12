// Filename: Player.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonExplorer
{
    public class Player
    {
        /// <summary>
        /// Initialises the player's name, max health & health.
        /// Handles the inventory system (including its text display), including:
        ///     - Picking up items
        ///     - Removing items from the inventory
        ///     - Returning the inventory's text display
        ///     - Displaying the inventory's text display
        ///     - Allowing selection between each item in the inventory, showing their descriptions when selected
        ///     - Equipping an item from the inventory - which is then handled by the Room class
        /// </summary>
        private string[] _slotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };
        private string[] _itemDescription = new string[4];

        public static string Name { get; set; }
        public static string NamePlural { get; set; }
        public static int MaxHealth { get; set; }
        public static int Health { get; set; }

        public static string[] EmptySlot = {
        "       ",
        "       ",
        "       ",
        "       ",
        "      "
        };

        public static string[] EmptyDescription = {
        "",
        "",
        "",
        ""
        };

        private readonly List<string[]> _inventoryItem = new List<string[]>();
        private readonly List<string[]> _inventoryItem_Descriptions = new List<string[]>();
        
        private readonly string _emptyNormal = "       ";
        private readonly string _emptyBottom = "      ";  // There is an inventory slot number so have one less space

        private readonly string _selectedSlotChar = "+";

        private readonly string[] _slot_1 = new string[5];  // Each string in the array represents the horizontal line in that inventory slot
        private readonly string[] _slot_2 = new string[5];
        private readonly string[] _slot_3 = new string[5];
        private readonly string[] _slot_4 = new string[5];
        private readonly string[] _slot_5 = new string[5];
        private readonly string[] _slot_6 = new string[5];
        private readonly string[] _slot_7 = new string[5];
        private readonly string[] _slot_8 = new string[5];
        private readonly string[] _slot_9 = new string[5];
        private readonly string[] _slot_0 = new string[5];

        public Player()
        {
            MaxHealth = 6;
            Health = MaxHealth;

            _inventoryItem.Add(_slot_1);
            _inventoryItem.Add(_slot_2);
            _inventoryItem.Add(_slot_3);
            _inventoryItem.Add(_slot_4);
            _inventoryItem.Add(_slot_5);
            _inventoryItem.Add(_slot_6);
            _inventoryItem.Add(_slot_7);
            _inventoryItem.Add(_slot_8);
            _inventoryItem.Add(_slot_9);
            _inventoryItem.Add(_slot_0);

            for (int i = 0; i < _inventoryItem.Count; i++)  //For each inventory slot array - initialise each string to the width of the inventory slot
            {
                _inventoryItem[i][0] = _emptyNormal;
                _inventoryItem[i][1] = _emptyNormal;
                _inventoryItem[i][2] = _emptyNormal;
                _inventoryItem[i][3] = _emptyNormal;
                _inventoryItem[i][4] = _emptyBottom;

                _inventoryItem_Descriptions.Add(EmptyDescription);
            }
        }

        public void PickUpItem(string[][] item)  //Passes the item to be added to the inventory - this is preset and passed from Inventory_Items (index 0 of the array is the item, index 1 is the description)
        {
            bool itemAdded = false;

            for (int i = 0; i < _inventoryItem.Count; i++)
            {
                // Check if the current slot's middle line is equal to the empty slot string (as the middle line will always have text if an item is assigned to that slot)
                if (_inventoryItem[i][2].Equals(_emptyNormal) && itemAdded.Equals(false))
                {
                    itemAdded = true;

                    _inventoryItem[i] = item[0];
                    _inventoryItem_Descriptions[i] = item[1];
                }
            }
        }

        public void RemoveItemFromInventory(string[] item)
        {
            for (int i = 0; i < _inventoryItem.Count; i++)
            {
                if (_inventoryItem[i].Equals(item))
                {
                    _inventoryItem[i] = EmptySlot;
                    _itemDescription = EmptyDescription;
                    _inventoryItem_Descriptions[i] = EmptyDescription;
                }
            }

            _slotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };

            Room.CurrentEquippedItem = EmptySlot;
        }

        // Used to refresh the display after an item is selected etc.
        private string GetInventoryDisplay()
        {
            string inventoryDisplay = $@"
     Inventory:
    ---───══───═══════════════════───══───---  Description:
    │{_inventoryItem[0][0]}│{_inventoryItem[1][0]}│{_inventoryItem[2][0]}│{_inventoryItem[3][0]}│{_inventoryItem[4][0]}│ ╔══════=──────────---
    │{_inventoryItem[0][1]}│{_inventoryItem[1][1]}│{_inventoryItem[2][1]}│{_inventoryItem[3][1]}│{_inventoryItem[4][1]}│ ║ {_itemDescription[0]}
    │{_inventoryItem[0][2]}║{_inventoryItem[1][2]}║{_inventoryItem[2][2]}║{_inventoryItem[3][2]}║{_inventoryItem[4][2]}│ │ {_itemDescription[1]}
    │{_inventoryItem[0][3]}│{_inventoryItem[1][3]}│{_inventoryItem[2][3]}│{_inventoryItem[3][3]}│{_inventoryItem[4][3]}│ │ {_itemDescription[2]}
    ║{_slotNumbers[0]}{_inventoryItem[0][4]}│{_slotNumbers[1]}{_inventoryItem[1][4]}│{_slotNumbers[2]}{_inventoryItem[2][4]}│{_slotNumbers[3]}{_inventoryItem[3][4]}│{_slotNumbers[4]}{_inventoryItem[4][4]}║ │ {_itemDescription[3]}
    ║ ───────────────────────────────────── ║ ║ [Enter] to Equip/Use
    ║{_inventoryItem[5][0]}│{_inventoryItem[6][0]}│{_inventoryItem[7][0]}│{_inventoryItem[8][0]}│{_inventoryItem[9][0]}║ ╚══════=──────────---
    │{_inventoryItem[5][1]}│{_inventoryItem[6][1]}│{_inventoryItem[7][1]}│{_inventoryItem[8][1]}│{_inventoryItem[9][1]}│
    │{_inventoryItem[5][2]}║{_inventoryItem[6][2]}║{_inventoryItem[7][2]}║{_inventoryItem[8][2]}║{_inventoryItem[9][2]}│
    │{_inventoryItem[5][3]}│{_inventoryItem[6][3]}│{_inventoryItem[7][3]}│{_inventoryItem[8][3]}│{_inventoryItem[9][3]}│
    │{_slotNumbers[5]}{_inventoryItem[5][4]}│{_slotNumbers[6]}{_inventoryItem[6][4]}│{_slotNumbers[7]}{_inventoryItem[7][4]}│{_slotNumbers[8]}{_inventoryItem[8][4]}│{_slotNumbers[9]}{_inventoryItem[9][4]}│
    ---──────────═══════════════──────────---
        ";

            return inventoryDisplay;
        }

        public void DisplayInventory()
        {
            Program.CLEAR_CONSOLE();

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
                            Program.CLEAR_CONSOLE();

                            Game.RoomHandler.ReturnToLevel();

                        }
                        else if (optionChosen.Equals("Enter"))
                        {
                            Program.CLEAR_CONSOLE();

                            //To add:
                            //If selected and a useable item (health kit etc.) then use straight away and remove from inventory.

                            if (!_itemDescription[0].Equals(""))
                            {
                                int itemIndex = _inventoryItem_Descriptions.IndexOf(_itemDescription);

                                Room.CurrentEquippedItem = _inventoryItem[itemIndex];

                                Game.RoomHandler.ReturnToLevel();
                            }
                            else
                            {
                                Room.CurrentEquippedItem = EmptySlot;

                                Game.RoomHandler.ReturnToLevel();
                            }

                        }
                        else  // Player has chosen an item
                        {
                            Program.CLEAR_CONSOLE();

                            int slotChosen = Array.IndexOf(Options.InventoryOptionsKeyBinds, optionChosen);

                            if (_slotNumbers[slotChosen].ToString() != _selectedSlotChar)
                            {
                                _itemDescription = _inventoryItem_Descriptions[slotChosen];

                                _slotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };

                                _slotNumbers[slotChosen] = _selectedSlotChar;
                            }
                            else
                            {
                                _itemDescription = EmptyDescription;

                                if (slotChosen.Equals(9)) _slotNumbers[slotChosen] = "0";
                                else
                                {
                                    _slotNumbers[slotChosen] = (slotChosen + 1).ToString();
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