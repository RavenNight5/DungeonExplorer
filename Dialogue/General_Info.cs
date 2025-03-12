// Filename: General_Info.cs
using System;

namespace DungeonExplorer.Dialogue
{
    internal class General_Info
    {
        /// <summary>
        /// Contains the dialogue first shown to the player when they enter the game.
        /// </summary>
        public string[] WelcomeDialogue;

        public General_Info()
        {
            WelcomeDialogue = new string[] {
                $"{Player.Name}, you are the dungeon's lead cleaner! The only cleaner at that...",
                "Your job is to explore, find things to clean, probably clean them, and that's about it.",
                "Remember to check out the room's description [D] before doing anything drastic!",
                "Who knows, maybe you will have a more interesting day today..."
            };
        }
    }
}
