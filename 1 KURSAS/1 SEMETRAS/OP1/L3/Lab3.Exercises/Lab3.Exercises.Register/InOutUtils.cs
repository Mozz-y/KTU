﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Provides utility methods for reading, printing, and exporting dog and vaccination data.
    /// </summary>
    static class InOutUtils
    {
        /// <summary>
        /// Reads dog data from a specified file and stores it in a DogsContainer.
        /// </summary>
        /// <param name="fileName">The file path containing dog data.</param>
        /// <returns>A DogsContainer populated with dog data.</returns>
        public static DogsContainer ReadDogs(string fileName)
        {
            // Initialize container for dogs
            DogsContainer dogs = new DogsContainer();

            // Read all lines from file with UTF-8 encoding
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);

            // Process each line into dog data
            foreach (string line in lines)
            {
                string[] values = line.Split(';');

                // Parse dog data from line values
                int id = int.Parse(values[0]);
                string name = values[1];
                string breed = values[2];
                DateTime birthDate = DateTime.Parse(values[3]);

                // Parse gender from string; set default if parsing fails
                Enum.TryParse(values[4], out Gender gender);

                // Create a new dog instance and add to container if unique
                Dog dog = new Dog(id, name, breed, birthDate, gender);
                if (!dogs.Contains(dog))
                {
                    dogs.Add(dog);
                }
            }

            return dogs;
        }

        /// <summary>
        /// Reads vaccination data from a specified file and stores it in a list.
        /// </summary>
        /// <param name="fileName">The file path containing vaccination data.</param>
        /// <returns>A list of Vaccination instances.</returns>
        public static List<Vaccination> ReadVaccinations(string fileName)
        {
            // Initialize list for vaccinations
            List<Vaccination> vaccinations = new List<Vaccination>();

            // Read all lines from file
            string[] lines = File.ReadAllLines(fileName);

            // Process each line into vaccination data
            foreach (string line in lines)
            {
                string[] values = line.Split(';');

                // Parse vaccination data from line values
                int id = int.Parse(values[0]);
                DateTime vaccinationDate = DateTime.Parse(values[1]);

                // Create and add new vaccination instance
                Vaccination vaccination = new Vaccination(id, vaccinationDate);
                vaccinations.Add(vaccination);
            }

            return vaccinations;
        }

        /// <summary>
        /// Prints a table of dogs to the console with a specified label.
        /// </summary>
        /// <param name="label">Label for the printed table.</param>
        /// <param name="dogs">Container of dogs to print.</param>
        public static void PrintDogs(string label, DogsContainer dogs)
        {
            // Print header for table with label
            Console.WriteLine(new string('-', 74));
            Console.WriteLine("| {0,-70} |", label);
            Console.WriteLine(new string('-', 74));

            // Print column headers
            Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12} | {4,-8} |",
                              "Reg.Nr.", "Vardas", "Veislė", "Gimimo data", "Lytis");
            Console.WriteLine(new string('-', 74));

            // Print each dog's information in table format
            for (int i = 0; i < dogs.Count; i++)
            {
                Dog dog = dogs.Get(i);
                Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} |",
                                  dog.ID, dog.Name, dog.Breed, dog.BirthDate, dog.Gender);
            }

            Console.WriteLine(new string('-', 74));
        }

        /// <summary>
        /// Prints a list of unique dog breeds to the console.
        /// </summary>
        /// <param name="breeds">List of unique dog breeds.</param>
        public static void PrintBreeds(List<string> breeds)
        {
            // Print each breed in the list
            foreach (string breed in breeds)
            {
                Console.WriteLine(breed);
            }
        }

        /// <summary>
        /// Exports dog data to a specified file in a table format.
        /// </summary>
        /// <param name="fileName">File path to save the dog data.</param>
        /// <param name="dogs">Container of dogs to export.</param>
        public static void PrintDogsToFile(string fileName, DogsContainer dogs)
        {
            // Prepare lines for file output, starting with header row
            string[] lines = new string[dogs.Count + 1];
            lines[0] = string.Format("{0};{1};{2};{3};{4}",
                                     "Reg.Nr.", "Vardas", "Veislė", "Gimimo data", "Lytis");

            // Add each dog's data as a formatted line in the file
            for (int i = 0; i < dogs.Count; i++)
            {
                Dog dog = dogs.Get(i);
                lines[i + 1] = string.Format("{0};{1};{2};{3};{4}",
                                             dog.ID, dog.Name, dog.Breed, dog.BirthDate, dog.Gender);
            }

            // Write all lines to file with UTF-8 encoding
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Prints all dogs from a DogsRegister instance in a table format.
        /// </summary>
        /// <param name="register">Dogs register to print.</param>
        public static void PrintRegister(DogsRegister register)
        {
            // Print header for register table
            Console.WriteLine(new string('-', 74));
            Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12} | {4,-8} |",
                              "Reg.Nr.", "Vardas", "Veislė", "Gimimo data", "Lytis");
            Console.WriteLine(new string('-', 74));

            // Print each dog's information from register
            for (int i = 0; i < register.DogsCount(); i++)
            {
                Dog current = register.GetDogByIndex(i);
                Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} |",
                                  current.ID, current.Name, current.Breed, current.BirthDate, current.Gender);
            }

            Console.WriteLine(new string('-', 74));
        }
    }
}