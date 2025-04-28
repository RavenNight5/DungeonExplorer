// Filename: Player.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        /// <summary>
        /// Initialises the player's name, max health, health & gold coins.
        /// Handles the player's inventory, including:
        ///     - Picking up items
        ///     - Removing items from the inventory
        ///     - Calling on the Inventory class to display the content of the player's inventory
        /// </summary>
        public List<string> InventoryItems = new List<string>();  // Holds the current items the player has in their inventory

        public static int Health { get; set; }
        public static int BaseDamage { get; set; }
        public static int CRITDamage { get; set; }
        public static int CRITRate { get; set; }

        public static int GoldCoins { get; set; }

        public override bool IsPlayer => true;
        public override bool IsEnemy => false;
        public override bool IsNPC => false;

        public override string Name { get; set; }
        public override string Plural { get; set; }
        public override int MaxHealth { get; set; }

        public Player(string name, string plural, int maxHealth) : base(name, plural, maxHealth)
        {
            Name = name;
            Plural = plural;

            BaseDamage = 5;
            CRITDamage = 10;  // A % that determines the additional damage dealt if the attack is a CRIT HIT
            CRITRate = 15;  // A % chance out of 100 that the attack will be a CRIT HIT

            Health = maxHealth;

            GoldCoins = 10;
        }

        public void PickUpItem(string item)  //Passes the item to be added to the inventory - this is preset and passed from Inventory_Items (index 0 of the array is the item, index 1 is the description)
        {
            if (!InventoryItems.Contains(item))
            {
                InventoryItems.Add(item);  // Only adds the name of the item to be stored in the player object
            }
        }

        public void RemoveItemFromInventory(string item)
        {
            InventoryItems.Remove(item);  // Removes the name of the item from the player object inventory

            Inventory.InventorySlotNumbers = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", };  // Resets the inventory slot numbers (so none are shown as selected)

            if (item == Room.CurrentEquippedItem)  // item[1] is the "image" of the item and would correspond to the CurrentEquippedItem slot
            {
                Room.CurrentEquippedItem = "";
                Room.CurrentEquippedItemImage = Inventory.InventoryEmptySlot;
            }
        }

        // Uses the inventory class to display the passed list of items on a screen
        public void DisplayInventory(string currentlyChoosing = "")
        {
            Inventory inventory = new Inventory();

            inventory.DisplayInventory(InventoryItems, currentlyChoosing);
        }

        public override int Attack(bool miss = false, bool hitWeakSpot = false, List<int> availableDamage = null)
        {
            Program.CLEAR_CONSOLE();

            int result = 0;

            int baseDamage = 0;
            int critDamage = 0;
            int critRate = 0;
            int weakSpotDmg = 0;

            if (!(availableDamage == null) && miss == false)
            {
                baseDamage = availableDamage[0];
                critDamage = availableDamage[1];
                critRate = availableDamage[2];
                weakSpotDmg = availableDamage[3];

                // If the attack is a crit hit (from random chance of crit rate), add the base damage and crit damage
                Random critRateChance = new Random();

                if (critRateChance.Next(0, 100) <= critRate)  // CRIT Hit
                {
                    result = baseDamage + (baseDamage * (critDamage/100));  // Adds the percentage of crit damage to the base damage
                    
                    if (hitWeakSpot)
                    {
                        result = result + (result * (weakSpotDmg / 100));  // Adds the percentage of weak spot damage to the result damage
                        
                        Console.Write($@"
 WEAK SPOT CRIT HIT!
 [ {baseDamage} ] Weapon Base
 +[ {critDamage} ]% CRIT Dmg
  +[ {weakSpotDmg} ]% Weak Spot Dmg
 ---
 [ {result} ] Total Dmg

                        ");
                    }
                    else
                    {
                        Console.Write($@"
 CRIT HIT!
 [ {baseDamage} ] Weapon Base
 +[ {critDamage} ]% CRIT Dmg
 ---
 [ {result} ] Total Dmg

                        ");
                    }
                }
                else  // Regular hit
                {
                    result = baseDamage;

                    if (hitWeakSpot)
                    {
                        result = result + (result * (weakSpotDmg / 100));

                        Console.Write($@"
 WEAK SPOT HIT!
 [ {baseDamage} ] Weapon Base
  +[ {weakSpotDmg} ]% Weak Spot Dmg
 ---
 [ {result} ] Total Dmg

                        ");
                    }
                    else
                    {
                        Console.Write($@"
 HIT!
 [ {baseDamage} ] Weapon Base
 ---
 [ {result} ] Total Dmg

                        ");
                    }
                }

               
            }

           

            return result;
        }

        public override void DamagePlayer(int dmg)
        {
            Console.WriteLine($"'{Name}' took {dmg} damage.");
        }
    }
}