// Filename: Inventory_Items.cs
using System;

namespace DungeonExplorer.Text_Displays
{
    internal class Inventory_Items
    {
        /// <summary>
        /// Holds the inventory items and their descriptions each in a jagged array.
        /// </summary>

        public static string[][] II_Sponge = new string[][] { new string[5] {
        "       ",
        "  ░▒▓  ",
        "  ░▒▓  ",
        "  ░▓▓  ",
        "      "
        },
        new string[4] {
        "~ Sponge ~",
        "Your trusted cleaning companion!",
        "Sometimes you talk to it.",
        ""
        }
        };
        public static string[][] II_DustpanBrush = new string[][] { new string[5] {
        "░▒▓╢   ",
        "░▒▓╢   ",
        "   __  ",
        "  ╥╥╥╥ ",
        "      "
        },
        new string[4] {
        "~ Dustpan & Brush ~",
        "For sweeping and cleaning small areas.",
        "",
        ""
        }
        };
        public static string[][] II_Mop = new string[][] { new string[5] {
        "   ╥   ",
        "   ║   ",
        "  ┌║-  ",
        " ~░░┘░ ",
        " ░ ░~ "
        },
        new string[4] {
        "~ Mop ~",
        "Handy for all the puddles this dungeon",
        "seems to create.",
        ""
        }
        };

        public static string[][] II_Key1 = new string[][] { new string[5] {
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
        public static string[][] II_CupEmpty = new string[][] { new string[5] {
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
        public static string[][] II_CupFull = new string[][] { new string[5] {
        "       ",
        "       ",
        "  │▒│╕ ",
        "  ╘═╛  ",
        "      "
        },
        new string[4] {
        "~ Full Cup ~",
        "A cup filled with some strange blood you found",
        "dripping from the ceiling in the hallway...",
        ""
        }
        };
        public static string[][] II_Dagger = new string[][] { new string[5] {
        "   ^   ",
        @"  /░\  ",
        "  │░│  ",
        " -═▒═- ",
        "  ▒   "
        },
        new string[4] {
        "~ Dagger ~",
        "A wide-bladed dagger, probably more useful for",
        "self defense than cleaning.",
        ""
        }
        };
        public static string[][] II_Longsword = new string[][] { new string[5] {
        "   ^   ",
        "   ▒   ",
        "  │▒│  ",
        " -╒▒╕- ",
        "  ▒   "
        },
        new string[4] {
        "~ Longsword ~",
        "I'm a cleaner. Now, let's not get",
        "ahead of ourselves...",
        ""
        }
        };
    }
}
