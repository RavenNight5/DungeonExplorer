using System;
using System.Media;
using DungeonExplorer.Dialogue;
using DungeonExplorer.Text_Displays;

namespace DungeonExplorer
{
    internal class Game
    {
        public static bool playing = true;

        public Player player;
        private Room currentRoom;

        private Input input;

        private General_Info generalInfo;

        public Game()
        {
            player = new Player();
            currentRoom = new Room();

            input = new Input();

            generalInfo = new General_Info();

            Start();

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

                Input.WaitOnKey("Spacebar");

                Console.Clear();
            }

            for (int i = 1; i <= Program.NumOfLevels; i++)
            {

                currentRoom.StartLevel(i);

                //var startLevel = currentRoom.StartLevel(i);
                //// Returned items from the tuple
                //var displayRoom = startLevel.Item1;
                //var options = startLevel.Item2;

                ////if (!displayRoom.Equals(Room.RoomNotFound))  // Do in testing
                //Console.WriteLine(displayRoom + "\n" + options + "\n\n");
            }

            Console.WriteLine("---Game finished---");

        }
    }
}