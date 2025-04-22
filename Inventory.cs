using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Inventory
    {

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

        private string _selectedSlotChar = "+";

        
        // Used to refresh the display after an item is selected etc.
        private string GetInventoryDisplay(List<string> inventoryItems)
        {
            //#######################
            // use the _slots where for each inventoryItem , find the corresponding Weapon or other item and assign it's image to the slot

            string inventoryDisplay = $@"
     Inventory:

    ---───══───═══════════════════───══───---  Description:
    │{inventoryItems[0][0]}│{inventoryItems[1][0]}│{inventoryItems[2][0]}│{inventoryItems[3][0]}│{inventoryItems[4][0]}│ ╔══════=──────────---
    │{inventoryItems[0][1]}│{inventoryItems[1][1]}│{inventoryItems[2][1]}│{inventoryItems[3][1]}│{inventoryItems[4][1]}│ ║ {InventoryItemDescription[0]}
    │{inventoryItems[0][2]}║{inventoryItems[1][2]}║{inventoryItems[2][2]}║{inventoryItems[3][2]}║{inventoryItems[4][2]}│ │ {InventoryItemDescription[1]}
    │{inventoryItems[0][3]}│{inventoryItems[1][3]}│{inventoryItems[2][3]}│{inventoryItems[3][3]}│{inventoryItems[4][3]}│ │ {InventoryItemDescription[2]}
    ║{InventorySlotNumbers[0]}{inventoryItems[0][4]}│{InventorySlotNumbers[1]}{inventoryItems[1][4]}│{InventorySlotNumbers[2]}{inventoryItems[2][4]}│{InventorySlotNumbers[3]}{inventoryItems[3][4]}│{InventorySlotNumbers[4]}{inventoryItems[4][4]}║ │ {InventoryItemDescription[3]}
    ║ ───────────────────────────────────── ║ ║ [Enter] to Equip/Use
    ║{inventoryItems[5][0]}│{inventoryItems[6][0]}│{inventoryItems[7][0]}│{inventoryItems[8][0]}│{inventoryItems[9][0]}║ ╚══════=──────────---
    │{inventoryItems[5][1]}│{inventoryItems[6][1]}│{inventoryItems[7][1]}│{inventoryItems[8][1]}│{inventoryItems[9][1]}│
    │{inventoryItems[5][2]}║{inventoryItems[6][2]}║{inventoryItems[7][2]}║{inventoryItems[8][2]}║{inventoryItems[9][2]}│
    │{inventoryItems[5][3]}│{inventoryItems[6][3]}│{inventoryItems[7][3]}│{inventoryItems[8][3]}│{inventoryItems[9][3]}│
    │{InventorySlotNumbers[5]}{inventoryItems[5][4]}│{InventorySlotNumbers[6]}{inventoryItems[6][4]}│{InventorySlotNumbers[7]}{inventoryItems[7][4]}│{InventorySlotNumbers[8]}{inventoryItems[8][4]}│{InventorySlotNumbers[9]}{inventoryItems[9][4]}│
    ---──────────═══════════════──────────---

        ";
            //                < Q  Content  E >
            return inventoryDisplay;
        }

        public void DisplayInventory(List<string> inventoryItems, List<string[]> inventoryItemDescriptions)
        {
            Program.CLEAR_CONSOLE();

            Console.Write(GetInventoryDisplay(inventoryItems)); Console.WriteLine("\n\n" + Game.OptionHandler.GetInventoryOptions() + "\n");

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

                            if (!InventoryItemDescription[0].Equals(""))
                            {
                                int itemIndex = inventoryItemDescriptions.IndexOf(InventoryItemDescription);

                                //#####################Room.CurrentEquippedItem = inventoryItems[itemIndex];

                                Game.RoomHandler.ReturnToLevel();
                            }
                            else
                            {
                                Room.CurrentEquippedItem = InventoryEmptySlot;

                                Game.RoomHandler.ReturnToLevel();
                            }

                        }
                        else  // Player has chosen an item
                        {
                            Program.CLEAR_CONSOLE();

                            int slotChosen = Array.IndexOf(Options.InventoryOptionsKeyBinds, optionChosen);

                            if (InventorySlotNumbers[slotChosen].ToString() != _selectedSlotChar)
                            {
                                InventoryItemDescription = inventoryItemDescriptions[slotChosen];

                                InventorySlotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };

                                InventorySlotNumbers[slotChosen] = _selectedSlotChar;
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

                            Console.Write(GetInventoryDisplay(inventoryItems)); Console.WriteLine("\n\n" + Game.OptionHandler.GetInventoryOptions() + "\n");

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
