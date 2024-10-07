using System;
using System.Collections.Generic;
using System.IO;

namespace Lab2.Individual.Register
{
    internal class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        static void Main(string[] args)
        {
            // Read the apartment data from the CSV file
            List<FlatInfo> flats = InOutUtils.ReadApartments("Apartments.csv");
            FlatRegister flatRegister = new FlatRegister();

            // Add each flat to the flat register
            foreach (FlatInfo flat in flats)
            {
                flatRegister.AddFlat(flat);
            }

            // Get client requirements from user input
            Console.WriteLine("Įveskite didžiausią pageidaujama kainą (€): ");
            decimal maxPrice = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Įveskite pageidaujama kambarių skaičių: ");
            int howManyRooms = int.Parse(Console.ReadLine());
            Console.WriteLine("Įveskite žemiausią pageidaujama aukštą: ");
            int lowestFloor = int.Parse(Console.ReadLine());
            Console.WriteLine("Įveskite aukščiausią pageidaujama aukštą: ");
            int highestFloor = int.Parse(Console.ReadLine());

            // Create client requirements object
            ClientRequirements requirements = new ClientRequirements(maxPrice, howManyRooms, lowestFloor, highestFloor);

            // Find suitable apartments based on client requirements
            SuitableApartments suitableFlats = flatRegister.FindApartment(requirements);
            
            if (suitableFlats.Apartments.Count == 0)
            {
                Console.WriteLine("Tinkamų butų nerasta");
            }
            else
            {
                // Display information about each suitable apartment
                foreach (var (flat, floor) in suitableFlats.Apartments)
                {
                    Console.WriteLine($"Buto numeris: {flat.ApartmentNumber}, Bendras plotas: {flat.Area}, Kambarių skaičius: {flat.RoomCount}, Aukštas: {floor}, Kaina: {flat.Price}€, Telefono numeris: {flat.PhoneNumber}");
                }
            }
        }
    }
}
