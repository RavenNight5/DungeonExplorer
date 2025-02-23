﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Input
    {
        public void SetName(int min, int max)
        {
            string name = Console.ReadLine();

            ClearCurrentConsoleLine();

            if (name.Equals(null) || name.Any(Char.IsWhiteSpace) || !name.All(Char.IsLetter))
            {
                ClearCurrentConsoleLine();

                Console.WriteLine("Please input a valid name.");

                SetName(min, max);

            }
            else
            {
                if (name.Length >= min && name.Length <= max)
                {
                    Player.Name = name;
                }
                else
                {
                    ClearCurrentConsoleLine();

                    Console.WriteLine($"Name must be between {min} and {max} characters long.");

                    SetName(min, max);

                }
            }
        }

        ///
        public string OptionsGetPlayerResponse(string[] setOptions, bool clearConsoleLine = true, bool enter = false)
        {
            string keyInfo = null;

            keyInfo = Console.ReadKey().Key.ToString();

            if (clearConsoleLine) ClearCurrentConsoleLine();

            if (setOptions.Contains(keyInfo))  // If the set option keybinds contain the pressed key then return
            {
                if (keyInfo.Equals("Enter") && enter.Equals(false))
                {
                    return keyInfo;
                }
                else if (keyInfo.Equals("Enter") && enter.Equals(true))  // User has pressed an incorrect key then enter cannot be pressed to submit it
                {
                    OptionsGetPlayerResponse(setOptions, true);

                    return null;
                }
                else
                {
                    return keyInfo;
                }
               
            }
            else
            {
                if (!clearConsoleLine) ClearCurrentConsoleLine();

                Console.WriteLine("That is not an option, or the action has already been taken.");
                
                return null;
            }
        }

        ///
        public void WaitOnKey(string keyRequired)
        {
            var keyInfo = Console.ReadKey();

            ClearCurrentConsoleLine();

            if (keyInfo.Key.ToString().Equals(keyRequired))
            {
                return;
            }
            else
            {
                Console.WriteLine("That is not an option, or the action has already been taken.");

                WaitOnKey(keyRequired);

            }
        }
        public void WaitOnAnyKey()
        {
            Console.ReadKey();
        }
        ///

        public static void ClearCurrentConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            int currentLineCursor = Console.CursorTop;

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
