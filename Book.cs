using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ReturnChar
{
    class Book
    {
        public static Valid ValidBook = new Valid();
        public static Random rand = new Random();
        public static char[] delim = { ',' };

        //public static List<Book> DictAns { get; set; }
        //public static List<Book> ComStateList { get; set; }

        public static List<Book> ComStateList = new List<Book>();
        public static List<Book> Dictionary = new List<Book>();

        public int BookID { get; set; } // 0 .BookID
        public string Name { get; set; } // 1 .Name
        public string Type { get; set; } // 2 .Type
        public string Where { get; set; } // 3 .Where
        public List<string> NameAssoc { get; set; } // 4 <- Name
        public List<string> TypeAssoc { get; set; } // 5 <- Type
        public List<string> WhereAssoc { get; set; } // 6 <- Where
        public List<string> ExtraList { get; set; } // 7 <- Extra

        public static bool BoolBookEqual;
        private static string ReadFromStream;

        //The case of dictionary being set everytime the constructor is called
        // GetDictionary()?
        public Book() { }

        static Book()
        {
            ReadFromStream = "";
            ComStateList = new List<Book>();
            Dictionary = new List<Book>();

        }

        public static void SetReadFromStream(string X)
        {
            ReadFromStream = X.ToLower();
        }

        public static string GetReadFromStream()
        {
            return ReadFromStream;
        }

        public static List<Book> GetComStateList()
        {
            return ComStateList;
        }

        public static List<Book> GetDictionary()
        {
            return Dictionary;
        }

        //Generate a Dictionary list
        public static void SetDictionary()
        {
            Dictionary.Clear();

            if (File.Exists(GetReadFromStream()))
            {
                try
                {
                    using (StreamReader aStreamReader = new StreamReader(GetReadFromStream()))
                    {
                        while (aStreamReader.Peek() > -1)
                        {
                            var tempArray = aStreamReader.ReadLine().Split(delim).ToList();

                            if (tempArray.Count() == 8)
                            {
                                Dictionary.Add(new Book
                                {      
                                    BookID = Convert.ToInt32(tempArray[0].Replace(" ", "")),
                                    Name = tempArray[1].Replace(" ", ""),
                                    Type = tempArray[2].Replace(" ", ""),
                                    Where = tempArray[3].Replace(" ", ""),
                                    NameAssoc = tempArray[4].Replace(" ", "").Split(';').ToList(),
                                    TypeAssoc = tempArray[5].Replace(" ", "").Split(';').ToList(),
                                    WhereAssoc = tempArray[6].Replace(" ", "").Split(';').ToList(),
                                    ExtraList = tempArray[7].Replace(" ", "").Split(';').ToList()
                                });
                            }
                        }
                    }
                }
                catch (StackOverflowException stackexc)
                {
                    Console.WriteLine($"Book -> GetDictionary() + {stackexc.Message}\n{stackexc.InnerException}\nApp shutdown"); Environment.Exit(Environment.ExitCode);
                }
                catch (SystemException sysexc)
                {
                    Console.WriteLine($"Book -> GetDictionary() + {sysexc.Message}\n{sysexc.InnerException}\nApp shutdown"); Environment.Exit(Environment.ExitCode);
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"Book -> GetDictionary() + {exc.Message}\n{exc.InnerException}\nApp shutdown");Environment.Exit(Environment.ExitCode);
                }

            }
            else
            {
                Console.WriteLine($"Book -> GetDictionary()"); Console.WriteLine($"File does not exist.");
                Console.ReadKey();
                Menu.DisplayMenuOptions();
            }

            
        }

        //Return ComStateList
        public List<Book> GetGameArray(char difflvl)
        {
            switch (difflvl.ToString().ToLower())
            {
                case "e":
                    if (GetComStateListEasy()) { return ComStateList; } else { return null; }
                case "m":
                    if (GetComStateListMedium()) { return ComStateList; } else { return null; }
                case "h":
                    if (GetComStateListHard()) { return ComStateList; } else { return null; }
                case "s":
                    if (GetComStateListSuper()) { return ComStateList; } else { return null; }
                default:
                    return null;
            }
        }

        //Set ComStateList
        public bool GetComStateListEasy()
        {
        
            try
            {
                ComStateList.Clear();
                var DictAns = GetDictionary();
                var tempIntList = new List<int>();

                if (DictAns.Count() < 100 || DictAns.Count() > 300)
                {
                    Console.WriteLine($"Length of dictionary < 100 || > 300");
                    return false;
                }

                //A limit minimum on the size of a list?
                for (int i = 0; i < 10; i++)
                {
                    var temp_var = rand.Next(DictAns.Count());
                    while (tempIntList.Contains(temp_var))
                    {
                        temp_var = rand.Next(DictAns.Count());
                    }

                    ComStateList.Add(DictAns[temp_var]);
                    tempIntList.Add(temp_var);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Book -> GetComStateListEasy() + {e.Message}\n{e.InnerException}");
                return false;   
            }
        }

        public bool GetComStateListMedium()
        {
            // name, the type and where
            try
            {
                ComStateList.Clear();
                var DictAns = GetDictionary();
                var tempIntList = new List<int>();

                if (DictAns.Count() < 100 || DictAns.Count() > 300)
                {
                    Console.WriteLine($"Length of dictionary < 100 || > 300");
                    return false;
                }

                for (int i = 0; i < 30; i++)
                {
                    var temp_var = rand.Next(DictAns.Count());
                    while (tempIntList.Contains(temp_var))
                    {
                        temp_var = rand.Next(DictAns.Count());
                    }

                    ComStateList.Add(DictAns[temp_var]);
                    tempIntList.Add(temp_var);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Book -> GetComStateListMedium() + {e.Message}\n{e.InnerException}");
                return false;
            }
 
        }

        public bool GetComStateListHard()
        {
            //public List<string> GetComStateList(string randChoice)
            //countries, continents + more
            // name, the type and where

            // if x % 2 = 0; even number // as many 'A's as 'B's etc
            // Will com1 select a _minimum _even number from each List; 'Rivers' as 'Mountains' etc or a _range?
            // RiversCounter += 1, Mountains +=1 ,  x+=1 

            try
            {
                ComStateList.Clear();
                var DictAns = GetDictionary();
                var tempIntList = new List<int>();

                if (DictAns.Count() < 100 || DictAns.Count() > 300)
                {
                    Console.WriteLine($"Length of dictionary < 100 || > 300");
                    return false;
                }

                for (int i = 0; i < Convert.ToInt32(0.50 * DictAns.Count()); i++)
                {
                    var temp_var = rand.Next(DictAns.Count());
                    while (tempIntList.Contains(temp_var))
                    {
                        temp_var = rand.Next(DictAns.Count());
                    }

                    ComStateList.Add(DictAns[temp_var]);
                    tempIntList.Add(temp_var);
                }

                return true;
            }catch(Exception e)
            {
                Console.WriteLine($"Book -> GetComStateListHard()");
                Console.WriteLine($"{e.Message}\n{e.InnerException}");
                return false;
            }

        }

        public bool GetComStateListSuper()
        {

            try
            {
                ComStateList.Clear();
                var DictAns = GetDictionary();
                var tempIntList = new List<int>();

                if (DictAns.Count() < 100 || DictAns.Count() > 300)
                {
                    Console.WriteLine($"Length of dictionary < 100 || > 300");
                    return false;
                }

                for (int i = 0; i < Convert.ToInt32(0.75 * DictAns.Count()); i++)
                {
                    var temp_var = rand.Next(DictAns.Count());
                    while (tempIntList.Contains(temp_var))
                    {
                        temp_var = rand.Next(DictAns.Count());
                    }

                    ComStateList.Add(DictAns[temp_var]);
                    tempIntList.Add(temp_var);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.InnerException}");
                return false;
            }

        }

        public static Book GetBookID(int bookid)
        {
           
            Book newbook = new Book();
            var book = GetDictionary();

            foreach (Book x in book)
            {
                if (x.BookID == bookid)
                {
                    newbook = x;
                }
            }

            return newbook;
        }

        public static void GetLengthOfBook(List<Book> x)
        {
            Console.WriteLine($"Length of Book = {x.Count()}");
        }

        public static void GetLengthOfListBooks(List<Book> x)
        {
            Console.WriteLine($"Length of list of books = {x.Count()}");
        }

        //Return a default list of books with 1 book
        public List<Book> GetDefaultBooks()
        {
            List<string> newlist = new List<string>(); newlist.Add("default");
            List<Book> newbooks = new List<Book>();

            int j = 0;

            for(int i = 0; i < 5; i++)
            { 
                j--; //decrement 5 times
                newbooks.Add(new Book { BookID = j, Name = "default", Type = "default", Where = "default", NameAssoc = newlist, TypeAssoc = newlist, WhereAssoc = newlist, ExtraList = newlist });
            }

            return newbooks;

            //return new List<Book> {new Book { BookID = -1, Name = "_default", Type = "_default", Where = "_default", NameAssoc = newlist, TypeAssoc = newlist, WhereAssoc = newlist, ExtraList = newlist }};
        }

        //Return a book
        public Book GetDefaultBook()
        {
            List<string> newlist = new List<string>();newlist.Add("_default");

            return new Book { BookID = -1, Name = "_default", Type = "_default", Where = "_default", NameAssoc = newlist, TypeAssoc = newlist, WhereAssoc = newlist, ExtraList = newlist };
        }

        //Display/Write all properties within a book
        public static void DisplayBook(Book abook)
        {
            try
            {
                Console.WriteLine($"BookID = {abook.BookID}, Name = {abook.Name}, Type = {abook.Type}, Where = {abook.Where}");

                if (abook.NameAssoc.Any())
                {
                    for (int i = 0; i < abook.NameAssoc.Count(); i++)
                    {
                        Console.WriteLine($"Name Assoc = {abook.NameAssoc[i]}");
                    }
                }

                if (abook.TypeAssoc.Any())
                {
                    for (int i = 0; i < abook.TypeAssoc.Count(); i++)
                    {
                        Console.WriteLine($"Type Assoc = {abook.TypeAssoc[i]}");
                    }
                }

                if (abook.WhereAssoc.Any())
                {
                    for (int i = 0; i < abook.WhereAssoc.Count(); i++)
                    {
                        Console.WriteLine($"Where Assoc = {abook.WhereAssoc[i]}");
                    }
                }

                if (abook.ExtraList.Any())
                {
                    for (int i = 0; i < abook.ExtraList.Count(); i++)
                    {
                        Console.WriteLine($"Extra Assoc = {abook.ExtraList[i]}");
                    }
                }
            }
            catch (ArgumentNullException argnull)
            {
                Console.WriteLine($"Book -> DisplayBook() + ArgumentNullExc + {argnull.Message} \n {argnull.InnerException}");

            }catch(Exception e)
            {
                Console.WriteLine($"Book -> DisplayBook() + {e.Message} \n {e.InnerException}");
                Console.WriteLine($"{e.InnerException}");

            }

        }

        //Display/Write all properties of all books within a list //Call the displaybook method to display each book
        public static void DisplayBooks(List<Book> dictansbooks)
        {
            try
            {
                foreach (Book x in dictansbooks)
                {
                    DisplayBook(x);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Book -> DisplayBooks(List<Book>) + {e.Message}\n{e.InnerException}");
            }
           
        }

        //Compare two books and their properties if two properties are not equal bool returns false
        public bool IsBookEqual(Book abook, Book abook2)
        {
            try
            {
                BoolBookEqual = true;

                if (abook.BookID != abook2.BookID) { BoolBookEqual = false; }
                if (!abook.Name.Equals(abook2.Name)) { BoolBookEqual = false; }
                if (!abook.Type.Equals(abook2.Type)) { BoolBookEqual = false; }
                if (!abook.Where.Equals(abook2.Where)) { BoolBookEqual = false; }
              
                //Take the length of the abook assoc
                if (abook.NameAssoc.Any() && abook.NameAssoc.Count() == abook2.NameAssoc.Count())
                {
                    for (int i = 0; i < abook.NameAssoc.Count(); i++)
                    {
                        if (!abook.NameAssoc[i].Equals(abook2.NameAssoc[i]))
                        {
                            BoolBookEqual = false;
                        }
                    }
                }

                if (abook.TypeAssoc.Any() && abook.TypeAssoc.Count() == abook2.TypeAssoc.Count())
                {
                    for (int i = 0; i < abook.TypeAssoc.Count(); i++)
                    {
                        if (!abook.TypeAssoc[i].Equals(abook2.TypeAssoc[i]))
                        {
                            BoolBookEqual = false;
                        }
                    }
                }

                if (abook.WhereAssoc.Any() && abook.WhereAssoc.Count() == abook2.WhereAssoc.Count())
                {
                    for (int i = 0; i < abook.WhereAssoc.Count(); i++)
                    {
                        if (!abook.WhereAssoc[i].Equals(abook2.WhereAssoc[i]))
                        {
                            BoolBookEqual = false;
                        }
                    }
                }

                //Any elements in the extra list and the count of abook list and the cnt of abook2 list are equal
                if (abook.ExtraList.Any() && abook.ExtraList.Count() == abook2.ExtraList.Count())
                {
                    for( int i = 0; i < abook.ExtraList.Count(); i++)
                    {
                        if (!abook.ExtraList[i].Equals(abook2.ExtraList[i]))
                        {
                            BoolBookEqual = false;
                        }
                    }
                }


            }
            catch (ArgumentNullException argnull)
            {
                Console.WriteLine("Book -> IsBookEqual()");
                Console.WriteLine($"One or more properties equal null");
                Console.WriteLine($"{argnull.Message}");
            }
            catch(Exception e)
            {
                Console.WriteLine("Book -> IsBookEqual()");
                Console.WriteLine($"{e.Message}\n{e.InnerException}");
            }

            return BoolBookEqual;

        }

   
    }
}
