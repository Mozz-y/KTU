using System;

namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Represents a dog with properties and behaviors, including age calculation and vaccination requirements.
    /// </summary>
    public class Dog : Animal
    {
        private const int VaccinationDuration = 1;
        public bool Aggresive { get; set; }
        public Dog(int id, string name, string breed, DateTime birthDate,
        Gender gender, bool aggresive) : base(id, name, breed, birthDate, gender)
        {
            this.Aggresive = aggresive;
        }
        public override bool RequiresVaccination
        {
            get
            {
                if (LastVaccinationDate.Equals(DateTime.MinValue))
                {
                    return true;
                }
                return LastVaccinationDate.AddYears(VaccinationDuration)
               .CompareTo(DateTime.Now) < 0;
            }
        }
    }
}
