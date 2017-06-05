using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTest.Definitions
{
    public class Pet
    {
        public string name { get; set; }
        public string type { get; set; }

        public override bool Equals(object obj)
        {
            var pet = obj as Pet;

            return (pet.name == name
                && pet.type == type);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
