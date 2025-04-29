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
            new string[8] {
            "Sponge",  // Name (used for filtering in search etc.)
            "35",  // Cost (if bought in a shop)
            "5",  // Uses (reset after each combat session)
            "0",  // Base Dmg (if 0 then has different effects)...
            "0",  // CRIT Dmg
            "0",  // CRIT Rate
            "0",  // Difficulty of use (10-100) where 10 is an easy attack and 100 is extremely difficult - this number
                   // acts as the speed percentage the dial moves before an attack (if 0 then it is a bonus item/has different effects)
            "Shield 50" // Special effect (split by whitespace, first string = effect, second = percentage effect has, third = when it applies (if applicable)
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
            "(Damage Absorption - will soak 50% damage up to 5 times)",
            ""
            }
        };
        public static string[][] Bonus_Item_BeefySponge = new string[][] {
            new string[8] {
            "Beefy Sponge",
            "70",  // Cost
            "3",  // Uses
            "0",  // Base Dmg
            "0",  // CRIT Dmg
            "0",  // CRIT Rate
            "0",  // Difficulty
            "Shield 70" // Shields from 70% of damage with any attack
            },
            new string[5] {
            "       ",
            "  ▒▓█  ",
            "  ▒▓█  ",
            "  ▓██  ",
            "      "
            },
            new string[4] {
            "~ Beefy Sponge ~",
            "So dense you could make armour out of it.",
            "(Damage Absorption - Will soak 70% damage from ANY HIT up to 3 times)",//(Speed Absorption - makes hitting the opponent 30% easier when equipped)"
            ""
            }
        };
        public static string[][] Bonus_Item_Mop = new string[][] {
            new string[8] {
            "Mop",
            "0",  // 0 price means it can't be bought
            "0",  // Uses 0 = infinite
            "8",  // Base Dmg
            "0",  // CRIT Dmg
            "5",  // CRIT Rate
            "0",  // Difficulty
            ""
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
            "(+ 8 Base Damage & + 5% CRIT Rate on all attacks)",
            ""
            }
        };
        public static string[][] Bonus_Item_DustpanBrush = new string[][] {
            new string[8] {
            "Dustpan",
            "20",
            "1",  // Uses
            "0",  // Base Dmg
            "0",  // CRIT Dmg
            "0",  // CRIT Rate
            "0",  // Difficulty
            "LifeShield 30"  // Special effect "LifeShield" - when this is equipped you will not be defeated by losing all health,
                              // instead this item will restore 30% of your max health (only once per combat session)
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
            "(Life Shield - Restore 30% health upon death (once per combat session))",
            ""
            }
        };
        public static string[][] Bonus_Item_EmptyCup = new string[][] {
            new string[8] {
            "Empty Cup",
            "10",
            "0",  // Uses
            "0",  // Base Dmg
            "0",  // CRIT Dmg
            "0",  // CRIT Rate
            "20",  // Difficulty
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
            "(Slows down your attack by 20%... Used as a distraction?)",
            ""
            }
        };
        public static string[][] Bonus_Item_BloodCup = new string[][] {
            new string[8] {
            "Blood Cup",
            "0",
            "1",  // Uses
            "20",  // Base Dmg
            "20",  // CRIT Dmg
            "100",  // CRIT Rate
            "0",  // Difficulty
            "#"  // # means perishable (after uses are depleated to 0 then remove from inventory)
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
            "(+ 20 Base Damage, + 20% CRIT Damage and + 100% CRIT Rate on any attack - perishable, one use)",
            ""
            }
        };

        // Misc Items
        public static string[][] Bonus_Item_Key = new string[][] {
            new string[8] {
            "Rusty Key",
            "0",
            "0",  // Uses
            "0",  // Base Dmg
            "10",  // CRIT Dmg
            "0",  // CRIT Rate
            "0",  // Difficulty
            "WeakSpotDmg 30 #"
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
            "A key that looks like it would work on one of the cell doors.",
            "(+ 30% Weak Spot Damage and + 10% CRIT Damage on any attack) or (Unlock one cell door - perishable, one use)",
            ""
            }
        };

        public Bonus_Items()
        {
            _items_List.Add(Bonus_Item_Sponge);
            _items_List.Add(Bonus_Item_Mop);
            _items_List.Add(Bonus_Item_DustpanBrush);
            _items_List.Add(Bonus_Item_EmptyCup);
            _items_List.Add(Bonus_Item_BeefySponge);
            _items_List.Add(Bonus_Item_BloodCup);

            _items_List.Add(Bonus_Item_Key);

            AllItems.Add(_items_List);
        }

    }
}
