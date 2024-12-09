using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Exercises.Register
{
    class AnimalsComparatorByBirthDate : AnimalsComparator
    {
        public override int Compare(Animal a, Animal b)
        {
            int dateComparison = a.BirthDate.CompareTo(b.BirthDate);
            if (dateComparison == 0) // Birth dates are the same
            {
                return a.ID.CompareTo(b.ID); // Compare by ID if birth dates are identical
            }
            return dateComparison;
        }
    }
}
