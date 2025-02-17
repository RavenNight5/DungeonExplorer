using DungeonExplorer.Levels;

namespace DungeonExplorer
{
    public class Room
    {
        public static string InteractableDisplayNotFound = "IDisp Not Found.";
        public static string RoomNotFound = "Room Not Found.";
        public static string RoomDescriptionNotFound = "Description Not Found.";
        public static string RoomOptionsDescriptionNotFound = "Options Description Not Found.";

        public static int currentLevel = 1;
        public static int currentRoom = 1;

        private string currentRoomDescription;

        Options options;

        //Level_1 newLevel1;

        public Room()
        {
            options = new Options();

            //newLevel1 = new Level_1();

            currentRoomDescription = "";
        }

        public string GetRoomDescription()
        {
            return currentRoomDescription;
        }

        public void StartLevel(int levelNum)
        {
            // Each time StartLevel is called it will be the next level (the iteration levelNum from class Game)
            // Therefore currentRoom needs to be set back to 1 as it will be the first room of the new level
            currentLevel = levelNum;
            currentRoom = 1;

            if (levelNum.Equals(1))  // Statement is required to get the correct Level_x class
            {
                Level_1 newLevel = new Level_1();
                currentRoomDescription = newLevel.GetDescription();

                int[] newOptions = { 0, 1, 2 };

                string concatenatedOptions = options.GetGeneralOptions(newOptions);

                //Console.Write(displayRoom + "\n" + options + "\n\n");
            }

        }

        //public Tuple<string, string> StartLevel(int levelNum)
        //{
        //    currentLevel = levelNum;

        //    if (levelNum.Equals(1))
        //    {
        //        string roomDisplay = newLevel1.GetRoom(1);
        //        currentRoomDescription = newLevel1.GetDescription();

        //        int[] newOptions = { 0, 1, 2 };

        //        string concatenatedOptions = options.GetGeneralOptions(newOptions);

        //        return Tuple.Create(roomDisplay, concatenatedOptions);
        //    }
        //    else return Tuple.Create(RoomNotFound, RoomOptionsDescriptionNotFound);

        //}
    }
}