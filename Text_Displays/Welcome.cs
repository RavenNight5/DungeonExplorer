// Filename: Welcome.cs
using System;

namespace DungeonExplorer.Text_Displays
{
    internal class Welcome
    {
        /// <summary>
        /// Initialises the title of the game and returns it as a string.
        /// </summary>
        private readonly string _welcomeTitle;

        public Welcome()
        {
            _welcomeTitle = $@"
                                                                      
▀███▀▀▀██▄
  ██    ▀██▄
  ██     ▀█████  ▀███ ▀████████▄  ▄█▀█████ ▄▄█▀██  ▄██▀██▄▀████████▄
  ██      ██ ██    ██   ██    ██ ▄██    █ ▄█▀   ██▓█▀   ▀██ ██    ██
  █▓     ▄██ ▓█    ██   █▓    ██ ▀▓▓▀  ▀  ▓█▀▀▀▀▀▀▓█     ██ █▓    ██
  █▓    ▄█▓▀ ▓█    █▓   █▓    ▓█ █▓       ██▄    ▄▓█     ▓█ █▓    ▓█
  ▓▓     ▓▓▓ ▓█    ▓▓   ▓▓    ▓▓ ▀▓▓▓▓▓▀  ▓▓▀▀▀▀▀▀▓█     ▓▓ ▓▓    ▓▓
  ▓▒    ▓▓▒▀ ▓▓    ▓▓   ▓▓    ▓▓ ▓▒       ▒▓▓     ▓▓▓   ▓▓▓ ▓▓    ▓▓
▒ ▒ ▒ ▒ ▒    ▒▒▒▓▒ ▒▓▒▒ ▒▒▒  ▒▓▒ ▒▒ ▒▓▒ ▒  ▒ ▒ ▒▒  ▒ ▒▒▒ ▒ ▒▒▒  ▒▓▒ ▒
                                ▒▒     ▒▒
                                 ▒▒▒▒ ▒▒    ▄▀▀ █   ██▀ ▄▀▄ █▄ █ ██▀ █▀▄
                                            ▓▄▄ ▓▄▄ ▓▄▄ ▒▀▒ ▒ ▀▓ ▓▄▄ ▓▀▄  {Program.VersionNumber}
										    
                                           



[Enter full screen or use a large window for the best experience.]";
        }

        public string GetWelcomeTitle()
        {
            return _welcomeTitle;
        }
    }
}
