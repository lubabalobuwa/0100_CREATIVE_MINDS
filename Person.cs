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

        public double cellphoneNumber { get; set; }

        

        public Person (string id)
        {
            this.id = id;

        }


    }
}
