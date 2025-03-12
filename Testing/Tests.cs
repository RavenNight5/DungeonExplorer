// Filename: Tests.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonExplorer.Testing
{
    internal class Tests
    {
        /// <summary>
        /// Uses static methods containing the Debug.Assert() method to check an unexpected/erroneous value has not been wrongly passed through the input checks.
        /// </summary>
        
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
