using Lab2.Excercises.Register;
using System;
using System.Collections.Generic;

namespace Lab2.Excercises.Register
{
    /// <summary>
    /// Represents a register of dogs.
    /// </summary>
    class DogsRegister
    {
        // Properties
        public List<Dog> AllDogs { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DogsRegister"/> class.
        /// </summary>
        public DogsRegister()
        {
            AllDogs = new List<Dog>();
        }

        /// <summary>
        /// Adds a dog to the register.
        /// </summary>
        /// <param name="dog">The dog to add to the register.</param>
        public void Add(Dog dog)
        {
            AllDogs.Add(dog);
        }

        /// <summary>
        /// Checks if the register contains a specific dog.
        /// </summary>
        /// <param name="dog">The dog to check for in the register.</param>
        /// <returns>True if the dog is found; otherwise, false.</returns>
        public bool Contains(Dog dog)
        {
            return AllDogs.Contains(dog);
        }

        /// <summary>
        /// Returns the total count of dogs in the register.
        /// </summary>
        /// <returns>The total number of dogs in the register.</returns>
        public int DogsCount()
        {
            return AllDogs.Count;
        }

        /// <summary>
        /// Counts dogs by their gender.
        /// </summary>
        /// <param name="gender">The gender to count (Male or Female).</param>
        /// <returns>The count of dogs of the specified gender.</returns>
        public int CountByGender(Gender gender)
        {
            int count = 0;
            foreach (Dog dog in AllDogs)
            {
                if (dog.Gender == gender)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Finds the oldest dog in the register.
        /// </summary>
        /// <returns>The oldest dog in the register.</returns>
        public Dog FindOldestDog()
        {
            Dog oldestDog = AllDogs[0];
            foreach (Dog dog in AllDogs)
            {
                if (dog.Age > oldestDog.Age)
                {
                    oldestDog = dog;
                }
            }
            return oldestDog;
        }

        /// <summary>
        /// Finds all unique breeds in the register.
        /// </summary>
        /// <returns>A list of unique dog breeds in the register.</returns>
        public List<string> FindBreeds()
        {
            HashSet<string> breedsSet = new HashSet<string>();
            foreach (Dog dog in AllDogs)
            {
                breedsSet.Add(dog.Breed);
            }
            return new List<string>(breedsSet);
        }

        /// <summary>
        /// Filters dogs by breed.
        /// </summary>
        /// <param name="breed">The breed to filter dogs by.</param>
        /// <returns>A list of dogs that match the specified breed.</returns>
        public List<Dog> FilterByBreed(string breed)
        {
            List<Dog> filtered = new List<Dog>();
            foreach (Dog dog in AllDogs)
            {
                if (dog.Breed.Equals(breed, StringComparison.OrdinalIgnoreCase))
                {
                    filtered.Add(dog);
                }
            }
            return filtered;
        }

        /// <summary>
        /// Updates the vaccination information of dogs based on the provided vaccination data.
        /// </summary>
        /// <param name="vaccinations">A list of vaccinations to update the dogs' records.</param>
        public void UpdateVaccinationsInfo(List<Vaccination> vaccinations)
        {
            foreach (Vaccination vaccination in vaccinations)
            {
                foreach (Dog dog in AllDogs)
                {
                    if (dog.ID == vaccination.DogID)
                    {
                        dog.LastVaccinationDate = vaccination.VaccinationDate;
                    }
                }
            }
        }

        /// <summary>
        /// Filters dogs with expired vaccinations or unvaccinated dogs.
        /// </summary>
        /// <returns>A DogsRegister containing dogs with expired or no vaccinations.</returns>
        public DogsRegister FilterByVaccinationExpired()
        {
            DogsRegister expiredDogs = new DogsRegister();
            foreach (Dog dog in AllDogs)
            {
                if (dog.RequiresVaccination)
                {
                    expiredDogs.Add(dog);
                }
            }
            return expiredDogs;
        }
    }
}
