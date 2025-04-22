using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private readonly List<Level_1> _levels = new List<Level_1>();  // Using a list containing the level objects so they can automatically be referenced

        public Game_Map()
        {
            _levels.Add(new Level_1());
        }

        public void ReturnToLevel()  // If player is in inventory or another screen this method will be called to continue the gameplay
        {
            _levels[CurrentLevel - 1].DisplayRooms();
        }

        public void StartLevel(int levelNum)
        {
            // Each time StartLevel is called it will be the next level (the iteration levelNum from class Game)
            // Therefore CurrentRoom needs to be set back to 1 as it will be the first room of the new level
            CurrentLevel = levelNum;
            CurrentRoom = 1;

            _levels[CurrentLevel - 1].Start();
        }

    }
}
