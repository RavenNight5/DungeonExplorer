// Filename: Inventory.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DungeonExplorer
{
    public class Inventory
    {
        /// <summary>
        /// The Inventory class takes a list of strings and displays them in a visual grid for the user to select an item.
        /// The functionalities are as follows:
        ///     - Allows selection between each item in the grid, showing their descriptions when selected
        ///     
        ///     For the Regular Inventory (not in combat):
        ///     - Player can equip an item from the grid - which is then displayed on the main game screen (where the room is shown)
        ///     
        ///     For the Combat Inventory:
        ///     - Player can equip an item from the grid:
        ///         - If selecting a weapon then only weapons are shown (LINQ) & the combat equipped weapon is set
        ///         - If selecting a bonus item then only bonus items are shown (LINQ) & the combat equipped bonus item is set
        ///     
        /// </summary>
        private string selectedSlotChar = "+";

        public static string[] InventorySlotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        public static string[] InventoryItemDescription = new string[4];  // Assigned values only when an item is selected

        public static string[] InventoryEmptySlot = {
        "       ",
        "       ",
        "       ",
        "       ",
        "      "
        };

        public static string[] InventoryEmptyDescription = {
        "",
        "",
        "",
        ""
        };

        private readonly List<string[]> _slots = new List<string[]>();
        private readonly List<string[]> _descriptionSlots = new List<string[]>();

        private readonly string _emptyNormal = "       ";
        private readonly string _emptyBottom = "      ";  // There is an inventory slot number so have one less space

        // Used to refresh the display after an item is selected, filters the list based on the combat selection
        private string GetInventoryDisplay(List<string> inventoryItems, string currentlyChoosing = "")
        {
            List<string> newInventoryItems = Player.InventoryItems;

            // Filter out all items that are not of type currentlyChoosing by creating a new list
            if (!string.IsNullOrEmpty(currentlyChoosing))
            {
                newInventoryItems = GetAllItemOfType(Player.InventoryItems, currentlyChoosing);

                // Sort the items in alphabetical order  
                newInventoryItems = newInventoryItems.OrderBy(n => n).ToList();
            }

            if (_slots.Count <= 0)
            {
                for (int i = 0; i < 10; i++)  // For each slot in the inventory
                {
                    _slots.Add(new string[5]);  // Add an empty slot for each space in the inventory - each string in the array represents the line
                                                // of whitespace in that displayable inventory slot

                    _descriptionSlots.Add(new string[4]);
                }

                for (int i = 0; i < _slots.Count; i++)  //For each inventory slot array - initialise each string to the width of the inventory slot
                {
                    _slots[i][0] = _emptyNormal;
                    _slots[i][1] = _emptyNormal;
                    _slots[i][2] = _emptyNormal;
                    _slots[i][3] = _emptyNormal;
                    _slots[i][4] = _emptyBottom;
                }

                for (int i = 0; i < newInventoryItems.Count; i++)
                {
                    _slots[i] = Item.GetItemImage(newInventoryItems[i]);
                    _descriptionSlots[i] = Item.GetItemDescription(newInventoryItems[i]);
                }
            }

            string inventoryDisplay = $@"
     Inventory:

    ---───══───═══════════════════───══───---  Description:
    │{_slots[0][0]}│{_slots[1][0]}│{_slots[2][0]}│{_slots[3][0]}│{_slots[4][0]}│ ╔══════=──────────---
    │{_slots[0][1]}│{_slots[1][1]}│{_slots[2][1]}│{_slots[3][1]}│{_slots[4][1]}│ ║ {InventoryItemDescription[0]}
    │{_slots[0][2]}║{_slots[1][2]}║{_slots[2][2]}║{_slots[3][2]}║{_slots[4][2]}│ │ {InventoryItemDescription[1]}
    │{_slots[0][3]}│{_slots[1][3]}│{_slots[2][3]}│{_slots[3][3]}│{_slots[4][3]}│ │ {InventoryItemDescription[2]}
    ║{InventorySlotNumbers[0]}{_slots[0][4]}│{InventorySlotNumbers[1]}{_slots[1][4]}│{InventorySlotNumbers[2]}{_slots[2][4]}│{InventorySlotNumbers[3]}{_slots[3][4]}│{InventorySlotNumbers[4]}{_slots[4][4]}║ │ {InventoryItemDescription[3]}
    ║ ───────────────────────────────────── ║ ║ [Enter] to Equip/Use
    ║{_slots[5][0]}│{_slots[6][0]}│{_slots[7][0]}│{_slots[8][0]}│{_slots[9][0]}║ ╚══════=──────────---
    │{_slots[5][1]}│{_slots[6][1]}│{_slots[7][1]}│{_slots[8][1]}│{_slots[9][1]}│
    │{_slots[5][2]}║{_slots[6][2]}║{_slots[7][2]}║{_slots[8][2]}║{_slots[9][2]}│
    │{_slots[5][3]}│{_slots[6][3]}│{_slots[7][3]}│{_slots[8][3]}│{_slots[9][3]}│
    │{InventorySlotNumbers[5]}{_slots[5][4]}│{InventorySlotNumbers[6]}{_slots[6][4]}│{InventorySlotNumbers[7]}{_slots[7][4]}│{InventorySlotNumbers[8]}{_slots[8][4]}│{InventorySlotNumbers[9]}{_slots[9][4]}│
    ---──────────═══════════════──────────---

        ";
            //                < Q  Content  E >
          
            return inventoryDisplay;
        }

        public void DisplayInventory(List<string> inventoryItems, string currentlyChoosing = "", bool incorrectInput = false)
        {
            Program.CLEAR_CONSOLE();

            if (currentlyChoosing != "")  // Accessing inventory from the combat screen
            {
                // reset the currently selected item due to filtering
                InventorySlotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                InventoryItemDescription = new string[4];
            }

            Console.Write(GetInventoryDisplay(Player.InventoryItems, currentlyChoosing)); Console.WriteLine("\n\n" + Game.OptionHandler.GetInventoryOptions() + "\n");
            
            if (incorrectInput)
            {
                Console.WriteLine($"You are currently choosing a {currentlyChoosing} to equip for combat. Make sure your selection matches the {currentlyChoosing} type.");
            }

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
                            if (Combat.InCombat == false)
                            {
                                Program.CLEAR_CONSOLE();

                                Game.RoomHandler.ReturnToLevel();
                            }
                            else
                            {
                                Program.CLEAR_CONSOLE();

                                Game.ReturnToCombat();
                            }
                        }
                        else if (optionChosen.Equals("Enter"))
                        {
                            Program.CLEAR_CONSOLE();
                            //To add:
                            //If selected and a useable item (health kit etc.) then use straight away and remove from inventory.

                            if (!(InventoryItemDescription[0] == null || InventoryItemDescription[0] == ""))  // If not no item selected
                            {
                                int itemIndex = _descriptionSlots.IndexOf(InventoryItemDescription);  // Get index of currently selected item

                                if (Combat.InCombat == false)
                                {
                                    Room.CurrentEquippedItem = Item.GetItemNameFromImage(_slots[itemIndex]);  // GetItemNameAndTypeFromImage returns a string[] where index 0 = item name, index 1 = item type
                                    Room.CurrentEquippedItemImage = _slots[itemIndex];

                                    Game.RoomHandler.ReturnToLevel();
                                }
                                else
                                {
                                    if (currentlyChoosing == "Weapon")
                                    {
                                        Combat.Combat_EquippedWeapon = Item.GetItemNameFromImage(_slots[itemIndex]);
                                        Combat.Combat_EquippedWeaponImage = _slots[itemIndex];

                                        Game.ReturnToCombat();  // Return to the main combat screen which will update the slots
                                    }
                                    else if (currentlyChoosing == "Bonus Item")
                                    {
                                        Combat.Combat_EquippedBonus = Item.GetItemNameFromImage(_slots[itemIndex]);
                                        Combat.Combat_EquippedBonusImage = _slots[itemIndex];

                                        Game.ReturnToCombat();
                                    }
                                    else
                                    {
                                        DisplayInventory(Player.InventoryItems, currentlyChoosing, true);
                                    }
                                }
                               
                            }
                            else  // If a blank slot is chosen to be equipped then remove the items from their slots
                            {
                                if (Combat.InCombat == false)
                                {
                                    Room.CurrentEquippedItem = "";
                                    Room.CurrentEquippedItemImage = InventoryEmptySlot;

                                    Game.RoomHandler.ReturnToLevel();
                                }
                                else
                                {
                                    if (currentlyChoosing == "Weapon")
                                    {
                                        Combat.Combat_EquippedWeapon = "";
                                        Combat.Combat_EquippedWeaponImage = InventoryEmptySlot;

                                        Game.ReturnToCombat();
                                    }
                                    else if (currentlyChoosing == "Bonus Item")
                                    {
                                        Combat.Combat_EquippedBonus = "";
                                        Combat.Combat_EquippedBonusImage = InventoryEmptySlot;

                                        Game.ReturnToCombat();
                                    }
                                    else
                                    {
                                        DisplayInventory(Player.InventoryItems, currentlyChoosing, true);
                                    }
                                }
                            }

                        }
                        else  // Player has chosen an item
                        {
                            Program.CLEAR_CONSOLE();

                            int slotChosen = Array.IndexOf(Options.InventoryOptionsKeyBinds, optionChosen);

                            if (InventorySlotNumbers[slotChosen].ToString() != selectedSlotChar)
                            {
                                InventoryItemDescription = _descriptionSlots[slotChosen];

                                InventorySlotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

                                InventorySlotNumbers[slotChosen] = selectedSlotChar;
                            }
                            else
                            {
                                InventoryItemDescription = InventoryEmptyDescription;

                                if (slotChosen.Equals(9)) InventorySlotNumbers[slotChosen] = "0";
                                else
                                {
                                    InventorySlotNumbers[slotChosen] = (slotChosen + 1).ToString();
                                }
                            }

                            Console.Write(GetInventoryDisplay(Player.InventoryItems, currentlyChoosing)); Console.WriteLine("\n\n" + Game.OptionHandler.GetInventoryOptions() + "\n");

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

        // Returns a list of all items in the player's inventory that match the current slelection type e.g. "Weapon"
        private static List<string> GetAllItemOfType(List<string> inventoryItems, string itemType)
        {
            List<string> result = new List<string>();

            // For each item in the inventory, if the item matches the item that the player is looking for (e.g. "Weapon") then add it to a new list of strings
            foreach (string item in Player.InventoryItems)
            {
                string thisItemType = Item.GetItemType(item);

                if (thisItemType == itemType)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
