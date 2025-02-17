using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer.Text_Displays
{
    internal class Description_Box
    {
        public string Description { get; set; }
        public string Title { get; set; }

        public int LineMaxCharLen { get; set; }

        private int numberOfLines;

        private string len_;
        private string len_space;

        public string descriptionCurrentSplit;

        private List<string> description_List;
        private List<string> descriptionPadding_List;

        public Description_Box(string description, int lineMaxCharLen = 54, string title = "")
        {
            Debug.Assert(description != null || description != "", "Description was not provided.");

            Description = description;
            LineMaxCharLen = lineMaxCharLen;
            Title = title;

            numberOfLines = 0;

            len_ = "";
            len_space = "";

            descriptionCurrentSplit = description;

            if (LineMaxCharLen.Equals(0))
            {
                numberOfLines = 1;

                for (int i = 0; i < Description.Length; i++)
                {
                    len_ = len_ + "_";
                    len_space = len_space + " ";
                }
            }
            else
            {
                double calcNumLines = (double)Description.Length / LineMaxCharLen;

                if (calcNumLines % 1 == 0)  // Check if a whole number
                {
                    numberOfLines = (int)calcNumLines;
                }
                else
                {
                    numberOfLines = (int)(calcNumLines + 1);  // If not then round up for the remaining characters
                }

                for (int i = 0; i < lineMaxCharLen; i++)
                {
                    len_ = len_ + "_";
                    len_space = len_space + " ";
                }
            }

            description_List = new List<string>();
            descriptionPadding_List = new List<string>();

            ImplementDescriptionToBox(len_, len_space);

        }

        private void GetLines(string des)
        {
            if (descriptionCurrentSplit.Length >= LineMaxCharLen)
            {
                int l = des.LastIndexOf(" ", LineMaxCharLen);
                string newDes = des.Substring(0, l).Trim();

                description_List.Add(newDes);

                descriptionCurrentSplit = descriptionCurrentSplit.Remove(0, newDes.Length + 1);

                GetLines(descriptionCurrentSplit);
            }
            else
            {
                description_List.Add(des);

                descriptionCurrentSplit = "";

                return;
            }
        }


        private void ImplementDescriptionToBox(string _len_, string _len_space)
        {
            if (!numberOfLines.Equals(1))
            {

                GetLines(Description);

                // For the spaces that fill after each line
                for (int i = 0; i < description_List.Count; i++)
                {
                    string newLen_Space = " ";

                    for (int j = 0; j <= LineMaxCharLen - description_List[i].Length; j++)
                    {
                        newLen_Space = newLen_Space + " ";
                    }

                    descriptionPadding_List.Add(newLen_Space);
                }
            }

            //for (int i = 0; i < description_List.Count; i++)
            //{
            //    Console.WriteLine(description_List[i]);
            //}


            Console.Write($@"     {Title}
     _____{_len_}__
    / \\  {_len_space}  \\
");

            string descriptionBox = "";

            if (numberOfLines > 0 && numberOfLines <= 1)  // One line
            {
                descriptionBox = $@"    |  │  {Description}   |
    \\_│  {_len_space}   |";

                Console.WriteLine(descriptionBox);
            }
            else if (numberOfLines > 1 && numberOfLines <= 2)  // Two lines
            {
                descriptionBox = $@"    |  │  {description_List[0]}{descriptionPadding_List[0]} |
    \\_│  {description_List[1]}{descriptionPadding_List[1]} |";

                Console.WriteLine(descriptionBox);
            }
            else if (numberOfLines > 2)  // Three or more lines
            {
                descriptionBox = $@"    |  │  {description_List[0]}{descriptionPadding_List[0]} |
    \\_│  {description_List[1]}{descriptionPadding_List[1]} |";

                for (int i = 2; i < numberOfLines + 2; i++)
                {

                    try
                    {
                        descriptionBox = descriptionBox + ("\n       │  " + description_List[i] + descriptionPadding_List[i] + " |");
                    }
                    catch (Exception) { }
                }

                Console.WriteLine(descriptionBox);
            }

            Console.Write($@"       │   {_len_}__|__
       │  /{_len_space}   //
       \\/_{_len_}__//
");

        }
    }
}
