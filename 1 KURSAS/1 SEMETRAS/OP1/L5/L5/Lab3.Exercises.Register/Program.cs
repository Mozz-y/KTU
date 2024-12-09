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
            AnimalsContainer animalsContainer = InOutUtils.ReadAnimals("Dogs.csv");
            AnimalsContainer shallowCopy = new AnimalsContainer(animalsContainer); // Backup of original data
            animalsContainer.Sort(new AnimalsComparatorByName());
            Register register = new Register(animalsContainer);

            // Read vaccination data and update dogs' vaccination info
            List<Vaccination> vaccinationsData = InOutUtils.ReadVaccinations("Vaccinations.csv");
            register.UpdateVaccinationsInfo(vaccinationsData);

            // Display register information
            Console.WriteLine("Registro informacija:");
            InOutUtils.PrintRegister(register);
            Console.WriteLine("Iš viso gyvunų: {0}", register.AnimalsCount());

            // Count dogs by gender and display counts
            Console.WriteLine("Patinų: {0}", register.CountByGender(Gender.Male));
            Console.WriteLine("Patelių: {0}", register.CountByGender(Gender.Female));
            Console.WriteLine();

            Console.WriteLine($"Agresyvių šunų skaičius: {register.CountAggresiveDogs()}");
            Console.WriteLine();

            // Find and display the oldest dog
            Animal oldest = register.FindOldestAnimal();
            Console.WriteLine("Seniausias gyvunas");
            Console.WriteLine("Vardas: {0}, Veislė: {1}, Amžius: {2}",
                oldest.Name, oldest.Breed, oldest.Age);
            Console.WriteLine();

            // List and display all breeds
            List<string> breeds = register.FindBreeds();
            Console.WriteLine("Gyvunų veislės:");
            InOutUtils.PrintBreeds(breeds);
            Console.WriteLine();

            // Filter dogs by user-selected breed and display them
            Console.WriteLine("Kokios veislės gyvunus atrinkti?");
            string selectedBreed = Console.ReadLine();
            AnimalsContainer filteredByBreed = register.FilterByBreed(selectedBreed);
            InOutUtils.PrintAnimals($"Pasirinktos veislės ({selectedBreed}) gyvunai", filteredByBreed);

            // Export selected breed dogs to a file
            string fileName = $"{selectedBreed}.csv";
            InOutUtils.PrintAnimalsToFile(fileName, filteredByBreed);
            Console.WriteLine();

            // Display dogs with expired vaccinations
            AnimalsContainer expiredVaccinationsRegister = register.FilterByVaccinationExpired();
            InOutUtils.PrintAnimals("Gyvunai, kuriems reikalingi skiepai", expiredVaccinationsRegister);
            Console.WriteLine();

            // Display dogs of the selected breed that need vaccination
            InOutUtils.PrintAnimals($"Reikia skiepyti ({selectedBreed})", expiredVaccinationsRegister.Intersect(filteredByBreed));
            Console.WriteLine();

            animalsContainer.Sort(new AnimalsComparatorByName());
            InOutUtils.PrintRegister(register);

            animalsContainer.Sort(new AnimalsComparatorByBirthDate());
            InOutUtils.PrintRegister(register);

            // Display original, unmodified data
            InOutUtils.PrintAnimals("Pradiniai nepakeisti duomenys", shallowCopy);
        }
    }
}
