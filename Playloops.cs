using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Media;
using System.IO;

namespace ReturnChar
{
    class Playloops
    {
        public static bool IsSoundPlaying;
        public static SoundPlayer soundfileobj;
        
        public Playloops()
        {
            IsSoundPlaying = false;
        }

        public static void SetSoundPath()
        {
            const string filepath = "C:/program files/returnchar/sounds/";
            Menu.EnumDirs("C:/program files/returnchar/sounds/");
            Console.WriteLine($"Current sound filepath = {filepath}");

            Console.Write("Input sound filepath = "); var input = filepath+Console.ReadLine();

            try
            {
                soundfileobj = new SoundPlayer(input);
            }
            catch (FileNotFoundException fnfexc) { Console.WriteLine($"{fnfexc.Message}"); Console.WriteLine($"File not found"); Console.ReadLine(); Menu.DisplayMenuOptions(); }
            catch (Exception exc) { Console.WriteLine($"{exc.Message}\n{exc.InnerException}"); Console.ReadLine(); Menu.DisplayMenuOptions(); }
        }

        public static SoundPlayer GetSoundPath()
        {
            return soundfileobj;
        }

        public static void PlayReturnChar()
        {
            string press = "Press enter any key to stop";
            Console.WriteLine($"{press}");
            Console.ReadLine();
            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.WriteLine($"{Player.GetAlphabet().PadLeft(50, ' ')} {Player.GetAlphabet()}  {Player.GetAlphabet()}" +
                        $"  {Player.GetAlphabet()} {Player.GetAlphabet()} {Player.GetAlphabet()} {Player.GetAlphabet()} {Player.GetAlphabet()} {Player.GetAlphabet()} " +
                        $" {Player.GetAlphabet()} {Player.GetAlphabet()} {Player.GetAlphabet()} {Player.GetAlphabet()} {Player.GetAlphabet()}");
                    Thread.Sleep(35);
                }
            }

            while (!Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar);
                //if(Console.ReadKey().Key == ConsoleKey.A) { break;  }
            
        }

        public static void PlaySound()
        {
            try
            {
                Console.WriteLine("1.Play 2.Stop");
                string mode = Console.ReadLine();

                //SoundPlayer soundobject = GetSoundFile();

                switch (mode)
                {
                    case "1":
                        SetSoundPath();

                        GetSoundPath().Load();
                        if (GetSoundPath().IsLoadCompleted)
                        {
                            GetSoundPath().PlayLooping();
                            IsSoundPlaying = true;
                        }
                        else
                        {
                            Console.WriteLine("Failed to load sound");
                            IsSoundPlaying = false;
                        }
                        break;
                    case "2":
                        if (IsSoundPlaying == false)
                        {
                            Console.WriteLine("Play sound before stopping it");
                        }
                        else if (IsSoundPlaying == true)
                        {
                            GetSoundPath().Stop();
                            IsSoundPlaying = false;

                        }
                        else
                        {
                            Console.WriteLine("Failed to load sound function \"2\" ");
                        }
                        break;
                    default:
                        Console.WriteLine("Failed to load sound function");
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.InnerException}");
                Console.ReadLine();
                Menu.DisplayMenuOptions();

            }

        }

    }
}
