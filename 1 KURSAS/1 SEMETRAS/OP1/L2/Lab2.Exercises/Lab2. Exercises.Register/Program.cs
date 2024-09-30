using Lab2.Excercises.Register;
using System;
using System.Collections.Generic;

namespace Lab2.Excercises.Register
{
    /// <summary>
    /// Main class for running the Dog Register application.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read dogs from CSV file
            DogsRegister register = InOutUtils.ReadDogs(@"Dogs.csv");
            Console.WriteLine("Registro informacija:");
            InOutUtils.PrintDogs(register.AllDogs);
            Console.WriteLine("Iš viso šunų: {0}", register.DogsCount());

            // Count and display male and female dogs
            Console.WriteLine("Patinų: {0}", register.CountByGender(Gender.Male));
            Console.WriteLine("Patelių: {0}", register.CountByGender(Gender.Female));
            Console.WriteLine();

            // Find and display the oldest dog
            Dog oldest = register.FindOldestDog();
            Console.WriteLine("Seniausias šuo");
            Console.WriteLine("Vardas: {0}, Veislė: {1}, Amžius: {2}", oldest.Name, oldest.Breed, oldest.Age);

            // Find and display all dog breeds
            List<string> Breeds = register.FindBreeds();
            Console.WriteLine("Šunų veislės:");
            InOutUtils.PrintBreeds(Breeds);

            // Filter by selected breed
            Console.WriteLine("Kokios veislės šunis atrinkti?");
            string selectedBreed = Console.ReadLine();

            List<Dog> FilteredByBreed = register.FilterByBreed(selectedBreed);
            InOutUtils.PrintDogs(FilteredByBreed);

            // Export filtered dogs to CSV
            string fileName = selectedBreed + ".csv";
            InOutUtils.PrintDogsToCSVFile(fileName, FilteredByBreed);

            // Read vaccination data and update dogs' vaccination information
            List<Vaccination> VaccinationsData = InOutUtils.ReadVaccinations(@"Vaccinations.csv");
            register.UpdateVaccinationsInfo(VaccinationsData);

            // Filter and display dogs with expired vaccinations
            DogsRegister expiredVaccinations = register.FilterByVaccinationExpired();
            Console.WriteLine("Neskiepytų ar su nebegaliojančiais skiepais šunų sąrašas:");
            InOutUtils.PrintDogs(expiredVaccinations.AllDogs);
        }
    }
}
