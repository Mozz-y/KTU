using System;

namespace Lab2.Individual.Register
{
    class ClientRequirements
    {
        public decimal MaxPrice { get; set; }
        public int HowManyRooms { get; set; }
        public int LowestFloor { get; set; }
        public int HighestFloor { get; set; }

        /// <summary>
        /// Initializes a new instance of the ClientRequirements class.
        /// </summary>
        /// <param name="maxPrice">The maximum price of the desired apartment.</param>
        /// <param name="howManyRooms">The desired number of rooms.</param>
        /// <param name="lowestFloor">The lowest acceptable floor.</param>
        /// <param name="highestFloor">The highest acceptable floor.</param>
        public ClientRequirements(decimal maxPrice, int howManyRooms, int lowestFloor, int highestFloor)
        {
            this.MaxPrice = maxPrice;
            this.HowManyRooms = howManyRooms;
            this.LowestFloor = lowestFloor;
            this.HighestFloor = highestFloor;
        }
    }
}
