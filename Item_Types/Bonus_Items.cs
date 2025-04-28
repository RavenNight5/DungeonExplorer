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

        private List<string[][]> _items_List = new List<string[][]>();

        public static string[][] Bonus_Item_Sponge = new string[][] {
            new string[6] {
            "Sponge",  // Name (used for filtering in search etc.)
            "35",  // Cost (if bought in a shop)
            "3",  // Uses (if perishable item)
            "0",  // Base dmg (if 0 then it is a bonus item/has different effects)
            "0",  // Difficulty of use (10-100) where 10 is an easy attack and 100 is extremely difficult - this number
                   // acts as the speed percentage the dial moves before an attack (if 0 then it is a bonus item/has different effects)
            "easeOfUse 0.3"//"Shield 0.5 notCRITHIT"  // Special effect (split by whitespace, first string = effect, second = percentage effect has, third = when it applies (if applicable)
            },
            new string[5] {
            "       ",
            "  ░▒▓  ",
            "  ░▒▓  ",
            "  ░▓▓  ",
            "      "
            },
            new string[4] {
            "~ Sponge ~",
            "Your trusted cleaning companion! Sometimes you talk to it.",
            "(Speed Absorption - makes hitting the opponent 30% easier when equipped)",//"(Shield - If not a CRIT HIT will soak 50% damage for 3 turns)",
            ""
            }
        };
        public static string[][] Bonus_Item_Mop = new string[][] {
            new string[6] {
            "Mop",
            "0",  // 0 price means it can't be bought
            "0",  // 0 uses = infinite
            "0",
            "0",
            "AddBaseDamage 3"
            },
            new string[5] {
            "   ╥   ",
            "   ║   ",
            "  ┌║-  ",
            " ~░░┘░ ",
            " ░ ░~ "
            },
            new string[4] {
            "~ Mop ~",
            "Handy for all the puddles this dungeon seems to create.",
            "(+ 3 Base Damage on all attacks)",
            ""
            }
        };
        public static string[][] Bonus_Item_DustpanBrush = new string[][] {
            new string[6] {
            "Dustpan",
            "20",
            "1",
            "0",
            "0",
            "LifeShield 0.2"  // Here the special effect is "LifeShield" - when this is equipped you will not be defeated
                              // from losing all health, instead this item will be destroyed and restore 20% of your max health
            },
            new string[5] {
            "░▒▓╢   ",
            "░▒▓╢   ",
            "   __  ",
            "  ╥╥╥╥ ",
            "      "
            },
            new string[4] {
            "~ Dustpan & Brush ~",
            "For sweeping and cleaning small areas.",
            "(Life Shield - Restore 20% health upon death)",
            ""
            }
        };
        public static string[][] Bonus_Item_EmptyCup = new string[][] {
            new string[6] {
            "Empty Cup",
            "10",
            "1",
            "0",
            "0",
            ""
            },
            new string[5] {
            "       ",
            "       ",
            "  │ │╕ ",
            "  ╘═╛  ",
            "      "
            },
            new string[4] {
            "~ Empty Cup ~",
            "An empty cup. It's pretty empty at the moment.",
            "",
            ""
            }
        };
        public static string[][] Bonus_Item_BloodCup = new string[][] {
            new string[6] {
            "Blood Cup",
            "0",
            "1",
            "0",
            "0",
            "AddBaseDamage 20"
            },
            new string[5] {
            "       ",
            "       ",
            "  │▒│╕ ",
            "  ╘═╛  ",
            "      "
            },
            new string[4] {
            "~ Blood Cup ~",
            "A cup filled with some strange blood you found dripping from the ceiling in the hallway...",
            "(+ 20 Base Damage on any attack - one use)",
            ""
            }
        };

        // Misc Items
        public static string[][] Bonus_Item_Key = new string[][] {
            new string[6] {
            "Rusty Key",
            "0",
            "0",
            "0",
            "0",
            ""
            },
            new string[5] {
            "   ┌-  ",
            "   ├-  ",
            "   │╗  ",
            "   ╚╝  ",
            "      "
            },
            new string[4] {
            "~ Old Rusted Key ~",
            "A key that looks like it would work",
            "on one of the cell doors.",
            ""
            }
        };

        public Bonus_Items()
        {
            _items_List.Add(Bonus_Item_Sponge);
            _items_List.Add(Bonus_Item_Mop);
            _items_List.Add(Bonus_Item_DustpanBrush);
            _items_List.Add(Bonus_Item_EmptyCup);
            _items_List.Add(Bonus_Item_BloodCup);

            _items_List.Add(Bonus_Item_Key);

            AllItems.Add(_items_List);
        }
    }
}
