using System;
using System.Collections.Generic;

namespace Lab3.Exercises.Register
{
    /// <summary>
    /// The main class to run the dog registry application, providing options to view, sort, filter, and export dog data.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point of the program. Reads dog data, updates vaccination information,
        /// allows filtering by breed, and outputs data to the console and files.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Read initial dog data from file
            DogsContainer dogsContainer = InOutUtils.ReadDogs("Dogs.csv");
            DogsContainer shallowCopy = new DogsContainer(dogsContainer); // Backup of original data
            dogsContainer.Sort();
            DogsRegister register = new DogsRegister(dogsContainer);

            // Read vaccination data and update dogs' vaccination info
            List<Vaccination> vaccinationsData = InOutUtils.ReadVaccinations("Vaccinations.csv");
            register.UpdateVaccinationsInfo(vaccinationsData);

            // Display register information
            Console.WriteLine("Registro informacija:");
            InOutUtils.PrintRegister(register);
            Console.WriteLine("Iš viso šunų: {0}", register.DogsCount());

            // Count dogs by gender and display counts
            Console.WriteLine("Patinų: {0}", register.CountByGender(Gender.Male));
            Console.WriteLine("Patelių: {0}", register.CountByGender(Gender.Female));
            Console.WriteLine();

            // Find and display the oldest dog
            Dog oldest = register.FindOldestDog();
            Console.WriteLine("Seniausias šuo");
            Console.WriteLine("Vardas: {0}, Veislė: {1}, Amžius: {2}",
                oldest.Name, oldest.Breed, oldest.Age);
            Console.WriteLine();

            // List and display all breeds
            List<string> breeds = register.FindBreeds();
            Console.WriteLine("Šunų veislės:");
            InOutUtils.PrintBreeds(breeds);
            Console.WriteLine();

            // Filter dogs by user-selected breed and display them
            Console.WriteLine("Kokios veislės šunis atrinkti?");
            string selectedBreed = Console.ReadLine();
            DogsContainer filteredByBreed = register.FilterByBreed(selectedBreed);
            InOutUtils.PrintDogs($"Pasirinktos veislės ({selectedBreed}) šunys", filteredByBreed);

            // Export selected breed dogs to a file
            string fileName = $"{selectedBreed}.csv";
            InOutUtils.PrintDogsToFile(fileName, filteredByBreed);
            Console.WriteLine();

            // Display dogs with expired vaccinations
            DogsContainer expiredVaccinationsRegister = register.FilterByVaccinationExpired();
            InOutUtils.PrintDogs("Šunys, kuriems reikalingi skiepai", expiredVaccinationsRegister);
            Console.WriteLine();

            // Display dogs of the selected breed that need vaccination
            InOutUtils.PrintDogs($"Reikia skiepyti ({selectedBreed})", expiredVaccinationsRegister.Intersect(filteredByBreed));
            Console.WriteLine();

            // Display original, unmodified data
            InOutUtils.PrintDogs("Pradiniai nepakeisti duomenys", shallowCopy);
        }
    }
}
