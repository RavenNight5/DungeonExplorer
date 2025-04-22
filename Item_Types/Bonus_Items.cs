using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer.Item_Types
{
    internal class Bonus_Items : Item, ICollectible
    {
        public bool CanCollect()
        {
            return true;
        }

        //private List<string[][]> _items_List = new List<string[][]>();

        private List<string> _items_List = new List<string>();

        //public static string[][] Bonus_Item_Sponge = new string[][] { 
        //    //new string[6] {
        //    //"Sponge",  // Name (used for filtering in search etc.)
        //    //"35",  // Cost (if bought in a shop)
        //    //"3",  // Uses (if perishable item)
        //    //"0",  // Base dmg (if 0 then it is a bonus item/has different effects)
        //    //"0",  // Difficulty of use (10-100) where 10 is an easy attack and 100 is extremely difficult - this number
        //    //       // acts as the speed percentage the dial moves before an attack (if 0 then it is a bonus item/has different effects)
        //    //"Shield 0.5 notCRITHIT"  // Special effect (split by whitespace, first string = effect, second = percentage effect has, third = when it applies (if applicable)
        //    //},
        //    new string[5] {
        //    "       ",
        //    "  ░▒▓  ",
        //    "  ░▒▓  ",
        //    "  ░▓▓  ",
        //    "      "
        //    },
        //    new string[4] {
        //    "~ Sponge ~",
        //    "Your trusted cleaning companion! Sometimes you talk to it.",
        //    "",
        //    "(Shield - If not a CRIT HIT will soak 50% damage for 3 turns)"
        //    }
        //};
        //public static string[][] Bonus_Item_DustpanBrush = new string[][] { 
        //    //new string[6] {
        //    //"Sponge",
        //    //"20",
        //    //"1",
        //    //"0",
        //    //"0",
        //    //"LifeShield 0.2"  // Here the special effect is "LifeShield" - when this is equipped you will not be defeated
        //    //                  // from losing all health, instead this item will be destroyed and restore 20% of your max health
        //    //},
        //    new string[5] {
        //    "░▒▓╢   ",
        //    "░▒▓╢   ",
        //    "   __  ",
        //    "  ╥╥╥╥ ",
        //    "      "
        //    },
        //    new string[4] {
        //    "~ Dustpan & Brush ~",
        //    "For sweeping and cleaning small areas.",
        //    "",
        //    "(Life Shield - Restore 20% health upon death)"
        //    }
        //};

        public string aaaaaaaa = "Dagger";
        public string aaaaaaa = "Longsword";

        public Bonus_Items()
        {
            _items_List.Add(aaaaaaaa);
            _items_List.Add(aaaaaaa);

            AllItems.Add(_items_List);
        }
    }
}
