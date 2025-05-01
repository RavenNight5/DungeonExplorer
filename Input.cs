// Filename: Input.cs
using System;
using System.Linq;

namespace DungeonExplorer
{
    public class Input
    {
        /// <summary>
        /// Handles multiple inputs the player can make, ensuring the provided input is valid regarding the provided criteria (passed as parameters).
        /// These player inputs include:
        ///     - Setting their character name
        ///     - Detecting a specific key that has been pressed during exploration of a room (and validating it with the provided keybind array)
        ///     - Waiting on a specific key (usually Spacebar for description boxes)
        /// </summary>

        public Input() { }

        public void SetName(int min, int max)
        {
            string name = Console.ReadLine();

            ClearCurrentConsoleLine();

            if (name.Equals(null) || name.Any(char.IsWhiteSpace) || !name.All(char.IsLetter))
            {
                ClearCurrentConsoleLine();

                Console.WriteLine("Please input a valid name.");

                SetName(min, max);

            }
            else
            {
                if (name.Length >= min && name.Length <= max)
                {
                    Program.NameTemp = name;
                }
                else
                {
                    ClearCurrentConsoleLine();

                    Console.WriteLine($"Name must be between {min} and {max} characters long.");

                    SetName(min, max);

                }
            }
        }

        public string OptionsGetPlayerResponse(string[] setOptions, bool clearConsoleLine = true, bool enter = false)
        {
            string keyInfo = null;

            keyInfo = Console.ReadKey().Key.ToString();

            if (clearConsoleLine) ClearCurrentConsoleLine();

            if (setOptions.Contains(keyInfo))  // If the setOptions keybinds contain the pressed key then return
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

        public static string WaitOnKey(string keyRequired, string optSecondKey = null, string optThirdKey = null)
        {
            var keyInfo = Console.ReadKey();

            ClearCurrentConsoleLine();

            if (keyInfo.Key.ToString().Equals(keyRequired))
            {
                return keyInfo.Key.ToString();
            }
            else if (keyInfo.Key.ToString().Equals(optSecondKey) && optSecondKey != null)
            {
                return keyInfo.Key.ToString();
            }
            else if (keyInfo.Key.ToString().Equals(optThirdKey) && optThirdKey != null)
            {
                return keyInfo.Key.ToString();
            }
            else
            {
                Console.WriteLine("That is not an option, or the action has already been taken.");

                WaitOnKey(keyRequired, optSecondKey, optThirdKey);

            }

            return null;
        }

        //Combat Inputs

        public string CombatMainOptions(string[] optionsKeyBinds, string currentlyChoosing = "")
        {
            string optionChosen = OptionsGetPlayerResponse(optionsKeyBinds);

            if (optionChosen != null)
            {
                return optionChosen;
            }
            else
            {
                CombatMainOptions(optionsKeyBinds);

                return null;
            }
        }

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
