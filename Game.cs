using System;
using System.Media;
using DungeonExplorer.Dialogue;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    internal class Game
    {
        public static bool playing = true;

        // Here I set multiple classes to static as they will only be defined once per game. Therefore they and their methods can be accessed in the level classes
        public static Player CurrentPlayer { get; private set; }
        public static Room RoomHandler { get; private set; }
        public static Input InputHandler { get; private set; }
        public static Options OptionHandler { get; private set; }


        private General_Info generalInfo;

        public Game()
        {
            Game.CurrentPlayer = new Player();

            Game.RoomHandler = new Room();

            Game.InputHandler = new Input();

            Game.OptionHandler = new Options();

            generalInfo = new General_Info();
        }

        public void Start()
        {
            Console.Clear();

            string[] dialogue = generalInfo.WelcomeDialogue;

            for (int i = 0; i < dialogue.Length; i++)
            {
                new Description_Box(dialogue[i]);

                if (i.Equals(dialogue.Length - 1)) Console.WriteLine("\n\n[Space] to Wake Up\n");
                else Console.WriteLine("\n\n[Space]\n");

                InputHandler.WaitOnKey("Spacebar");

                Console.Clear();
            }

            for (int i = 1; i <= Program.NumOfLevels; i++)
            {

                RoomHandler.StartLevel(i);

                //var startLevel = currentRoom.StartLevel(i);
                //// Returned items from the tuple
                //var displayRoom = startLevel.Item1;
                //var options = startLevel.Item2;

                ////if (!displayRoom.Equals(Room.RoomNotFound))  // Do in testing
                //Console.WriteLine(displayRoom + "\n" + options + "\n\n");
            }

            Console.WriteLine("\n\n---Game finished---\n");

        }
    }
}