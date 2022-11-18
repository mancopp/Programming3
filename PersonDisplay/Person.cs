using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDisplay
{
    public class Person
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string pesel;

        public Person()
        {
            this.firstName = "Jan";
            this.lastName = "Kowalski";
        }
        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public Person(Person osoba)
        {
            this.firstName = osoba.firstName;
            this.lastName = osoba.lastName;
        }
    }
}
