using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReturnChar
{
    class GetStartGameOptions
    {

        //GetDifficultyLevel
        //Returning to lower case
        //Defaults matching the defaults in the GameSeq***

        public static string GetGameFileList()
        {
            try
            {
                Console.WriteLine("\nInput a Gamefile List = ");
                string filepath = @"";
                return filepath+Console.ReadLine().ToLower();
            }
           
            catch(Exception e)
            {
                Console.WriteLine($"GetStartGameOptions -> GetGameFileList() + {e.Message}\n{e.InnerException}");
                return "";
            }
        }

        //Return the difficulty level of the game ranging from easy to super
        public static char GetDiff()
        {
            bool diffstring = false;
            string input;

            try
            {
                Console.Write($"Return Level ('E & M' Game Model Zero, 'H & S' Game Model One) = ");
                char[] chararr = { 'E', 'M', 'H', 'S' };
                input = Console.ReadLine();

                foreach (char x in chararr)
                {
                    if (x.ToString().ToLower().Equals(input.ToLower()))
                    {
                        diffstring = true; break;
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"GetStartGameOptions -> GetDiff() + {e.Message} {e.InnerException}");
                return 'E';
            }

            if (diffstring == true) return GetChar(input); else return 'E';
       
        }

        //Return the mode ranging from a timer to a target
        public static char GetMode()
        {

            try
            {
                Console.Write($"Return Mode ('X' timer 'Y' empty 'Z' target) = ");
                return GetChar(Console.ReadLine());
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine($"{e.InnerException}");
                return 'Z';
            }
         
        }

        //Return the set timer limit
        public static int GetTimer()
        {
            Console.Write($"Return time (1-5mins) = ");

            try
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        return 60000;
                    case "2":
                        return 120000;
                    case "3":
                        return 180000;
                    case "4":
                        return 240000;
                    case "5":
                        return 300000;
                    default:
                        return 60000;

                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message} \n {e.InnerException}");
                return int.Parse("60000");
            }
           
        }

        //Return the game score in the gameseqtarget option
        public static int GetGameScore()
        {
            Console.Write($"Return Game Score = ");

            try
            {
                int rtngamescre = Convert.ToInt32(Console.ReadLine());

                if(rtngamescre < 1)
                { 
                    return int.Parse("5");
                }
                else
                {
                    return rtngamescre;
                }

            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message} {e.InnerException}");
                return int.Parse("5");
            }
    
        }

        //GetNewGameOrRestoreGame
        public static char GetNewRes()
        {
            // S(save) R(restore)
            Console.Write($"Start new game or restore = ");
            try
            {
                return GetChar(Console.ReadLine());
            }

            catch(Exception e)
            { 
                Console.WriteLine($"{e.Message}");
                Console.WriteLine($"{e.InnerException}");
                return 'N';
            }
            
        }

        public static char GetChar(string x)
        {
            try
            {
                return x.ToCharArray()[0].ToString().ToUpper().First();
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine($"{e.InnerException}");
                return char.ToUpper(x.First());
            }
          
        }

    }
}
