// Filename: Game.cs
using System;
using System.Collections.Generic;
using DungeonExplorer.Dialogue;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    internal class Game
    {
        /// <summary>
        /// Initialises four objects that will be the main objects throughout the game.
        /// Starts the game throug the newly-initialised room object.
        /// </summary>
        
        // Here I set multiple classes to static as they will only be defined once per game. Therefore, they and their methods can be accessed in other main classes (e.g. Level_1)
        public static Player CurrentPlayer { get; private set; }
        public static Room RoomHandler { get; private set; }
        public static Input InputHandler { get; private set; }
        public static Options OptionHandler { get; private set; }

        public Game()
        {
            Game.CurrentPlayer = new Player();

            Game.RoomHandler = new Room();

            Game.InputHandler = new Input();

            Game.OptionHandler = new Options();
        }

        public void Start()
        {
            Program.CLEAR_CONSOLE();

            General_Info general_Info = new General_Info();

            string[] dialogue = general_Info.WelcomeDialogue;

            for (int i = 0; i < dialogue.Length; i++)
            {
                new Description_Box(dialogue[i]);

                if (i.Equals(dialogue.Length - 1)) Console.WriteLine("\n\n[Space] to Wake Up\n");
                else Console.WriteLine("\n\n[Space]\n");

                InputHandler.WaitOnKey("Spacebar");

                Program.CLEAR_CONSOLE();
            }

            for (int i = 1; i <= Program.NumOfLevels; i++)
            {
                RoomHandler.StartLevel(i);
            }

            Console.WriteLine("\n\n---Game Finished---\n");
        }
    }
}