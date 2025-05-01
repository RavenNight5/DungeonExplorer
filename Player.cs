// Filename: Player.cs
using System;
using System.Collections.Generic;
using System.Threading;

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
        public static List<string> InventoryItems = new List<string>();  // Holds the current items the player has in their inventory

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

        private readonly string[] _d6Visuals = new string[] { "   ·\r\n", "    ·\r\n   ·", "     ·\r\n    ·\r\n   ·", "   · ·\r\n   · ·", "   · ·\r\n    ·\r\n   · ·", "   ···\r\n   ···" };

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

            float result = 0;

            int dice1 = DiceRoll();
            int dice2 = DiceRoll();

            if (!(availableDamage == null) && miss == false)
            {
                int baseDamage = availableDamage[0];
                int critDamage = availableDamage[1];
                int critRate = availableDamage[2];
                int weakSpotDmg = availableDamage[3];

                Random critRateChance = new Random();

                if (critRateChance.Next(0, 100) <= critRate)  // CRIT Hit  
                {
                    result = baseDamage + (baseDamage * critDamage / 100);  // Adds the percentage of crit damage to the base damage

                    if (hitWeakSpot)
                    {
                        result += (result * weakSpotDmg / 100);  // Adds the percentage of weak spot damage to the result damage

                        Console.WriteLine($" WEAK SPOT CRIT HIT!\n");
                        Thread.Sleep(300);
                        Console.WriteLine($" [ {baseDamage} ] Weapon Base Dmg");
                        Thread.Sleep(200);
                        Console.WriteLine($" +[ {critDamage} ]% CRIT Dmg");
                        Thread.Sleep(100);
                        Console.WriteLine($"  +[ {weakSpotDmg} ]% Weak Spot Dmg");
                        Thread.Sleep(50);
                        Console.WriteLine($" ---\n [ {(int)Math.Ceiling(result)} ] Total Dmg");
                    }
                    else
                    {
                        Console.WriteLine($" CRIT HIT!\n");
                        Thread.Sleep(200);
                        Console.WriteLine($" [ {baseDamage} ] Weapon Base Dmg");
                        Thread.Sleep(100);
                        Console.WriteLine($" +[ {critDamage} ]% CRIT Dmg");
                        Thread.Sleep(50);
                        Console.WriteLine($" ---\n [ {(int)Math.Ceiling(result)} ] Total Dmg");
                    }
                }
                else  // Regular hit  
                {
                    result = baseDamage;

                    if (hitWeakSpot)
                    {
                        result += (result * weakSpotDmg / 100);

                        Console.WriteLine($" WEAK SPOT HIT!\n");
                        Thread.Sleep(200);
                        Console.WriteLine($" [ {baseDamage} ] Weapon Base Dmg");
                        Thread.Sleep(100);
                        Console.WriteLine($"  +[ {weakSpotDmg} ]% Weak Spot Dmg");
                        Thread.Sleep(50);
                        Console.WriteLine($" ---\n [ {(int)Math.Ceiling(result)} ] Total Dmg");
                    }
                    else
                    {
                        Console.WriteLine($" HIT!\n");
                        Thread.Sleep(200);
                        Console.WriteLine($" [ {baseDamage} ] Weapon Base Dmg");
                        Thread.Sleep(100);
                        Console.WriteLine($" ---\n [ {(int)Math.Ceiling(result)} ] Total Dmg");
                    }
                }

                Console.WriteLine($"\n\n > Roll 2x D6 [Space]\n");

                Console.ReadKey();

                Program.CLEAR_CONSOLE();

                int totalResult = (int)Math.Ceiling(result + dice1 + dice2);

                string space = "   ";

                if (totalResult.ToString().Length == 2)
                {
                    space = "  ";
                }
                else if (totalResult.ToString().Length == 3)
                {
                    space = " ";
                }

                Thread.Sleep(100);
                Console.WriteLine($"{_d6Visuals[dice1 - 1]}");
                Thread.Sleep(200);
                Console.WriteLine($"{_d6Visuals[dice2 - 1]}");

                Thread.Sleep(200);
                Console.WriteLine($"\n  [ {dice1 + dice2} ] + DICE Damage\n");
                Thread.Sleep(100);
                Console.WriteLine($@" ╔═─~~─═╗
 │  {totalResult}{space}│  TOTAL Attack Dmg
 ╚═─~~─═╝");

                Console.WriteLine($"\n > Continue [Space]");

                Console.ReadKey();
            }

            return (int)Math.Ceiling(result + dice1 + dice2);
        }

        private int DiceRoll()
        {
            int result = 0;
            
            // Avoids repeated seed initialization (it wouldn't give me a unique random number with just the regular instance of random somehow)
            Random random = new Random(Guid.NewGuid().GetHashCode());

            result = random.Next(1, 7);  // Rolls a D6  

            return result;
        }

        public override void DamagePlayer(int dmg, int healthDefense = 0, bool lifeShield = false)
        {
            if (Health - dmg <= 0)
            {
                if (lifeShield)  // If the bonus item equipped is a life shield
                {
                    Health = (int)Math.Ceiling((double)(MaxHealth * healthDefense) / 100);  // If the player has a life shield they won't die and will be restored healthDefense as a % of their max health

                    Program.CLEAR_CONSOLE();

                    Console.WriteLine(" A CRITICAL blow was dealt, but you were saved by your Life Shield!\n");
                    Thread.Sleep(200);
                    Console.WriteLine($" {healthDefense}% of max health has been restored.\n");
                    Thread.Sleep(100);

                    Console.WriteLine($" Press [any key] to continue\n");
                    Console.ReadKey();
                }
                else
                {
                    Health = 0;
                }
            }
            else
            {
                if (lifeShield)
                {
                    Health -= dmg;
                    Health = Health + (int)Math.Ceiling((double)(MaxHealth * healthDefense) / 100);  // Add the health restored by the life shield even though the player is still alive (so the use was not in vein)

                    Program.CLEAR_CONSOLE();

                    Console.WriteLine(" No critical blow was dealt, you're still alive! Since a Life Shield is equipped:\n");
                    Thread.Sleep(200);
                    Console.WriteLine($" + {healthDefense}% of max health has been restored.\n");
                    Thread.Sleep(100);

                    Console.WriteLine($" Press [any key] to continue\n");
                    Console.ReadKey();
                }
                else
                {
                    if (healthDefense != 0)  // A shield is equipped
                    {
                        int shielded = (dmg * healthDefense / 100);
                        
                        Health -= dmg - shielded;  // Take off the shield percentage from the damage dealt

                        Program.CLEAR_CONSOLE();

                        Thread.Sleep(200);
                        Console.WriteLine($"Your Shield absorbed {shielded} Damage from the opponent!\n");
                        Thread.Sleep(100);

                        Console.WriteLine("Press [any key] to continue\n");
                        Console.ReadKey();
                    }
                    else
                    {
                        Health -= dmg;
                    }
                }
            }
        }
    }
}