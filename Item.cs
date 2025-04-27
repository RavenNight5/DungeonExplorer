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

        // Finds the corresponding item image from the single string name stored in the player's inventory
        public static string[] GetItemImage(string item)
        {
            //foreach (string stats in itemStats[6].Split(' '))
            //{

            //}

            //for (int i = 0; i < AllItems.Count; i++)
            //{

            //}

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

        public static string GetItemTypeFromImage(string[] item)
        {
            //foreach (var itemType in AllItems)
            //{
            //    for (var i = 0; i < itemType.Count; i++)
            //    {
            //        if (itemType[i][1] == item)
            //        {
            //            Console.WriteLine(ItemTypeIndex[i]);
            //            Console.ReadKey();
            //            return ItemTypeIndex[i];
            //        }
            //    }
            //}

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

            foreach (var itemType in AllItems)
            {
                for (var i = 0; i < itemType.Count; i++)
                {
                    if (itemType[i][0][0] == item)
                    {
                        return ItemTypeIndex[i];
                    }
                }
            }

            return null;
        }

        //Check if item can be collected with CanCollect(), if true is returned then proceed
    }
}
