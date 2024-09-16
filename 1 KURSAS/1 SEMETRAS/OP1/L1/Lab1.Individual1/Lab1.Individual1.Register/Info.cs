using System;
namespace Lab1.Individual1.Register
{
    class Info
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Money { get; set; }

        public Info(string name, string surname, decimal money)
        {
            this.Name = name;
            this.Surname = surname;
            this.Money = money;
        }
        public decimal Contribution => Money / 4; // 25% of all of their money
    }
}
