using System;

namespace Lab2.Individual.Register
{
    class FlatInfo
    {
        public int ApartmentNumber { get; set; }
        public int Area { get; set; }
        public int RoomCount { get; set; }
        public decimal Price { get; set; }
        public long PhoneNumber { get; set; }

        /// <summary>
        /// Initializes a new instance of the FlatInfo class.
        /// </summary>
        /// <param name="apartmentNumber">The number of the apartment.</param>
        /// <param name="area">The area of the apartment in square meters.</param>
        /// <param name="roomCount">The number of rooms in the apartment.</param>
        /// <param name="price">The price of the apartment.</param>
        /// <param name="phoneNumber">The contact phone number.</param>
        public FlatInfo(int apartmentNumber, int area, int roomCount, decimal price, long phoneNumber)
        {
            this.ApartmentNumber = apartmentNumber;
            this.Area = area;
            this.RoomCount = roomCount;
            this.Price = price;
            this.PhoneNumber = phoneNumber;
        }
    }
}
