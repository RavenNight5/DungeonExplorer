using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                            ▓▄▄ ▓▄▄ ▓▄▄ ▒▀▒ ▒ ▀▓ ▓▄▄ ▓▀▄
										    
                                           



[Enter full screen or use a large window for the best experience.]";
        }

        public string GetWelcomeTitle()
        {
            return welcomeTitle;
        }
    }
}
