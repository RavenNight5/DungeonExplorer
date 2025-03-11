using System;
using System.Diagnostics;
using DungeonExplorer.Levels;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    public class Room
    {
        public static int CurrentLevel = 1;
        public static int CurrentRoom = 1;

        public static string currentRoomDescription = "";

        public static string[] currentEquippedItem = Player.emptySlot;

        Level_1 level_1;

        public Room()
        {
            level_1 = new Level_1();
        }


        public static string GetCurrentItemsAndStats()
        {
            string HealthVisual = "";

            for (int i = 0; i < Player.Health; i++)
            {
                HealthVisual += "+ ";
            }

            string equippedBox = $@" Equipped:    Gold Coins:
 --── ──--    ┌───--- - -  
 │{currentEquippedItem[0]}│    ║ 10 
 │{currentEquippedItem[1]}│    └───--- - - 
 ║{currentEquippedItem[2]}║    {Player.NamePlural} Health:
 │{currentEquippedItem[3]}│    ┌───────----- - - - 
 │ {currentEquippedItem[4]}│    ║ {HealthVisual} ({Player.Health}/{Player.MaxHealth})
 --─ + ─--    └───────----- - - - 

";

            Level_1.UpdateOptions();

            return equippedBox;
        }


        public void ReturnToLevel()  // If player is in inventory or another screen this method will be called to continue the gameplay
        {
            //Console.WriteLine("Returning to " + CurrentLevel + " room: " + CurrentRoom);
            
            if (CurrentLevel.Equals(1))
            {
                level_1.DisplayRooms();
            }
        }
        public void NextRoom()
        {
            CurrentRoom++;
        }

        public void StartLevel(int levelNum)
        {
            // Each time StartLevel is called it will be the next level (the iteration levelNum from class Game)
            // Therefore CurrentRoom needs to be set back to 1 as it will be the first room of the new level
            CurrentLevel = levelNum;
            CurrentRoom = 1;

            if (levelNum.Equals(1))  // Statement is required to get the correct Level_x class
            {
                level_1.Start();
            }

        }

        public static string GetDescription()
        {
            return currentRoomDescription;
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