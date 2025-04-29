using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer.Item_Types;

namespace DungeonExplorer
{
    public class Item
    {
        /// <summary>
        /// Uses dynamic polymorphism to create the subclass objects of Items  ////////////////////////////////////
        /// These objects then assign the item string arrays to a list then that list to All_Items
        /// </summary>

        public static List<List<string[][]>> AllItems = new List<List<string[][]>>();  // AllItems[index] where index directly correlates to ItemTypeIndex

        public static readonly string[] ItemTypeIndex = new string[] { "Weapon", "Bonus Item" };

        public static string[] GetItemStats(string item)
        {
            foreach (var itemType in AllItems)
            {
                for (var i = 0; i < itemType.Count; i++)
                {
                    if (itemType[i][0][0] == item)
                    {
                        return itemType[i][0];
                    }
                }
            }

            return null;
        }

        public static int GetItemUses(string item)
        {
            int defaultUses = 0;

            if (item == null || item == "")
            {
                return defaultUses;
            }
            else
            {
                try
                {
                    defaultUses = GetItemStats(item)[2] == "" ? 0 : int.Parse(GetItemStats(item)[2]);  // If uses is empty set it as 0 (infinite uses), otherwise parse the available string to an int
                }
                catch
                {
                    Debug.WriteLine("Trying to get stats, or an item, that does not exist.");
                }
            }
            
            return defaultUses;
        }

        // Finds the corresponding item image from the single string name stored in the player's inventory
        public static string[] GetItemImage(string item)
        {
            foreach (var itemType in AllItems)
            {
                for (var i = 0; i < itemType.Count; i++)
                {
                    if (itemType[i][0][0] == item)
                    {
                        return itemType[i][1];
                    }
                }
            }

            return null;
        }


        public static string GetItemNameFromImage(string[] item)
        {
            foreach (var itemType in AllItems)
            {
                for (var i = 0; i < itemType.Count; i++)
                {
                    if (itemType[i][1] == item)
                    {
                        return itemType[i][0][0];
                    }
                }
            }

            return null;
        }

        public static string[] GetItemDescription(string item)
        {

            foreach (var itemType in AllItems)
            {
                for (var i = 0; i < itemType.Count; i++)
                {
                    if (itemType[i][0][0] == item)
                    {
                        return itemType[i][2];
                    }
                }
            }

            return null;
        }

        public static string GetItemType(string item)
        {

            for (int i = 0; i < AllItems.Count; i++)
            {
                foreach (var newItem in AllItems[i])
                {
                    if (newItem[0][0] == item)
                    {
                        return ItemTypeIndex[i];
                    }
                }
            }

            return null;
        }

        public static List<string> GetItemSpecialEffects(string item)
        {
            if (item == null || item == "")
            {
                return null;
            }
            else
            {
                for (int i = 0; i < AllItems.Count; i++)
                {
                    foreach (var newItem in AllItems[i])
                    {
                        if (newItem[0][0] == item)
                        {
                            if (newItem[0][7] == "")
                            {
                                return null;  // No special effects
                            }
                            else
                            {
                                return newItem[0][7].Split(' ').ToList();  // Returns a list of each effect written in the string (e.g. "Shield 20" will return as ["Shield", "20"])
                            }
                        }
                    }
                }
            }

            return null;
        }

    }
}
