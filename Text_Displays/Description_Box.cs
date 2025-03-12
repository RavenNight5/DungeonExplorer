// Filename: Description_Box.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonExplorer.Text_Displays
{
    internal class Description_Box
    {
        /// <summary>
        /// - Initialises a new description box object with the passed values.
        /// - Puts the description inside a display box, making sure the sides allign by using the _len_ & _len_space variables, this box can can be iterated
        /// by pressing the Spacebar key to show the next element in the passed array (if an array was chosen to be displayed in the box).
        /// - Makes sure the words don't get cut in half when splitting the string into new lines based on the max char length requested.
        /// </summary>
        public string Description { get; set; }
        public string descriptionCurrentSplit;
        public string Title { get; set; }
        public int LineMaxCharLen { get; set; }

        private List<string> _description_List;
        private List<string> _descriptionPadding_List;

        private readonly int _numberOfLines;

        private readonly string _len_;
        private readonly string _len_space;

        public Description_Box(string description, int lineMaxCharLen = 54, string title = "")
        {
            Debug.Assert(description != null || description != "", "Description was not provided.");

            Description = description;
            LineMaxCharLen = lineMaxCharLen;
            Title = title;

            _description_List = new List<string>();
            _descriptionPadding_List = new List<string>();

            _numberOfLines = 0;

            _len_ = "";
            _len_space = "";

            descriptionCurrentSplit = description;

            if (LineMaxCharLen.Equals(0))
            {
                _numberOfLines = 1;

                for (int i = 0; i < Description.Length; i++)
                {
                    _len_ = _len_ + "_";
                    _len_space = _len_space + " ";
                }
            }
            else
            {
                double calcNumLines = (double)Description.Length / LineMaxCharLen;

                if (calcNumLines % 1 == 0)  // Check if a whole number
                {
                    _numberOfLines = (int)calcNumLines;
                }
                else
                {
                    _numberOfLines = (int)(calcNumLines + 1);  // If not then round up for the remaining characters
                }

                for (int i = 0; i < lineMaxCharLen; i++)
                {
                    _len_ = _len_ + "_";
                    _len_space = _len_space + " ";
                }
            }

            _description_List = new List<string>();
            _descriptionPadding_List = new List<string>();

            ImplementDescriptionToBox(_len_, _len_space);

        }

        // Puts the description inside a display box, making sure the sides allign by using the _len_ & _len_space variables
        private void ImplementDescriptionToBox(string _len_, string _len_space)
        {
            GetLines(Description);

            // For the spaces that fill after each line
            for (int i = 0; i < _description_List.Count; i++)
            {
                string newLen_Space = " ";
                for (int j = 0; j <= LineMaxCharLen - _description_List[i].Length; j++)
                {
                    newLen_Space = newLen_Space + " ";
                }

                _descriptionPadding_List.Add(newLen_Space);
            }

            
            Console.Write($@"     {Title}
     _____{_len_}__
    / \\  {_len_space}  \\
");

            string descriptionBox = "";

            if (_numberOfLines > 0 && _numberOfLines <= 1)  // One line
            {
                descriptionBox = $@"    |  │  {Description}{_descriptionPadding_List[0]} |
    \\_│  {_len_space}   |";

                Console.WriteLine(descriptionBox);
            }
            else if (_numberOfLines > 1 && _numberOfLines <= 2)  // Two lines
            {
                descriptionBox = $@"    |  │  {_description_List[0]}{_descriptionPadding_List[0]} |
    \\_│  {_description_List[1]}{_descriptionPadding_List[1]} |";

                Console.WriteLine(descriptionBox);
            }
            else if (_numberOfLines > 2)  // Three or more lines
            {
                descriptionBox = $@"    |  │  {_description_List[0]}{_descriptionPadding_List[0]} |
    \\_│  {_description_List[1]}{_descriptionPadding_List[1]} |";

                for (int i = 2; i < _numberOfLines + 2; i++)
                {

                    try
                    {
                        descriptionBox = descriptionBox + ("\n       │  " + _description_List[i] + _descriptionPadding_List[i] + " |");
                    }
                    catch (Exception) { }
                }

                Console.WriteLine(descriptionBox);
            }

            Console.Write($@"       │    {_len_}_|__
       │   /{_len_space}  //
       \\_/_{_len_}_//
");

        }

        // Makes sure the words don't get cut in half when splitting the string into new lines based on the max char length requested
        private void GetLines(string desc)
        {
            if (descriptionCurrentSplit.Length >= LineMaxCharLen)  // The method is called until 
            {
                int l = desc.LastIndexOf(" ", LineMaxCharLen);  // Gets the index of the last space character nearest to the max character length (so words are not cut in half at the line break)
                string newDes = desc.Substring(0, l).Trim();  // Create a new substring containing all the characters up to the last space character (trim so the space char is not included)

                _description_List.Add(newDes);  // Add the newly trimmed line to the description list

                descriptionCurrentSplit = descriptionCurrentSplit.Remove(0, newDes.Length + 1);  // Remove the string just taken from the description from descriptionCurrentSplit

                GetLines(descriptionCurrentSplit);  // Repeat until the descriptionCurrentSplit length is less than the line length requested
            }
            else
            {
                _description_List.Add(desc);  // Add the final line

                descriptionCurrentSplit = "";

                return;  // _description_List has been ammended with the newly-split lines from the description, so return to the previous method and continue
            }
        }

        // Used to directly create a description box from a passed array (static so can be accessed throughout the program)
        public static void ArrayDescription(string[] arrayDesc, int maxCharLen)
        {
            for (int i = 0; i < arrayDesc.Length; i++)
            {
                // Clears the current console screen and the scrollback buffer (characters that may be out of view but still there when you scroll up)
                Program.CLEAR_CONSOLE();

                new Description_Box(arrayDesc[i], maxCharLen);

                if (i.Equals(arrayDesc.Length - 1))
                {
                    Console.WriteLine("\n\n[Space] to Resume\n");  // When player reaches last box of the current description box change the prompt to contain "Resume"
                }
                else
                {
                    Console.WriteLine("\n\n[Space]\n");
                }

                Game.InputHandler.WaitOnKey("Spacebar");
            }
        }

    }
}
