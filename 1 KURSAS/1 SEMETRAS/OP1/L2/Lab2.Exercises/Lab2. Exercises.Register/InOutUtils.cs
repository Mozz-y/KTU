using Lab2.Excercises.Register;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab2.Excercises.Register
{
    /// <summary>
    /// Utility class for input and output operations related to dogs and vaccinations.
    /// </summary>
    static class InOutUtils
    {
        /// <summary>
        /// Reads dogs from a CSV file and returns a DogsRegister object.
        /// </summary>
        /// <param name="fileName">The name of the CSV file to read.</param>
        /// <returns>A DogsRegister object containing the dogs read from the file.</returns>
        public static DogsRegister ReadDogs(string fileName)
        {
            DogsRegister dogs = new DogsRegister();
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                int id = int.Parse(values[0]);
                string name = values[1];
                string breed = values[2];
                DateTime birthDate = DateTime.Parse(values[3]);
                Gender gender;
                Enum.TryParse(values[4], out gender); // Attempts to convert value to enum
                Dog dog = new Dog(id, name, breed, birthDate, gender);
                if (!dogs.Contains(dog))
                {
                    dogs.Add(dog);
                }
            }
            return dogs;
        }

        /// <summary>
        /// Prints a list of dogs to the console.
        /// </summary>
        /// <param name="dogs">The list of dogs to print.</param>
        public static void PrintDogs(List<Dog> dogs)
        {
            Console.WriteLine(new string('-', 74));
            Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12} | {4,-8} |",
           "Reg.Nr.", "Vardas", "Veislė", "Gimimo data", "Lytis");
            Console.WriteLine(new string('-', 74));
            foreach (Dog dog in dogs)
            {
                Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} |",
               dog.ID, dog.Name, dog.Breed, dog.BirthDate, dog.Gender);
            }
            Console.WriteLine(new string('-', 74));
        }

        /// <summary>
        /// Prints a list of breeds to the console.
        /// </summary>
        /// <param name="breeds">The list of breeds to print.</param>
        public static void PrintBreeds(List<string> breeds)
        {
            foreach (string breed in breeds)
            {
                Console.WriteLine(breed);
            }
        }

        /// <summary>
        /// Exports a list of dogs to a CSV file.
        /// </summary>
        /// <param name="fileName">The name of the CSV file to export to.</param>
        /// <param name="dogs">The list of dogs to export.</param>
        public static void PrintDogsToCSVFile(string fileName, List<Dog> dogs)
        {
            string[] lines = new string[dogs.Count + 1];
            lines[0] = String.Format("{0};{1};{2};{3};{4}",
            "Reg.Nr.", "Vardas", "Veislė", "Gimimo data", "Lytis");
            for (int i = 0; i < dogs.Count; i++)
            {
                lines[i + 1] = String.Format("{0};{1};{2};{3:yyyy-MM-dd};{4}",
                dogs[i].ID, dogs[i].Name, dogs[i].Breed, dogs[i].BirthDate, dogs[i].Gender);
            }
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Reads vaccinations from a CSV file and returns a list of Vaccination objects.
        /// </summary>
        /// <param name="fileName">The name of the CSV file to read vaccinations from.</param>
        /// <returns>A list of Vaccination objects read from the file.</returns>
        public static List<Vaccination> ReadVaccinations(string fileName)
        {
            List<Vaccination> vaccinations = new List<Vaccination>();
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                int dogID = int.Parse(values[0]);
                DateTime vaccinationDate = DateTime.Parse(values[1]);
                vaccinations.Add(new Vaccination(dogID, vaccinationDate));
            }
            return vaccinations;
        }
    }
}
