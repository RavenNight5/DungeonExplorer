// Filename: Game_Map.cs
using System.Collections.Generic;
using DungeonExplorer.Levels;

namespace DungeonExplorer
{
    public class Game_Map
    {
        /// <summary>
        /// - Checks the level number and calls start() on the corresponding class
        /// - Handles returning to a level from a screen such as the inventory
        /// </summary>
        public static int CurrentLevel = 1;
        public static int CurrentRoom = 1;

        private List<Level_1> rooms = new List<Level_1>();  // A list containing the level objects so they can automatically be referenced

        public Game_Map()
        {
            rooms.Add(new Level_1());
        }

        public void ReturnToLevel()  // If player is in inventory or another screen this method will be called to continue the gameplay
        {
            rooms[CurrentLevel - 1].DisplayRooms();
        }

        public void StartLevel(int levelNum, int room = 1)
        {
            CurrentLevel = levelNum;
            CurrentRoom = room;

            rooms[CurrentLevel - 1].Start();  // Get the index of the level object from the list and start that level
        }

    }
}
