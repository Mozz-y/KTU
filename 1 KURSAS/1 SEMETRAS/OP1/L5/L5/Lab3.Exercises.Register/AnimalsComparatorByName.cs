using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Exercises.Register
{
    class AnimalsComparatorByName : AnimalsComparator
    {
        public override int Compare(Animal a, Animal b)
        {
            int nameComparison = a.Name.CompareTo(b.Name);
            if (nameComparison == 0) // Names are the same
            {
                return a.ID.CompareTo(b.ID); // Compare by ID if names are identical
            }
            return nameComparison;
        }
    }
}
