using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnChar
{
    class EntryObject
    {
        //Properties
        public  int EntryID { get; set; }
        public  List<string> NameList { get; set; }
        public  List<string> TypeList { get; set; }
        public  List<string> WhereList { get; set; }
        public  List<string> ExtraList { get; set; }

        //Constructor 
        //Init' property values 

        public EntryObject()
        {
            EntryID = -1;
            NameList.Add("_null");
            TypeList.Add("_null");
            WhereList.Add("_null");
            ExtraList.Add("_null");

        }

        public EntryObject(int a, List<string> b, List<string> c, List<string> d, List<string> e)
        {
            EntryID = a;
            NameList = b;
            TypeList = c;
            WhereList = d;
            ExtraList = e;
        }


    }
}
