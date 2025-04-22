using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer.Item_Types;

namespace DungeonExplorer
{
    public class Item : Inventory
    {
        /// <summary>
        /// Uses dynamic polymorphism to create the subclass objects of Items
        /// These objects then assign the item string arrays to a list then that list to All_Items
        /// </summary>

        public List<List<string>> AllItems = new List<List<string>>();


        // Finds the corresponding item image from the single string name stored in the player's inventory
        public string[] GetItemImage(string item)
        {
            //foreach (string stats in itemStats[6].Split(' '))
            //{

            //}

            //for (int i = 0; i < AllItems.Count; i++)
            //{

            //}

            foreach (var a in AllItems)
            {
                Console.WriteLine(a);
            }

            return null;
        }


        //Check if item can be collected with CanCollect(), if true is returned then proceed
    }
}
