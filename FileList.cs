using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnChar
{
    class FileList
    {

        // ID = [0], Name = [1]
        // A limit min. on the size of a list
        public static List<string> FileListDictAns { get; set; }
        public static List<string> tempdictans;
        public static Valid FileListValid = new Valid();

        public static int numbercount;

        public FileList(){ numbercount = 0; }


        public static List<string> ReadAStream(string ReadPath)
        {
            tempdictans = new List<string>();

            try
            {
                using (StreamReader aSteamReader = new StreamReader(ReadPath))
                {
                    while (aSteamReader.Peek() > -1)
                    {
                        tempdictans.Add(aSteamReader.ReadLine());
                    }
                }

                return FileListDictAns = tempdictans;
            }
            catch (Exception e)
            {
                Console.WriteLine($"FileList -> ReadAStream(string rp) + {e.Message}\n{e.InnerException}");
                return FileListDictAns = tempdictans;
                //Environment.Exit(Environment.ExitCode);
            }

        }

        public List<int> ReturnRepeatedEntry(List<string> tempdictans)
        {

            List<int> oneset = new List<int>();
            List<string> listofbookids = new List<string>();
            List<string> listofbooknames = new List<string>();

            try

            {
                foreach (string x in tempdictans)
                {
                    var array = x.Split(',');

                    if (listofbooknames.Contains(array[1]+array[2]+array[3])) // 0 ID, 1 Name type and where
                    {
                        listofbookids.Add(array[0]);
                    }
                    else
                    {
                        listofbooknames.Add(array[1]+array[2]+array[3]);
                    }

                }


            }
            catch (Exception e)
            {

                Console.WriteLine($"FileList -> FindingRepeatedEntry() {e.Message}\n{e.InnerException}");

            }


            try
            {
                foreach (string i in listofbookids)
                {

                    oneset.Add(Convert.ToInt32(i));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"FileList -> FindingRepeatedEntry() {e.Message}\n{e.InnerException}");
            }


            return oneset;
        }

        //Write a list of Book.IDs
        public void DisplayRepeatedEntry(List<string> tempdictans)
        {
            List<string> listofbooksids = new List<string>(); // add a Book.ID to this list
            List<string> listofbooknames = new List<string>(); // add a Name to this list

            int number = 0; //initialise a count 

            //foreach string in a list of strings
            foreach (string x in tempdictans)
            {
                var array = x.Split(','); // split the string into a string array

                //If list of names contains this name
                if (listofbooknames.Contains(array[1]+array[2]+array[3])) // 0 ID, 1 Name
                {
                    listofbooksids.Add(array[0]); // Add a book ID
                }
                else
                {

                    listofbooknames.Add(array[1]+array[2]+array[3]); //Add a name to the list of names
                }

            }

            //foreach Book.ID in list of ids
            foreach (string i in listofbooksids)
            {

                number += 1; //increment a count
                Console.WriteLine($"{i} {number}"); // write the id and the count to the screen

            }
        }

        public static void UpdateEntryZero(string read, string write)
        {
            var tempdictans = ReadAStream(read);

            using (StreamWriter astreamwrite = new StreamWriter(write))
            {
                int count = 0;
                foreach (string x in tempdictans)
                {
                    string[] aRow = x.Split(',');

                    if (!aRow[1].Equals("City"))
                    {
                        
                        //ID name type where nameassoc typeassc whereassc extra
                        astreamwrite.WriteLine($"{count},{FileListValid.GetInputChar(aRow[1])},{aRow[3]},{FileListValid.GetInputChar(aRow[6])}, {"_null"}, {"_null"}, {"_null"}, {"_null"}");
                        count++;
                    }
                    

                }
            }
        }

        public static void UpdateEntryOne(string ReadPath, string WritePath)
        {
            var tempdictans = ReadAStream(ReadPath);
            int i;
            List<string> inputlist = new List<string>();
            string[] fields = { "name", "type", "where", "nameassoc", "typeassoc", "whereassoc", "extra" };
            Console.WriteLine($"Length of list (.csv) = {tempdictans.Count()}");

            try
            {
                using (StreamWriter stream = new StreamWriter(WritePath))
                {

                    for (int x = 0; x < tempdictans.Count(); x++)
                    {
                        i = 0;
                        inputlist.Clear();
                        var array = tempdictans[x].Split(',');

                        Console.WriteLine("quit and write remaining (q): ");
                        if (Console.ReadLine().Equals("q"))
                        {  
                            //Start from where we left; Cycle through the list as it's length.
                            //Index 99 still needs to be processed
                            for (int y = x; y < tempdictans.Count(); y++)
                            {
                                var arraysplit = tempdictans[y].Split(',');

                                stream.WriteLine($"{arraysplit[0]}, {arraysplit[1]}, {arraysplit[2]}, {arraysplit[3]}, {arraysplit[4]}, {arraysplit[5]}, {arraysplit[6]}, {arraysplit[7]}");
                            }

                            break;
                        }



                        for (int r = 0; r < fields.Count(); r++)
                        {
                            Console.WriteLine($"for {fields[r]} = {array[r + 1]}");
                        }

                        while (i < 7)
                        {
                            Console.Write("Update: ");
                            inputlist.Add(Console.ReadLine());
                            i++;
                        }

                        stream.WriteLine($"{array[0]}, {inputlist[0]}, {inputlist[1]}, {inputlist[2]}, {inputlist[3]}, {inputlist[4]}, {inputlist[5]}, {inputlist[6]}");
                    }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"FileList -> UpdateEntryOne(string, string) + {e.Message}\n{e.InnerException}");
            }
   
        }

        public static void GetEntry(List<string> tempdict, int i, string func, string j)
        {
            //if (j == null)
            //{
            //    throw new ArgumentNullException(nameof(j));

            //}

            int num = 0;

            foreach (string p in tempdict)
            {
                var array = p.Split(',');

                switch (func.ToLower())
                {
                    case "contains":
                        if (array[i].Trim().ToLower().Contains(j.ToLower().Trim()))
                        {
                            num++;
                            Console.WriteLine($"{p} {num}");
                            Console.WriteLine($"Index = {tempdict.IndexOf(p)}");
                            numbercount = num;
                        }
                        break;
                    case "equals":
                        if (array[i].Trim().ToLower().Equals(j.ToLower()))
                        {
                            num++;
                            Console.WriteLine($"{p} {num}");
                            Console.WriteLine($"Index = {tempdict.IndexOf(p)}");
                            numbercount = num;
                        }
                        break;
                    case "startswith":
                        if (array[i].Trim().ToLower().StartsWith(j.ToLower().Trim()))
                        {
                            num++;
                            Console.WriteLine($"{p} {num}");
                            Console.WriteLine($"Index = {tempdict.IndexOf(p)}");
                            numbercount = num;
                        }
                        break;
                    case "endswith":
                        if (array[i].Trim().ToLower().EndsWith(j.ToLower().Trim()))
                        {
                            num++;
                            Console.WriteLine($"{p} {num}");
                            Console.WriteLine($"Index = {tempdict.IndexOf(p)}");
                            numbercount = num;
                        }
                        break;
                    default:
                        if (array[i].Trim().ToLower().Contains(j.ToLower().Trim()))
                        {
                            num++;
                            Console.WriteLine($"{p} {num}");
                            Console.WriteLine($"Index = {tempdict.IndexOf(p)}");
                            numbercount = num;
                        }
                        break;
                }

            }
        }

        public int GetNumberCount()
        {
            return numbercount;
        }

    }
}

