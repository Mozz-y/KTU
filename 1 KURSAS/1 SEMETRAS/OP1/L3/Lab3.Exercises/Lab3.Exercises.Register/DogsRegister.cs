using System;
using System.Collections.Generic;

namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Manages a register of dogs, providing functionalities to add, filter, and analyze dog data.
    /// </summary>
    class DogsRegister
    {
        // Holds all registered dogs
        private DogsContainer AllDogs;

        /// <summary>
        /// Initializes an empty DogsRegister.
        /// </summary>
        public DogsRegister()
        {
            AllDogs = new DogsContainer();
        }

        /// <summary>
        /// Initializes the register with an existing collection of dogs.
        /// </summary>
        /// <param name="dogsContainer">A DogsContainer with existing dogs.</param>
        public DogsRegister(DogsContainer dogsContainer)
        {
            AllDogs = dogsContainer;
        }

        /// <summary>
        /// Adds a new dog to the register.
        /// </summary>
        /// <param name="dog">The dog to add.</param>
        public void Add(Dog dog)
        {
            AllDogs.Add(dog);
        }

        /// <summary>
        /// Retrieves the total number of dogs in the register.
        /// </summary>
        /// <returns>The count of dogs.</returns>
        public int DogsCount()
        {
            return AllDogs.Count;
        }

        /// <summary>
        /// Retrieves a dog at the specified index.
        /// </summary>
        /// <param name="index">Index of the dog to retrieve.</param>
        /// <returns>The dog at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when index is out of range.</exception>
        public Dog GetDogByIndex(int index)
        {
            // Validate index range
            if (index >= 0 && index < AllDogs.Count)
            {
                return AllDogs.Get(index);
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
            for (int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                if (dog.Gender.Equals(gender))
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
            for (int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                if (!breeds.Contains(dog.Breed))
                {
                    breeds.Add(dog.Breed);
                }
            }

            return breeds;
        }

        /// <summary>
        /// Filters dogs by a specified breed.
        /// </summary>
        /// <param name="breed">The breed to filter by.</param>
        /// <returns>A DogsContainer with dogs of the specified breed.</returns>
        public DogsContainer FilterByBreed(string breed)
        {
            DogsContainer filtered = new DogsContainer();

            // Add dogs to filtered list if they match the specified breed
            for (int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                if (dog.Breed.Equals(breed))
                {
                    filtered.Add(dog);
                }
            }

            return filtered;
        }

        /// <summary>
        /// Finds the oldest dog in the entire register.
        /// </summary>
        /// <returns>The oldest dog.</returns>
        public Dog FindOldestDog()
        {
            return FindOldestDog(AllDogs);
        }

        /// <summary>
        /// Finds the oldest dog of a specified breed.
        /// </summary>
        /// <param name="breed">The breed to search for the oldest dog.</param>
        /// <returns>The oldest dog of the specified breed.</returns>
        public Dog FindOldestDog(string breed)
        {
            DogsContainer filtered = FilterByBreed(breed);
            return FindOldestDog(filtered);
        }

        /// <summary>
        /// Finds the oldest dog in a provided collection of dogs.
        /// </summary>
        /// <param name="dogs">A DogsContainer to search within.</param>
        /// <returns>The oldest dog in the collection.</returns>
        private Dog FindOldestDog(DogsContainer dogs)
        {
            Dog oldest = dogs.Get(0);

            // Iterate through dogs to identify the oldest by birth date
            for (int i = 1; i < dogs.Count; i++)
            {
                Dog currentDog = dogs.Get(i);
                if (DateTime.Compare(oldest.BirthDate, currentDog.BirthDate) > 0)
                {
                    oldest = currentDog;
                }
            }

            return oldest;
        }

        /// <summary>
        /// Finds a dog by its unique ID.
        /// </summary>
        /// <param name="ID">The dog's ID.</param>
        /// <returns>The dog with the specified ID, or null if not found.</returns>
        private Dog FindDogByID(int ID)
        {
            // Iterate through all dogs to find a matching ID
            for (int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                if (dog.ID == ID)
                {
                    return dog;
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
                Dog dog = FindDogByID(vaccination.DogID);

                // Update if vaccination date is newer than current record
                if (dog != null && vaccination.Date > dog.LastVaccinationDate)
                {
                    dog.LastVaccinationDate = vaccination.Date;
                }
            }
        }

        /// <summary>
        /// Filters dogs whose vaccinations are expired or not up to date.
        /// </summary>
        /// <returns>A DogsContainer with dogs needing vaccination updates.</returns>
        public DogsContainer FilterByVaccinationExpired()
        {
            DogsContainer expiredVaccinationDogs = new DogsContainer();

            // Add dogs with expired vaccinations to the container
            for (int i = 0; i < AllDogs.Count; i++)
            {
                Dog dog = AllDogs.Get(i);
                if (dog.RequiresVaccination)
                {
                    expiredVaccinationDogs.Add(dog);
                }
            }

            return expiredVaccinationDogs;
        }

        /// <summary>
        /// Checks if a specified dog is in the register.
        /// </summary>
        /// <param name="dog">The dog to check for presence.</param>
        /// <returns>True if the dog is in the register; otherwise, false.</returns>
        public bool Contains(Dog dog)
        {
            return AllDogs.Contains(dog);
        }
    }
}
