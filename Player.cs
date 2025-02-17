using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public static string Name { get; set; }
        public int Health { get; set; }

        private List<string> inventory = new List<string>();

        public Player()
        {
            Health = 6;
        }

        public void PickUpItem(string item)
        {

        }

        public string InventoryContents()
        {
            return string.Join(", ", inventory);
        }
    }
}