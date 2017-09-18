using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_slmc
{
    class Person
    {

        public string name { get; set; }

        public string surname { get; set; }

        public int age { get; set; }

        public string id { get; set; }

        public string address { get; set; }

        public string occupation { get; set; }

        public bool female { get; set; }

        public string cellphoneNumber { get; set; }
        
        public Person (string name,string sur, int age,string id,string cellph,string occptn)
        {
            this.id = id;
            this.name = name;
            this.surname = sur;
            this.age = age;
            this.cellphoneNumber = cellph;
            this.occupation = occptn;
        }


    }
}
