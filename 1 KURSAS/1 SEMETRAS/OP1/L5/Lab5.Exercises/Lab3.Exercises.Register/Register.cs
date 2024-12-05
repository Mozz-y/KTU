using System;
using System.Collections.Generic;

namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Manages a register of dogs, providing functionalities to add, filter, and analyze dog data.
    /// </summary>
    class Register
    {
        // Holds all registered dogs
        private AnimalContainer AllAnimals;

        /// <summary>
        /// Initializes an empty DogsRegister.
        /// </summary>
        public Register()
        {
            AllAnimals = new AnimalContainer();
        }

        /// <summary>
        /// Initializes the register with an existing collection of dogs.
        /// </summary>
        /// <param name="dogsContainer">A DogsContainer with existing dogs.</param>
        public Register(AnimalContainer animalContainer)
        {
            AllAnimals = animalContainer;
        }

        /// <summary>
        /// Adds a new dog to the register.
        /// </summary>
        /// <param name="dog">The dog to add.</param>
        public void Add(Animal animal)
        {
            AllAnimals.Add(animal);
        }

        /// <summary>
        /// Retrieves the total number of dogs in the register.
        /// </summary>
        /// <returns>The count of dogs.</returns>
        public int AnimalCount()
        {
            return AllAnimals.Count;
        }

        /// <summary>
        /// Retrieves a dog at the specified index.
        /// </summary>
        /// <param name="index">Index of the dog to retrieve.</param>
        /// <returns>The dog at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index is out of range.</exception>
        public Animal GetAnimalByIndex(int index)
        {
            // Validate index range
            if (index >= 0 && index < AllAnimals.Count)
            {
                return AllAnimals.Get(index);
            }
            throw new IndexOutOfRangeException("Blogai parinktas indeksas");
        }

        /// <summary>
        /// Counts the number of dogs with a specified gender.
        /// </summary>
        /// <param name="gender">The gender to count.</param>
        /// <returns>Count of dogs matching the specified gender.</returns>
        public int CountByGender(Gender gender)
        {
            int count = 0;

            // Iterate through all dogs and count matching gender
            for (int i = 0; i < AllAnimals.Count; i++)
            {
                Animal animals = AllAnimals.Get(i);
                if (animals.Gender.Equals(gender))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Finds all unique dog breeds in the register.
        /// </summary>
        /// <returns>A list of unique dog breeds.</returns>
        public List<string> FindBreeds()
        {
            List<string> breeds = new List<string>();

            // Iterate through dogs to identify unique breeds
            for (int i = 0; i < AllAnimals.Count; i++)
            {
                Animal animal = AllAnimals.Get(i);
                if (!breeds.Contains(animal.Breed))
                {
                    breeds.Add(animal.Breed);
                }
            }

            return breeds;
        }

        /// <summary>
        /// Filters dogs by a specified breed.
        /// </summary>
        /// <param name="breed">The breed to filter by.</param>
        /// <returns>A DogsContainer with dogs of the specified breed.</returns>
        public AnimalContainer FilterByBreed(string breed)
        {
            AnimalContainer filtered = new AnimalContainer();

            // Add dogs to filtered list if they match the specified breed
            for (int i = 0; i < AllAnimals.Count; i++)
            {
                Animal animal = AllAnimals.Get(i);
                if (animal.Breed.Equals(breed))
                {
                    filtered.Add(animal);
                }
            }

            return filtered;
        }

        /// <summary>
        /// Finds the oldest dog in the entire register.
        /// </summary>
        /// <returns>The oldest dog.</returns>
        public Animal FindOldestAnimal()
        {
            return FindOldestAnimal(AllAnimals);
        }

        /// <summary>
        /// Finds the oldest dog of a specified breed.
        /// </summary>
        /// <param name="breed">The breed to search for the oldest dog.</param>
        /// <returns>The oldest dog of the specified breed.</returns>
        public Animal FindOldestAnimal(string breed)
        {
            AnimalContainer filtered = FilterByBreed(breed);
            return FindOldestAnimal(filtered);
        }

        /// <summary>
        /// Finds the oldest dog in a provided collection of dogs.
        /// </summary>
        /// <param name="dogs">A DogsContainer to search within.</param>
        /// <returns>The oldest dog in the collection.</returns>
        private Animal FindOldestAnimal(AnimalContainer animals)
        {
            Animal oldest = animals.Get(0);

            // Iterate through dogs to identify the oldest by birth date
            for (int i = 1; i < animals.Count; i++)
            {
                Animal currentAnimal = animals.Get(i);
                if (DateTime.Compare(oldest.BirthDate, currentAnimal.BirthDate) > 0)
                {
                    oldest = currentAnimal;
                }
            }

            return oldest;
        }

        /// <summary>
        /// Finds a dog by its unique ID.
        /// </summary>
        /// <param name="ID">The dog's ID.</param>
        /// <returns>The dog with the specified ID, or null if not found.</returns>
        private Animal FindAnimalByID(int ID)
        {
            // Iterate through all dogs to find a matching ID
            for (int i = 0; i < AllAnimals.Count; i++)
            {
                Animal animal = AllAnimals.Get(i);
                if (animal.ID == ID)
                {
                    return animal;
                }
            }

            return null;
        }

        /// <summary>
        /// Updates vaccination information for dogs based on a list of vaccinations.
        /// </summary>
        /// <param name="vaccinations">List of vaccinations to update dog records.</param>
        public void UpdateVaccinationsInfo(List<Vaccination> vaccinations)
        {
            // Update each dog's vaccination information
            foreach (var vaccination in vaccinations)
            {
                Animal animal = FindAnimalByID(vaccination.AnimalID);

                // Update if vaccination date is newer than current record
                if (animal != null && vaccination.Date > animal.LastVaccinationDate)
                {
                    animal.LastVaccinationDate = vaccination.Date;
                }
            }
        }

        /// <summary>
        /// Filters dogs whose vaccinations are expired or not up to date.
        /// </summary>
        /// <returns>A DogsContainer with dogs needing vaccination updates.</returns>
        public AnimalContainer FilterByVaccinationExpired()
        {
            AnimalContainer expiredVaccinationAnimals = new AnimalContainer();

            // Add dogs with expired vaccinations to the container
            for (int i = 0; i < AllAnimals.Count; i++)
            {
                Animal animal = AllAnimals.Get(i);
                if (animal.RequiresVaccination)
                {
                    expiredVaccinationAnimals.Add(animal);
                }
            }

            return expiredVaccinationAnimals;
        }

        /// <summary>
        /// Checks if a specified dog is in the register.
        /// </summary>
        /// <param name="dog">The dog to check for presence.</param>
        /// <returns>True if the dog is in the register; otherwise, false.</returns>
        public bool Contains(Animal animal)
        {
            return AllAnimals.Contains(animal);
        }

        public int CountAggresiveDogs()
        {
            int count = 0;
            for (int i = 0; i < this.AllAnimals.Count; i++)
            {
                Animal animal = this.AllAnimals.Get(i);
                if (animal is Dog && (animal as Dog).Aggresive)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
