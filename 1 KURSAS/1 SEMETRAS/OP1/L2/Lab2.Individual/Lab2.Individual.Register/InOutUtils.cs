using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab2.Individual.Register
{
    static class InOutUtils
    {
        /// <summary>
        /// Reads apartment data from a CSV file and returns a list of FlatInfo objects.
        /// </summary>
        /// <param name="fileName">The name of the CSV file to read from.</param>
        /// <returns>A list of FlatInfo objects.</returns>
        public static List<FlatInfo> ReadApartments(string fileName)
        {
            List<FlatInfo> apartments = new List<FlatInfo>();
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);

            // Parse each line in the CSV file
            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                int apartmentNumber = int.Parse(values[0]);
                int area = int.Parse(values[1]);
                int roomCount = int.Parse(values[2]);
                decimal price = decimal.Parse(values[3]);
                long phoneNumber = long.Parse(values[4]);

                // Create a new FlatInfo object and add it to the list
                FlatInfo flat = new FlatInfo(apartmentNumber, area, roomCount, price, phoneNumber);
                apartments.Add(flat);
            }
            return apartments; // Return the list of apartments
        }
    }
}
