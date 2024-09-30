using System;

namespace Lab2.Excercises.Register
{
    /// <summary>
    /// Represents vaccination data for a dog.
    /// </summary>
    class Vaccination
    {
        // Properties
        public int DogID { get; set; }
        public DateTime VaccinationDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vaccination"/> class.
        /// </summary>
        /// <param name="dogID">The ID of the dog associated with the vaccination.</param>
        /// <param name="vaccinationDate">The date of the vaccination.</param>
        public Vaccination(int dogID, DateTime vaccinationDate)
        {
            DogID = dogID;
            VaccinationDate = vaccinationDate;
        }
    }
}
