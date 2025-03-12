// Filename: Room.cs
using System;
using System.Diagnostics;
using DungeonExplorer.Levels;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    public class Room
    {
        /// <summary>
        /// - Checks the level number and calls start() on the corresponding class
        /// - Handles the player's quick-view stats (such as the current equipped item, coins and health)
        /// - Returns the current room's description
        /// - Handles choices the player can make from the current room they are in
        /// </summary>
        public static int CurrentLevel = 1;
        public static int CurrentRoom = 1;

        public static string CurrentRoomDescription = "";

        public static string[] CurrentEquippedItem = Player.EmptySlot;

        private readonly Level_1 _level_1;

        public Room()
        {
            _level_1 = new Level_1();
        }

        public void ReturnToLevel()  // If player is in inventory or another screen this method will be called to continue the gameplay
        {
            //Console.WriteLine("Returning to " + CurrentLevel + " room: " + CurrentRoom);
            
            if (CurrentLevel.Equals(1))
            {
                _level_1.DisplayRooms();
            }
        }

        public void StartLevel(int levelNum)
        {
            // Each time StartLevel is called it will be the next level (the iteration levelNum from class Game)
            // Therefore CurrentRoom needs to be set back to 1 as it will be the first room of the new level
            CurrentLevel = levelNum;
            CurrentRoom = 1;

            if (levelNum.Equals(1))  // Statement is required to get the correct Level_x class
            {
                _level_1.Start();
            }

        }


        // Called before the room display is written to the console, returning the player's quick-veiw stats as a string.
        public static string GetCurrentItemsAndStats()
        {
            string HealthVisual = "";

            for (int i = 0; i < Player.Health; i++)
            {
                HealthVisual += "+ ";
            }

            string stats = $@" Equipped:    Gold Coins:
 --── ──--    ┌───--- - -  
 │{CurrentEquippedItem[0]}│    ║ 10 
 │{CurrentEquippedItem[1]}│    └───--- - - 
 ║{CurrentEquippedItem[2]}║    {Player.NamePlural} Health:
 │{CurrentEquippedItem[3]}│    ┌───────----- - - - 
 │ {CurrentEquippedItem[4]}│    ║ {HealthVisual} ({Player.Health}/{Player.MaxHealth})
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
            string optionChosen = Game.InputHandler.OptionsGetPlayerResponse(optionsKeyBinds);

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

                        Game.InputHandler.WaitOnKey("D", "Enter", "Spacebar");

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
                    else  // Player chose a number action such as Open Chest
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