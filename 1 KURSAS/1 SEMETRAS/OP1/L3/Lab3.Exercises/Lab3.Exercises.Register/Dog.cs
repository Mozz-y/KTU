using System;

namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Represents a dog with properties and behaviors, including age calculation and vaccination requirements.
    /// </summary>
    class Dog
    {
        // Public properties to store basic dog details
        public int ID { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        // Duration in years for how long a vaccination lasts
        private const int VaccinationDuration = 1;

        // Last recorded vaccination date
        public DateTime LastVaccinationDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the Dog class with specified details.
        /// </summary>
        /// <param name="id">Unique identifier for the dog.</param>
        /// <param name="name">Name of the dog.</param>
        /// <param name="breed">Breed of the dog.</param>
        /// <param name="birthDate">Birth date of the dog.</param>
        /// <param name="gender">Gender of the dog.</param>
        public Dog(int id, string name, string breed, DateTime birthDate, Gender gender)
        {
            // Assigning constructor parameters to properties
            ID = id;
            Name = name;
            Breed = breed;
            BirthDate = birthDate;
            Gender = gender;
        }

        /// <summary>
        /// Gets the dog's age based on the current date and birth date.
        /// </summary>
        public int Age
        {
            get
            {
                // Calculate age by subtracting birth year from current year
                DateTime today = DateTime.Today;
                int age = today.Year - BirthDate.Year;

                // Adjust age if the birth date hasn't occurred this year yet
                if (BirthDate.Date > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }

        /// <summary>
        /// Determines if the dog requires vaccination based on the last vaccination date.
        /// </summary>
        public bool RequiresVaccination
        {
            get
            {
                // If vaccination date is unrecorded, vaccination is required
                if (LastVaccinationDate == DateTime.MinValue)
                {
                    return true;
                }

                // Check if the time since last vaccination exceeds the defined duration
                return LastVaccinationDate.AddYears(VaccinationDuration) < DateTime.Now;
            }
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current dog, based on ID.
        /// </summary>
        /// <param name="other">The object to compare with the current dog.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object other)
        {
            // Check equality by comparing IDs
            return other is Dog dog && ID == dog.ID;
        }

        /// <summary>
        /// Returns a hash code for the current dog based on its ID.
        /// </summary>
        /// <returns>A hash code for the current dog.</returns>
        public override int GetHashCode()
        {
            // Use ID's hash code
            return ID.GetHashCode();
        }

        /// <summary>
        /// Compares the current dog to another dog by gender first, then by breed.
        /// </summary>
        /// <param name="other">The other dog to compare with.</param>
        /// <returns>An integer that indicates the relative order.</returns>
        public int CompareTo(Dog other)
        {
            // Compare by gender first
            int genderComparison = Gender.CompareTo(other.Gender);

            // If genders differ, return comparison result; otherwise, compare by breed
            return genderComparison != 0 ? genderComparison : Breed.CompareTo(other.Breed);
        }
    }
}
