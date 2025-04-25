using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer.Item_Types
{
    public class Weapons : Item, ICollectible
    {
        public bool CanCollect()
        {
            return true;
        }

        private List<string[][]> _items_List = new List<string[][]>();

        public static string[][] Weapon_Dagger = new string[][] {
            new string[6] {
            "Dagger",  // Name (used for filtering in search etc.)
            "25",  // Cost (if bought in a shop)
            "",  // Uses (if perishable item)
            "18",  // Base dmg
            "50",  // Difficulty of use (10-100) where 10 is an easy attack and 100 is extremely difficult - this number
                   // acts as the speed percentage the dial moves before an attack
            ""  // Special effect if applicable
            },
            new string[5] {
            "   ^   ",
            @"  /░\  ",
            "  │░│  ",
            " -═▒═- ",
            "  ▒   "
            },
            new string[4] {
            "~ Dagger ~",
            "A wide-bladed dagger, probably better",
            "for self defense rather than cleaning.",
            ""
            }
        };
        public static string[][] Weapon_Longsword = new string[][] {
            new string[6] {
            "Longsword",  // Name (used for filtering in search etc.)
            "55",  // Cost (if bought in a shop)
            "8",  // Uses (if perishable item)
            "24",  // Base dmg
            "35",  // Difficulty of use (10-100) where 10 is an easy attack and 100 is extremely difficult - this number
                   // acts as the speed percentage the dial moves before an attack
            ""  // Special effect if applicable
            },
            new string[5] {
            "   ^   ",
            "   ▒   ",
            "  │▒│  ",
            " -╒▒╕- ",
            "  ▒   "
            },
            new string[4] {
            "~ Longsword ~",
            "I'm a cleaner.",
            "Now, let's not get ahead of ourselves...",
            ""
            }
        };

        public Weapons()
        {
            _items_List.Add(Weapon_Dagger);
            _items_List.Add(Weapon_Longsword);

            AllItems.Add(_items_List);
        }
    }
}
