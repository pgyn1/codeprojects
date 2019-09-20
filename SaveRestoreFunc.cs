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
        private readonly static char[] commadelim = { ',' };

        //Save option
        private const string WriteToPath = @"";
        private const string WriteToPathList = @"";

        private static string highscorepath;
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
                    c.Close();
                }

                using (StreamWriter d = new StreamWriter(WriteToPathList))
                {
                    foreach (Book y in x)
                    {
                        d.WriteLine($"{y.Name}, {y.Type}, {y.Where}");
                    }

                    d.Close();
                }
                return true; 
            }
            catch (Exception e)
            {
                //
                Console.WriteLine($"SaveRestoreFunc -> SaveGame(char,char,int,int,int,int,int,List<Book>) + \n {e.Message}\n {e.InnerException}");
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

        public static void SetHighScorePath(string x)
        {
            highscorepath = x;
        }

        public static void SetHighScorePath()
        {
            Console.Write("Input high score path: ");
            highscorepath = Console.ReadLine().ToLower();
        }

        public static string GetHighScorePath()
        {
            return highscorepath;
        }

        //No csv excel? SQL server alternative?
        public static void WriteScores(int computerscore, int playerscore)
        {

            try
            {
                List<string> highscores = new List<string>();

                //Read current scores
                using (StreamReader astreamread = new StreamReader(GetHighScorePath()))
                {
                    while (astreamread.Peek() > -1)
                    {
                        highscores.Add(astreamread.ReadLine());
                    }

                    astreamread.Close();
                }

                using (StreamWriter astreamwrite = new StreamWriter(GetHighScorePath()))
                {
                    //Write current scores 
                    foreach (string x in highscores)
                    {
                        var array = x.Split(',');
                        astreamwrite.WriteLine($"{array[0]}, {array[1]}");
                    }

                    //Append new score
                    astreamwrite.WriteLine($"{computerscore}, {playerscore}");
                    astreamwrite.Close();
                }
            }
            catch(Exception e) { Console.WriteLine($"SaveRestoreFunc -> WriteScores(int, int) + {e.Message}\n{e.InnerException}"); }
 

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
            try
            {

                List<string> listofscore = new List<string>();

                using (StreamReader d = new StreamReader(GetHighScorePath()))
                {
                    while (d.Peek() > -1)
                    {
                        var gamma = d.ReadLine().Split(',');
                        var difference = Convert.ToInt32(gamma[1]) - Convert.ToInt32(gamma[0]);
                        listofscore.Add(difference.ToString());


                    }

                    if (listofscore.Count() >= 5)
                    {
                        var listofscoreints = listofscore.Select(k => Convert.ToInt32(k)).ToList(); // convert list strings to list of ints
                        listofscoreints.Sort();
                        listofscoreints.Reverse();

                        Console.WriteLine("\nTop 5 greatest differences...");

                        for (int i = 0; i < 5; i++)
                        {
                            Console.WriteLine($"Playerscore = {listofscoreints[i]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("High score list is < 5");
                    }
                    d.Close();
                }
            }
            catch (Exception e) { Console.WriteLine($"SaveRestoreFunc -> ReturnHighScores() + {e.Message}\n{e.InnerException}"); }


        }
    }
}
