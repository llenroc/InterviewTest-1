using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTest.Definitions
{
    public class Person
    {
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public List<Pet> pets { get; set; }

        public override bool Equals(object obj)
        {
            var person = obj as Person;

            return (person.name == name
                && person.age == age
                && person.gender == gender
                && person.pets.SequenceEqual(pets));                
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
