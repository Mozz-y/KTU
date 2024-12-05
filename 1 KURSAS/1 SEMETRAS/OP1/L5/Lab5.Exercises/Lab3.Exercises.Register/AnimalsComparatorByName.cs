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
            return a.Name.CompareTo(b.Name);
        }
    }
}
