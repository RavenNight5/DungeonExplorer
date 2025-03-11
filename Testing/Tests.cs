using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

namespace DungeonExplorer.Testing
{
    internal class Tests
    {
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
