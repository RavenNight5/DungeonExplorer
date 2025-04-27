// Filename: Tests.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DungeonExplorer.Testing
{
    internal class Tests
    {
        /// <summary>
        /// Handles the Testing Menu that can be selected at the start of the game. This allows the player to start at a specific point in the game or start a combat session.
        /// Uses static methods containing the Debug.Assert() method to check an unexpected/erroneous value has not been wrongly passed through the input checks.
        /// </summary>

        public static bool InTestingMode = false;

        public void TestingMenu(bool incorrectInput = false)
        {
            Program.CLEAR_CONSOLE();

            InTestingMode = true;

            Console.WriteLine("Testing Menu\n------------\n\nStart the game from a specific room or begin a combat session. \nAll weapons and items will be available to use.\n\n");

            Console.WriteLine("Go to:\n > Room 1 (The Cell) [1]\n > Room 2 (Hallway) [2]\n > Room 7 (South Hall & Chamber) [7]\n\n");
            Console.WriteLine("Combat:\n > Battle Dragon [D]\n > Battle Gnome [G]\n\n > CLOSE TESTING MENU & Start Game [Space]\n");

            if (incorrectInput)
            {
                Console.WriteLine("That is not an option. Please try again.\n");
            }

            string[] testingOptions = new string[] { "D1", "D2", "D7", "D", "G", "Spacebar" };
            string playerInput = Game.InputHandler.OptionsGetPlayerResponse(testingOptions);  // D is the recognised key inputs 0-9

            if (playerInput == "Spacebar")
            {
                InTestingMode = false;

                beginGame(1);
            }
            else if (playerInput == "D" )
            {
                AddAllInventoryItems();

                Game.CurrentCombatSession = new Combat(Game.Monster[0]);  // Dragon

                Console.WriteLine("Testing combat session has ended, press [any key] to return to the Testing Menu.");

                Console.ReadKey();

                TestingMenu();
            }
            else if (playerInput == "G")
            {
                AddAllInventoryItems();
                
                Game.CurrentCombatSession = new Combat(Game.Monster[1]);  // Gnome

                Console.WriteLine("Testing combat session has ended, press [any key] to return to the Testing Menu.");

                Console.ReadKey();

                TestingMenu();
            }
            else  // A number input
            {
                if (testingOptions.Contains(playerInput))
                {
                    try
                    {
                        AddAllInventoryItems();

                        beginGame(int.Parse(playerInput.TrimStart('D')));
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Input {playerInput} was not recognised as an integer in this instance. \nException caught: " + e);
                    }
                    finally
                    {
                        TestingMenu();
                    }
                }
                else
                {
                    TestingMenu(true);
                }
            }
        }

        private void beginGame(int room)
        {
            Program.game.Start(room);
        }

        private void AddAllInventoryItems()
        {
            if (InTestingMode == true)
            {
                foreach (var itemType in Item.AllItems)
                {
                    for (var i = 0; i < itemType.Count; i++)
                    {
                        if (!Game.CurrentPlayer.InventoryItems.Contains(itemType[i][0][0]))  // If the inventory does not already contain the item
                        {
                            Game.CurrentPlayer.InventoryItems.Add(itemType[i][0][0]);  // Add the item name to the inventory
                        }
                    }
                }
            }

        }

        public static void CheckRoomDisplayExists(int lastRoomFetched, int descriptionsListCount)
        {
            Debug.Assert(lastRoomFetched < descriptionsListCount, "Room to be fetched does not exist.");
        }
        // Checks the current room's actioms are not null (if true then the wrong room has been loaded)
        public static void CheckRoomActionExists(List<List<List<string[]>>> L1_RoomActions)
        {
            Debug.Assert(L1_RoomActions[Room.CurrentRoom - 1] != null, "Error: The current room has no available actions.");
        }
        public static void CheckActionTakenIsValid(int action)
        {
            Debug.Assert(!(action <= -1), "An invalid explore action was erroneously passed through.");
        }

    }
}
