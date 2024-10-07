using System;
using System.Collections.Generic;

namespace Lab2.Individual.Register
{
    class FlatRegister
    {
        private List<FlatInfo> allFlats; // List to hold all apartments

        /// <summary>
        /// Initializes a new instance of the FlatRegister class.
        /// </summary>
        public FlatRegister()
        {
            allFlats = new List<FlatInfo>();
        }

        /// <summary>
        /// Adds a flat to the list of all flats.
        /// </summary>
        /// <param name="flat">The FlatInfo object representing the apartment.</param>
        public void AddFlat(FlatInfo flat)
        {
            allFlats.Add(flat); // Add the flat to the list
        }

        /// <summary>
        /// Calculates the floor number based on the apartment number.
        /// </summary>
        /// <param name="apartmentNumber">The number of the apartment.</param>
        /// <returns>The floor number where the apartment is located.</returns>
        private int FindFloor(int apartmentNumber)
        {
            return ((apartmentNumber - 1) % 27) / 3 + 1; // Calculate the floor
        }

        /// <summary>
        /// Finds suitable apartments based on the client's requirements.
        /// </summary>
        /// <param name="clientRequirements">The requirements set by the client.</param>
        /// <returns>A SuitableApartments object containing apartments that meet the criteria.</returns>
        public SuitableApartments FindApartment(ClientRequirements clientRequirements)
        {
            SuitableApartments suitableApartment = new SuitableApartments();

            // Iterate through all flats to find suitable ones
            foreach (FlatInfo flat in allFlats)
            {
                int floor = FindFloor(flat.ApartmentNumber); // Determine the floor for the flat
                // Check if flat meets all client requirements
                if ((flat.Price <= clientRequirements.MaxPrice) &&
                    (flat.RoomCount == clientRequirements.HowManyRooms) &&
                    (floor >= clientRequirements.LowestFloor) &&
                    (floor <= clientRequirements.HighestFloor))
                {
                    suitableApartment.Add(flat, floor); // Add flat and floor if suitable
                }
            }

            return suitableApartment; // Return the list of suitable apartments
        }
    }
}
