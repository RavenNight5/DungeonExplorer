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
                $"{Program.NameTemp}, you are the dungeon's lead cleaner! The only cleaner at that...",
                "Your job is to explore the dungeon and find things to clean for Gold Coins.",
                "Remember to check out the room's description [D] before doing anything drastic!",
                "Who knows, maybe you will have a more interesting day today..."
            };
        }
    }
}
