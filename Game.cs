// Filename: Game.cs
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DungeonExplorer.Dialogue;
using DungeonExplorer.Item_Types;
using DungeonExplorer.Levels;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    public class Game
    {
        /// <summary>
        /// Initialises the main objects that will be used and referenced throughout the game.
        /// This now includes two Monsters, each with different stats and abilities.
        /// Starts the game throug the newly-initialised room object.
        /// </summary>

        private string playerPlural = "";

        public static Combat CurrentCombatSession { get; set; }

        // Here I set multiple objects as static since they will only be defined once per game. Therefore, they and their methods can be accessed in other main classes (e.g. Level_1)
        public static Player CurrentPlayer { get; private set; }
        public static List<Monster> Monster { get; private set; }
        public static Game_Map RoomHandler { get; private set; }
        public static Item ItemHandler { get; private set; }
        public static Input InputHandler { get; private set; }
        public static Options OptionHandler { get; private set; }

        public Game()
        {
            CurrentPlayer = new Player(Program.NameTemp, playerPlural, 80);

            RoomHandler = new Game_Map();

            Monster = new List<Monster>();

            Monster.Add(new Monster("Dragon", "Dungeon Dweller", 250, "StealTurn", 1, 18, 20, 35));

            Monster.Add(new Monster("Gnome", "The Gardener", 400, "BaseDamage & CRITRate", 3, 10, 8, 45));

            Item weapons = new Weapons();
            Item bonus_Items = new Bonus_Items();

            ItemHandler = new Item();

            InputHandler = new Input();

            OptionHandler = new Options();
        }

        public void Start(int roomToStartAt = 1)  // roomToStartAt is used when the player selects a room from the testing menu
        {
            Program.CLEAR_CONSOLE();

            if (roomToStartAt == 1)
            {
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

            }

            for (int i = 1; i <= Program.NumOfLevels; i++)
            {
                RoomHandler.StartLevel(i, roomToStartAt);
            }

            Console.WriteLine("\n\n---Game Finished---\n");
        }

        public void StartCombat(int monsterIndex = 0)
        {
            if (CurrentCombatSession == null)
            {
                CurrentCombatSession = new Combat(Monster[monsterIndex]);
            }

            CurrentCombatSession.MainCombatScreen();
        }

        public static void ReturnToCombat()
        {
            if (CurrentCombatSession == null)
            {
                throw new InvalidOperationException("No active combat session exists to return to.");
            }

            if (!Combat.InCombat)
            {
                Combat.InCombat = true;
            }

            CurrentCombatSession.MainCombatScreen();
        }
    }
}