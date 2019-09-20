using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace ReturnChar
{
    class Program
    {
        static void Main(string[] args)
        {

            if (HighScorePath())
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Menu.DisplayMenuOptions();
            }
            else
            {
                Console.WriteLine("Path not set"); Console.ReadLine();
            } 
         
            

        }

        public static bool HighScorePath()
        {

            const string highscorefilepath = @"C:\program files\returnchar\highscores.csv";

            try
            {

                if (File.Exists(highscorefilepath))
                {
                    SaveRestoreFunc.SetHighScorePath(highscorefilepath);
                    Console.WriteLine($"Path successfully set");
                    Console.ReadLine();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception secexc)
            {
                Console.WriteLine($"{secexc.Message}");
                return false;
            }
            
        }
    }
}