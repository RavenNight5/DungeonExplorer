// Filename: Tests.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonExplorer.Testing
{
    internal class Tests
    {
        /// <summary>
        /// Handles the Testing Menu that can be selected at the start of the game. This allows the player to start at a specific point in the game or start a combat session.
        /// Uses static methods containing the Debug.Assert() method to check an unexpected/erroneous value has not been wrongly passed through the input checks.
        /// </summary>
        
        public void TestingMenu()
        {
            Program.CLEAR_CONSOLE();

            Console.WriteLine("Testing Menu\n------------\n\nStart the game from a specific room or begin a combat session. \nAll weapons and items will be available to use.\n\n");

            Console.WriteLine("Go to:\n > Room 1 (The Cell) [1]\n > Room 2 (Hallway) [2]\n > Room 3 (South Hall & Chamber) [3]\n\n");
            Console.WriteLine("Combat:\n > Battle Dragon [D]\n > Battle Gnome [G]\n\n > CLOSE TESTING MENU & Start Game [Space]\n\n");

            string playerInput = Game.InputHandler.OptionsGetPlayerResponse(new string[] { "97", "D1", "Spacebar"});

            if (playerInput == "Spacebar")
            {
                beginGame(7);
            }
            else if (playerInput == "D1")
            {
                Console.WriteLine("DDDDD1111111");
            }
            else if (playerInput == "97")
            {
                Console.WriteLine("11111");
            }
            else
            {
                Console.Write(playerInput);
            }
        }

        private void beginGame(int room)
        {
            Program.game.Start(room);
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
