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
            new string[8] {
            "Dagger",  // Name (used for filtering in search etc.)
            "25",  // Cost (if bought in a shop)
            "0",  // Uses (if perishable item), 0 = infinite
            "18",  // Base Dmg
            "20",  // CRIT Dmg
            "25",  // CRIT Rate
            "18",  // Difficulty of use (1-100) where 100 is an easy attack and 1 is extremely difficult - this number
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
            "A wide-bladed dagger, probably better for self defense rather than cleaning.",
            "(18 Base Damage, not too hard to use)",
            ""
            }
        };
        public static string[][] Weapon_Longsword = new string[][] {
            new string[8] {
            "Longsword",
            "55",
            "0",
            "45",  // Base
            "20",  // CRIT Dmg
            "35",  // CRIT Rate
            "10",  // Ease
            ""
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
            "I'm a cleaner. Now, let's not get ahead of ourselves...",
            "(45 Base Damage, difficult to use)",
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
