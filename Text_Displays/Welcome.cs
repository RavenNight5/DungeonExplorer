using System;

namespace DungeonExplorer.Text_Displays
{
    internal class Welcome
    {
        private string welcomeTitle;

        public Welcome()
        {
            welcomeTitle = @"
                                                                      
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
                                            ▓▄▄ ▓▄▄ ▓▄▄ ▒▀▒ ▒ ▀▓ ▓▄▄ ▓▀▄  v0.1
										    
                                           



[Enter full screen or use a large window for the best experience.]";
        }

        public string GetWelcomeTitle()
        {
            return welcomeTitle;
        }
    }
}
