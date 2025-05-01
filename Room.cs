// Filename: Room.cs
using System;
using System.Diagnostics;
using DungeonExplorer.Levels;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    public class Room : Game_Map
    {
        /// <summary>
        /// - Handles the player's quick-view stats (such as the current equipped item, coins and health)
        /// - Returns the current room's description
        /// - Handles choices the player can make from the current room they are in
        /// </summary>
        public static string CurrentEquippedItem = "";
        public static string[] CurrentEquippedItemImage = Inventory.InventoryEmptySlot;

        public static string CurrentRoomDescription = "";

        // Called before the room display is written to the console, returning the player's quick-veiw stats as a string.
        public static string GetCurrentItemsAndStats()
        {
            string HealthVisual = "";

            for (int i = 0; i < Player.Health; i += 10)
            {
                HealthVisual += "+ ";
            }

            
            string stats = $@" Equipped:    Gold Coins:
 --── ──--    ┌───--- - -  
 │{CurrentEquippedItemImage[0]}│    ║ {Player.GoldCoins} 
 │{CurrentEquippedItemImage[1]}│    └───--- - - 
 ║{CurrentEquippedItemImage[2]}║    {Program.NameTemp}{Program.TempPlural} Health:
 │{CurrentEquippedItemImage[3]}│    ┌───────----- - - - 
 │ {CurrentEquippedItemImage[4]}│    ║ {HealthVisual} ({Player.Health}/{Game.CurrentPlayer.MaxHealth})
 --─ + ─--    └───────----- - - - 

";

            Level_1.UpdateOptions();

            return stats;
        }

        public static string GetDescription()
        {
            return CurrentRoomDescription;
        }

        public static int PlayerChoice(string[] optionsKeyBinds)
        {
            string optionChosen = Game.InputHandler.OptionsGetPlayerResponse(optionsKeyBinds);  // Returns the key pressed by the player as a string

            if (optionChosen != null)
            {
                try
                {
                    //General
                    if (optionChosen.Equals("D"))
                    {
                        Program.CLEAR_CONSOLE();

                        new Description_Box(GetDescription(), 74);

                        Console.WriteLine("\n   [D] to Return");

                        Input.WaitOnKey("D", "Enter", "Spacebar");

                        Program.CLEAR_CONSOLE();

                        return -1;
                    }
                    else if (optionChosen.Equals("C"))
                    {
                        Console.WriteLine("[Stats coming in a later version!]");  // Like inventory but for strength etc, pick up things in dungeon that can level up a certain stat the player chooses - Inscryption

                        PlayerChoice(optionsKeyBinds);

                        return -1;
                    }
                    else if (optionChosen.Equals("Tab"))
                    {   
                        Game.CurrentPlayer.DisplayInventory();

                        return -1;
                    }
                    // Player chose a number action such as Open Chest
                    else
                    {
                        return Array.IndexOf(optionsKeyBinds, optionChosen);
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine(optionChosen + " was not recognised as a string in this instance. \nException caught: " + e);

                    return -1;
                }
            }
            else
            {
                PlayerChoice(optionsKeyBinds);

                return -1;
            }
        }
    }
}