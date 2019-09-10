using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReturnChar
{
    class EntryObjectStream
    {
        public static List<EntryObject> Dictionary = new List<EntryObject>();
        public static string StreamPath = "";
        public static char[] commadelim = { ',' };
        public static char[] semicolon = { ';' };

        public EntryObjectStream() { }

        public static List<EntryObject> GetDictionary()
        {
            return Dictionary;
        }

        public static string GetReadFromStreamPath()
        {
            return StreamPath;
        }

        //Set the gamelist path
        public static void SetStreamPath(string x)
        {
            StreamPath = x.ToLower();
        }

        //Build the dictionary
        public static void SetDictionary()
        {
            Dictionary.Clear();

            if (File.Exists(GetReadFromStreamPath()))
            {
                try
                {
                    using (StreamReader aStreamR = new StreamReader(GetReadFromStreamPath()))
                    {
                        while (aStreamR.Peek() > -1)
                        {

                            List<string> aReadLine = aStreamR.ReadLine().Split(commadelim).ToList();
                            var namelist = aReadLine[1].Split(semicolon).ToList();
                            var typelist = aReadLine[2].Split(semicolon).ToList();
                            var wherelist = aReadLine[3].Split(semicolon).ToList();
                            var extralist = aReadLine[4].Split(semicolon).ToList();

                            EntryObject entryobject = new EntryObject(Convert.ToInt32(aReadLine[0]), namelist, typelist, wherelist, extralist);
                            Dictionary.Add(entryobject); // Add the entry object to the List<EntryObjects>
                        }
                    }
                }
                catch (SystemException sysexc) { Console.WriteLine($"Book -> GetDictionary() + {sysexc.Message}\n{sysexc.InnerException}\nApp shutdown"); Environment.Exit(Environment.ExitCode);  }
                catch (Exception exc) { Console.WriteLine($"Book -> GetDictionary() + {exc.Message}\n{exc.InnerException}\nApp shutdown"); Environment.Exit(Environment.ExitCode); }
            }
            else
            {
                //If the files doesn't exist we want to return to the main menu
                Console.WriteLine($"EntryObjectStream -> SetDictionary()"); Console.WriteLine($"File does not exist.");
                Console.ReadKey();
                Menu.DisplayMenuOptions();
            }


        }
    }
}