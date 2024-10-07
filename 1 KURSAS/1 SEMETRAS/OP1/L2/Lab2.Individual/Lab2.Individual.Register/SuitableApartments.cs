using System;
using System.Collections.Generic;

namespace Lab2.Individual.Register
{
    class SuitableApartments
    {
        // List to hold suitable apartments and their corresponding floors
        public List<(FlatInfo Apartment, int Floor)> Apartments { get; private set; }

        /// <summary>
        /// Initializes a new instance of the SuitableApartments class.
        /// </summary>
        public SuitableApartments()
        {
            Apartments = new List<(FlatInfo, int)>();
        }

        /// <summary>
        /// Adds a suitable apartment and its floor to the list.
        /// </summary>
        /// <param name="flat">The FlatInfo object representing the apartment.</param>
        /// <param name="floor">The floor number where the apartment is located.</param>
        public void Add(FlatInfo flat, int floor)
        {
            Apartments.Add((flat, floor)); // Store the apartment and its floor
        }
    }
}
