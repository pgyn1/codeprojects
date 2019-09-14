using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReturnChar
{
    class SaveRestoreFunc
    {
        public readonly static char[] commadelim = { ',' };

        //Save option
        public const string WriteToPath = @"C:\temp\gamefiles\savenew.csv";
        public const string WriteToPathList = @"C:\temp\gamefiles\savelist.csv";

        public static string[] RestoredSavedGameValues {get; set;}

        //restoregamefile // We need a restoregamefile 
        public static bool SaveGame(char difflvl, char mode, int gamescore, int timer, int currentplayerscore, int currentcomputerscore, int randnumber, List<Book> x)
        {

            // write stream
            try
            {
                using (StreamWriter c = new StreamWriter(WriteToPath))
                {
                    c.WriteLine($"{difflvl}, {mode}, {gamescore}, {timer}, {currentplayerscore}, {currentcomputerscore}, {randnumber} ");
                }

                using (StreamWriter d = new StreamWriter(WriteToPathList))
                {
                    foreach (Book y in x)
                    {
                        d.WriteLine($"{y.Name}, {y.Type}, {y.Where}");
                    }
                }
                return true; 
            }
            catch (StackOverflowException e)
            {
                //
                Console.WriteLine($"{e.Message}");
                return false;
            }
           
        }

        //If not a new game we need to rebuild the gamearray in the gameseq class
        public static List<Book> RestoreGameArray(string restoregamefile)
        {
            List<Book> gamearraylist = new List<Book>();

            using (StreamReader d = new StreamReader(restoregamefile))
            {
                while (d.Peek() > -1)
                {
                    var gamma = d.ReadLine().Split(',');
                    gamearraylist.Add(new Book { Name = gamma[0], Type = gamma[1], Where = gamma[2] });
                }

            }

            return gamearraylist;

        }

        public static string[] RestoreGameVars(string restoregamefile)
        {
            using(StreamReader aStreamRead = new StreamReader(restoregamefile))
            {
                return RestoredSavedGameValues = aStreamRead.ReadLine().Split(commadelim);
            }
        }

        //No csv excel? SQL server alternative?
        public static void WriteScores(int computerscore, int playerscore)
        {
            string readpath = @"C:\users\ssd\desktop\highscores.csv";
            string writepath = @"C:\users\ssd\desktop\highscores.csv";
            List<string> highscores = new List<string>();

            //Read current scores
            using (StreamReader astreamread = new StreamReader(readpath))
            {
                while(astreamread.Peek() > -1)
                {
                    highscores.Add(astreamread.ReadLine());
                }
                
                
            }

            using (StreamWriter astreamwrite = new StreamWriter(writepath))
            {
                //Write current scores 
                foreach(string x in highscores)
                {
                    var array = x.Split(',');
                    astreamwrite.WriteLine($"{array[0]}, {array[1]}");
                }

                //Append new score
                astreamwrite.WriteLine($"{computerscore}, {playerscore}");
            }

        }

        //Save function that saves the time 
        //Time
        //Goto saved lists
        //Read the save list time values
        //Ready to display top 5

        //Target
        // Read the compscore and the playerscore
        public static void ReturnHighScores()
        {

            //0 comp //1 player
            string readpath = @"C:\users\ssd\desktop\highscores.csv";
            List<string> listofscore = new List<string>();

            using (StreamReader d = new StreamReader(readpath))
            {
                while (d.Peek() > -1)
                {
                    var gamma = d.ReadLine().Split(',');
                    listofscore.Add(gamma[1]);

                }

                if (listofscore.Count() >= 5)
                {
                    var listofscoreints = listofscore.Select(k => Convert.ToInt32(k)).ToList();
                    listofscoreints.Sort();
                    listofscoreints.Reverse();

                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine($"Player score = {listofscoreints[i]}");
                    }
                }
                else
                {
                    Console.WriteLine("High score list is < 5");
                }

            }

        }
    }
}
