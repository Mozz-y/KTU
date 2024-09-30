using System;

namespace Lab2.Excercises.Register
{
    /// <summary>
    /// Represents a dog with its attributes and vaccination information.
    /// </summary>
    class Dog
    {
        // Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public DateTime LastVaccinationDate { get; set; }

        // Vaccination duration in years
        private const int VaccinationDuration = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dog"/> class.
        /// </summary>
        /// <param name="id">The ID of the dog.</param>
        /// <param name="name">The name of the dog.</param>
        /// <param name="breed">The breed of the dog.</param>
        /// <param name="birthDate">The birth date of the dog.</param>
        /// <param name="gender">The gender of the dog.</param>
        public Dog(int id, string name, string breed, DateTime birthDate, Gender gender)
        {
            this.ID = id;
            this.Name = name;
            this.Breed = breed;
            this.BirthDate = birthDate;
            this.Gender = gender;
        }

        /// <summary>
        /// Gets the age of the dog in years.
        /// </summary>
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - this.BirthDate.Year;
                if (this.BirthDate.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }

        /// <summary>
        /// Checks if the dog requires vaccination.
        /// </summary>
        public bool RequiresVaccination
        {
            get
            {
                // If there is no vaccination date, the dog requires vaccination
                if (LastVaccinationDate.Equals(DateTime.MinValue))
                {
                    return true;
                }
                // Check if the last vaccination is expired
                return LastVaccinationDate.AddYears(VaccinationDuration).CompareTo(DateTime.Now) < 0;
            }
        }

        /// <summary>
        /// Checks for equality between two Dog objects based on their ID.
        /// </summary>
        /// <param name="other">The object to compare with.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object other)
        {
            return this.ID == ((Dog)other).ID;
        }

        /// <summary>
        /// Returns the hash code for the Dog object.
        /// </summary>
        /// <returns>The hash code for the Dog object.</returns>
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
