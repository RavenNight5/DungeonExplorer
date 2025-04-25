// Filename: Player.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    public class Player : Creature, IDamageable
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
        public List<string> _inventoryItems = new List<string>();  // Holds the current items the player has in their inventory

        public static string NamePlural { get; set; }
        public static int Health { get; set; }

        public static int GoldCoins { get; set; }

        public override bool IsPlayer => true;
        public override bool IsEnemy => false;
        public override bool IsNPC => false;

        public override string Name { get; set; }
        public override int MaxHealth { get; set; }

        public Player(string name, int maxHealth) : base(name, maxHealth)
        {
            Health = maxHealth;

            GoldCoins = 10;

            
            //_inventoryItems = _slots;
        }

        public void PickUpItem(string item)  //Passes the item to be added to the inventory - this is preset and passed from Inventory_Items (index 0 of the array is the item, index 1 is the description)
        {
            _inventoryItems.Add(item);  // Only adds the name of the item to be stored in the player object

            //###############
            //For when the inventory is displayed - use this to fill the slots with the contents of _inventoryitems
            //bool itemAdded = false;

            //for (int i = 0; i < _inventoryItems.Count; i++)
            //{
            //    // Check if the current slot's middle line is equal to the empty slot string (as the middle line will always have text if an item is assigned to that slot)
            //    if (_inventoryItems[i][2].Equals(_emptyNormal) && itemAdded.Equals(false))
            //    {
            //        itemAdded = true;  // Ensure the item is only added once

            //        _inventoryItems[i] = item[0];
            //        _inventoryItems_Descriptions[i] = item[1];
            //    }
            //}
        }

        public void RemoveItemFromInventory(string item)
        {
            //for (int i = 0; i < _inventoryItems.Count; i++)
            //{
            //    if (_inventoryItems[i].Equals(item))
            //    {
            //        _inventoryItems[i] = Inventory.InventoryEmptySlot;
            //        Inventory.InventoryItemDescription = Inventory.InventoryEmptyDescription;
            //        _inventoryItems_Descriptions[i] = Inventory.InventoryEmptyDescription;
            //    }
            //}

            _inventoryItems.Remove(item);  // Removes the name of the item from the player object inventory

            Inventory.InventorySlotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };  // Resets the inventory slot numbers (so none are shown as selected)

            if (item == Room.CurrentEquippedItem)  // item[1] is the "image" of the item and would correspond to the CurrentEquippedItem slot
            {
                Room.CurrentEquippedItem = "";
                Room.CurrentEquippedItemImage = Inventory.InventoryEmptySlot;
            }
        }

        // Uses the inventory class to display the passed list of items on a screen
        public void DisplayInventory(bool inCombat = false, string currentlyChoosing = "")
        {
            Inventory inventory = new Inventory();

            inventory.DisplayInventory(_inventoryItems, inCombat, currentlyChoosing);
        }

        //public override void Attack()
        //{
        //    throw new NotImplementedException();
        //}

        //public override void PassTurn()
        //{
        //    throw new NotImplementedException();
        //}

        //public override void EquipItem()
        //{
        //    throw new NotImplementedException();
        //}

        //public override void AccessInventory()
        //{
        //    throw new NotImplementedException();
        //}
    }
}