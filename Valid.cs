using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReturnChar
{
 
    // Validating the player input with the list
    // Try catch {} exception handling
    class Valid
    {
        public string AnsInvalid;
        public string PlayerAnsValid;

        public Book ValidBook = new Book();
        public static List<Book> ValidDictionary = new List<Book>();
        public static List<Book> SelectedAnsBooks = new List<Book>(); 

        private bool IsValid { get; set; }
        private bool BookContains { get; set; }

        public Valid()
        {
            IsValid = false;
            AnsInvalid = "{ Answer is invalid }\n"; // answer already selected, empty input or tocase/twostr==false. 
            PlayerAnsValid = "\n{ Answer valid BookID added, player+1}";
        }

        //ValidDictAns ListBooks is reduced over the course of the game
        public static void SetValidDictionary(List<Book> x)
        {
            ValidDictionary = x;
        }

        public static List<Book> GetValidDictionary()
        {
            return ValidDictionary;
        }

        public bool ValidComputerName(string computerinput)
        {
            IsValid = false;
            List<Book> aListOfBooks = GetValidDictionary();

            for(int i =0; i < aListOfBooks.Count(); i++)
            {
                if (TwoStr(computerinput, aListOfBooks[i].Name))
                {
                    //Validated computer input which is to be expected since the computer derives its list from dictans
                    SetSelectedAnsBooks(aListOfBooks[i]); GetValidDictionary().Remove(aListOfBooks[i]); IsValid = true; break;
                    
                }
            }
            
            //if(IsValid == false) { Console.WriteLine($"{AnsInvalid}"); }

            return IsValid;

        }

        //for easy mode
        public bool ValidName(string playerinput)
        {
           
            IsValid = false;
            bool IsSearch = true;
            int i = 0;
            List<Book> aListOfBooks = GetValidDictionary();

            try
            {
                if (!string.IsNullOrEmpty(playerinput.Trim()))
                {
                    while (i < aListOfBooks.Count() && IsSearch)
                    {
                        if (TwoStr(playerinput, aListOfBooks[i].Name))
                        {
                            Console.WriteLine($"{PlayerAnsValid}");
                            SetSelectedAnsBooks(aListOfBooks[i]); GetValidDictionary().Remove(aListOfBooks[i]); IsValid = true; IsSearch = false;
                        }
                        else
                        {
                            for (int j = 0; j < aListOfBooks[i].NameAssoc.Count(); j++) // Cycle through the name associations
                            {
                                if (TwoStr(playerinput, aListOfBooks[i].NameAssoc[j]))
                                {
                                    Console.WriteLine($"{PlayerAnsValid}");
                                    SetSelectedAnsBooks(aListOfBooks[i]); GetValidDictionary().Remove(aListOfBooks[i]); IsValid = true; IsSearch = false;
                                }
                            }

                        }
                        i++;
                    }
                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.InnerException}");
            }
             
            //if(IsValid == false) Console.WriteLine($"{AnsInvalid}");

            return IsValid;
        }

        //for medium & hard
        public bool ValidNameTypeWhere(List<string> playerinputlist)
        {
            List<Book> aListOfBooks = GetValidDictionary(); //List of books
            int i = 0; // A counter
            IsValid = false; //bool 

            if (playerinputlist.Any())
            {
                while (i < aListOfBooks.Count())
                {
                    if (GetContainsTrue(aListOfBooks, playerinputlist, i)) //Tests for the associations
                    {
                        Console.WriteLine($"{PlayerAnsValid}"); SetSelectedAnsBooks(aListOfBooks[i]); GetValidDictionary().Remove(aListOfBooks[i]); IsValid = true; break;
                    }

                    i++;

                }
            }
            //if (IsValid == false) Console.WriteLine($"{AnsInvalid}");
            return IsValid;
        }
        
        //for super
        public bool ValidNameTypeWhereExtra(List<string> playerinputlist)
        {
            //river lengths, mountain heights
            List<Book> aListOfBooks = GetValidDictionary();
            int i = 0; // A counter
            IsValid = false; //bool

            if (playerinputlist.Any())
            {
                while (i < aListOfBooks.Count())
                { 
                    if (GetContainsTrue(aListOfBooks, playerinputlist, i)) //Tests for the name type where 
                    {
                        //Console.WriteLine("gamelist extra is not a digit
                        if (ExtraListAssoc(aListOfBooks[i], playerinputlist[3]))
                        {
                            Console.WriteLine($"{PlayerAnsValid}"); SetSelectedAnsBooks(aListOfBooks[i]); GetValidDictionary().Remove(aListOfBooks[i]); IsValid = true; break;
                        }
       
                    }

                    i++;
                }
            }

            // (IsValid == false) Console.WriteLine($"{AnsInvalid}");

            return IsValid;
        }

        //If the ith ValidDictAns contains elements in it's sequence
        public bool ExtraListAssoc(Book aBook, string playerinputlistextra)
        {
            var y = false;

            if (aBook.ExtraList.Any())
            {
                for (int z = 0; z < aBook.ExtraList.Count(); z++)
                {
                    if (TwoStr(aBook.ExtraList[z], playerinputlistextra))
                    {
                        y = true; break;
                    }

                }
            }
      
            return y;
        }
        
        //Cycle through the Name type and where associations comparing the kthassoc with an input indexed by a list of strings from the user
        public bool GetContainsTrue(List<Book> books, List<string> playerinput, int ithBook)
        {
            int counter = 0;

            //If the ith book (Assoc list) contains any elements
            if (books[ithBook].NameAssoc.Any())
            {
                //Cycle through the ith book
                for (int kthAssoc = 0; kthAssoc < books[ithBook].NameAssoc.Count(); kthAssoc++)
                {
                    //Compare the kth nameassociation with the first element of the player input (name)
                    if (TwoStr(books[ithBook].NameAssoc[kthAssoc], playerinput[0])) counter += 1;
                }
            }

            //TypeAssoc
            if (books[ithBook].TypeAssoc.Any())
            {
                for (int kthAssoc = 0; kthAssoc < books[ithBook].TypeAssoc.Count(); kthAssoc++)
                {
                    //Compare the kth typeassocation with the second element of the player input (type)
                    if (TwoStr(books[ithBook].TypeAssoc[kthAssoc], playerinput[1])) counter += 1;
                }
            }

            //WhereAssoc
            if (books[ithBook].WhereAssoc.Any())
            {
                for (int kthAssoc = 0; kthAssoc < books[ithBook].WhereAssoc.Count(); kthAssoc++)
                {
                    //Compare the kth whereassocation with the third element of the player input (where)
                    if (TwoStr(books[ithBook].WhereAssoc[kthAssoc], playerinput[2])) counter += 1;
                }
            }
            
            if (counter == 3) return true; else return false;

        }

        // Cycle through the name assoc comparing the kthassoc " " 
        public bool ValidNameTrue(List<Book> books, int ithBook, string playerinput)
        {
            bool nameassocx = false;

            //If the ith book (Assoc list) contains any elements
            if (books[ithBook].NameAssoc.Any())
            {
                //Cycle through the ith book
                for (int kthAssoc = 0; kthAssoc < books[ithBook].NameAssoc.Count(); kthAssoc++)
                {
                    //Compare the kth nameassociation with the first element of the player input (name)
                    if (TwoStr(books[ithBook].NameAssoc[kthAssoc], playerinput)) nameassocx = true; kthAssoc = books[ithBook].NameAssoc.Count();
                }

                if (nameassocx == true) return true; else return false;
            }
            else
            {
                return false;
            }

       
        }
        //Display selected answers
        public static void DisplaySelectedBooks()
        {
            int counter = 0;

            foreach(Book x in GetSelectedBooks())
            {
                counter += 1;
                Console.WriteLine($"ID = {x.BookID}, Name = {x.Name}, Type = {x.Type}, Where = {x.Where}");
            }

            Console.WriteLine($"Total number of selected answers = {counter}");
        }

        //Book
        public bool AnyBookContains(Book abook)
        {
            BookContains = false;

            for(int i=0;i<GetSelectedBooks().Count();i++)
            {
                if (ValidBook.IsBookEqual(abook,GetSelectedBooks()[i]))
                {
                    BookContains = true;
                }

            }

            return BookContains;
        }

        //Book
        public static void SetSelectedAnsBooks(int bookid)
        {
            SelectedAnsBooks.Add(Book.GetBookID(bookid));
        }

        //Book
        public static void SetSelectedAnsBooks(Book abook)
        {
            SelectedAnsBooks.Add(abook);
        }


        //Book
        public static List<Book> GetSelectedBooks()
        {
            return SelectedAnsBooks;
        }

        //Extra as a list of strings
        public static bool IsRangeExtra(Book extra, string playerinput)
        {

            bool isdigit = false;

            if (extra.ExtraList.Any())
            {
                for(int i = 0; i < extra.ExtraList.Count(); i++)
                {
                    if(IsStringDigit(extra.ExtraList[i]) && IsStringDigit(playerinput))
                    {
                        //playerinputlist = playerinput // Extra [3]
                        double extrainp = Convert.ToInt32(extra);
                        int playerinputx = Convert.ToInt32(playerinput);
                        
                        // - .05 lowerlimit ; +.05 upperlimit
                        if (playerinputx >= extrainp - (extrainp * 0.05) && playerinputx <= extrainp + (extrainp * 0.05))
                        {
                            isdigit = true; break;
                        }
                     
                    }
                   
                }
            }
            else
            {
                return isdigit;
            }

            return isdigit;


        }

        public static bool IsStringDigit(string astring)
        {
            bool isdigit = true;

            foreach(char x in astring)
            {
                if (!char.IsDigit(x))
                {
                    isdigit = false;
                }
            }

            return isdigit;
        }

        // Compare the last char of the prev input with the first char of the current input
        // Will only compare alpha characters
        public bool ToCase(string prev, string current)
        {
            return GetInputChar(prev).LastOrDefault().Equals(GetInputChar(current).FirstOrDefault());
        }

        //Compare two strings and return true or false
        public bool TwoStr(string i, string j)
        {
            return i.ToLower().Replace(" ","").Equals(j.ToLower().Replace(" ",""));
        }

        public string GetInputChar(string input)
        {
            var string1 = "";

            for(int i=0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                { 
                    string1 += input[i];
                }
            }

            return string1.ToLower();
        }
    
    }
}
