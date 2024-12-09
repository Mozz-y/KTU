using System;
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
        public static AnimalsContainer ReadAnimals(string fileName)
        {
            AnimalsContainer animals = new AnimalsContainer();
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                string type = values[0];
                int id = int.Parse(values[1]);
                string name = values[2];
                string breed = values[3];
                DateTime birthDate = DateTime.Parse(values[4]);
                Gender gender;
                Enum.TryParse(values[5], out gender); //tries to convert value to enum
                switch (type)
                {
                    case "DOG":
                        bool aggresive = bool.Parse(values[6]);
                        Dog dog = new Dog(id, name, breed, birthDate, gender, aggresive);
                        animals.Add(dog);
                        break;
                    case "CAT":
                        Cat cat = new Cat(id, name, breed, birthDate, gender);
                        animals.Add(cat);
                        break;
                    case "GUINEAPIG":
                        GuineaPig guineaPig = new GuineaPig(id, name, breed, birthDate, gender);
                        animals.Add(guineaPig);
                        break;
                    default:
                        break;//unknown type
                }
            }
            return animals;
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
        public static void PrintAnimals(string label, AnimalsContainer animals)
        {
            // Print header for table with label
            Console.WriteLine(new string('-', 89));
            Console.WriteLine("| {0,-70} |", label);
            Console.WriteLine(new string('-', 89));

            // Print column headers
            Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12} | {4,-8} | {5, -12} |",
                              "Reg.Nr.", "Vardas", "Veislė", "Gimimo data", "Lytis", "Ar agresyvus");
            Console.WriteLine(new string('-', 89));

            // Print each dog's information in table format
            for (int i = 0; i < animals.Count; i++)
            {
                Animal animal = animals.Get(i);

                switch (animal)
                {
                    case Dog dog:
                        Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} | {5, -12} |",
                                          dog.ID, dog.Name, dog.Breed, dog.BirthDate, dog.Gender, dog.Aggresive ? "Taip" : "Ne");
                        break;

                    case Cat cat:
                        Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} | {5, -12} |",
                                          cat.ID, cat.Name, cat.Breed, cat.BirthDate, cat.Gender, "-");
                        break;

                    case GuineaPig guineaPig:
                        Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} | {5, -12} |",
                                          guineaPig.ID, guineaPig.Name, guineaPig.Breed, guineaPig.BirthDate, guineaPig.Gender, "-");
                        break;
                    default:
                        Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} | {5, -12} |",
                                          animal.ID, animal.Name, animal.Breed, animal.BirthDate, animal.Gender, "-");
                        break;
                }
            }

            Console.WriteLine(new string('-', 89));
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
        public static void PrintAnimalsToFile(string fileName, AnimalsContainer animals)
        {
            // Prepare lines for file output, starting with header row
            string[] lines = new string[animals.Count + 1];
            lines[0] = string.Format("{0};{1};{2};{3};{4};{5}",
                                     "Reg.Nr.", "Vardas", "Veislė", "Gimimo data", "Lytis", "Ar agresyvus");

            // Add each animal's data as a formatted line in the file
            for (int i = 0; i < animals.Count; i++)
            {
                Animal animal = animals.Get(i);

                // Use a switch statement for type handling
                switch (animal)
                {
                    case Dog dog:
                        lines[i + 1] = string.Format("{0};{1};{2};{3:yyyy-MM-dd};{4};{5}",
                                                     dog.ID, dog.Name, dog.Breed, dog.BirthDate, dog.Gender, dog.Aggresive ? "Taip" : "Ne");
                        break;

                    case Cat cat:
                        lines[i + 1] = string.Format("{0};{1};{2};{3:yyyy-MM-dd};{4};{5}",
                                                     cat.ID, cat.Name, cat.Breed, cat.BirthDate, cat.Gender, "-");
                        break;

                    case GuineaPig guineaPig:
                        lines[i + 1] = string.Format("{0};{1};{2};{3:yyyy-MM-dd};{4};{5}",
                                                     guineaPig.ID, guineaPig.Name, guineaPig.Breed, guineaPig.BirthDate, guineaPig.Gender, "-");
                        break;

                    default:
                        // Handle unknown animal types if needed
                        lines[i + 1] = string.Format("{0};{1};{2};{3:yyyy-MM-dd};{4};{5}",
                                                     animal.ID, animal.Name, animal.Breed, animal.BirthDate, animal.Gender, "-");
                        break;
                }
            }

            // Write all lines to file with UTF-8 encoding
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Prints all dogs from a DogsRegister instance in a table format.
        /// </summary>
        /// <param name="register">Dogs register to print.</param>
        public static void PrintRegister(Register register)
        {
            // Print header for register table
            Console.WriteLine(new string('-', 89));
            Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12} | {4,-8} | {5, -12} |",
                            "Reg.Nr.", "Vardas", "Veislė", "Gimimo data", "Lytis", "Ar agresyvus");
            Console.WriteLine(new string('-', 89));

            // Print each dog's information from register
            for (int i = 0; i < register.AnimalsCount(); i++)
            {
                Animal current = register.GetAnimalByIndex(i);
                switch (current)
                {
                    case Dog dog:
                        Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} | {5,-12} |",
                                          dog.ID, dog.Name, dog.Breed, dog.BirthDate, dog.Gender, dog.Aggresive ? "Taip" : "Ne");
                        break;

                    case Cat cat:
                        Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} | {5,-12} |",
                                          cat.ID, cat.Name, cat.Breed, cat.BirthDate, cat.Gender, "-");
                        break;

                    case GuineaPig guineaPig:
                        Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} | {5, -12} |",
                                          guineaPig.ID, guineaPig.Name, guineaPig.Breed, guineaPig.BirthDate, guineaPig.Gender, "-");
                        break;

                    default:
                        Console.WriteLine("| {0,8} | {1,-15} | {2,-15} | {3,-12:yyyy-MM-dd} | {4,-8} | {5,-12} |",
                                          current.ID, current.Name, current.Breed, current.BirthDate, current.Gender, "-");
                        break;
                };
            }

            Console.WriteLine(new string('-', 89));
        }
    }
}
