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
										    
                                           
	

	";
        }

        public string GetWelcomeTitle()
        {
            return welcomeTitle;
        }
    }
}
