using System;
using System.Collections.Generic;

namespace Lab1.Individual1.Register
{
    class Program
    {
        static void Main(string[] args)
        {
            // To print euro symbol
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Read data from the CSV file
            string fileName = "Data.csv";
            List<Info> members = InOutUtils.ReadData(fileName);

            // Calculate total contribution
            decimal totalContribution = TaskUtils.CalculateTotalContribution(members);

            // Find the top contributors
            List<Info> topContributors = TaskUtils.FindTopContributors(members);

            // Output the total contribution
            Console.WriteLine($"Bendrai surinkta: {totalContribution:C}");

            // Output the top contributor(s)
            Console.WriteLine("Daugiausiai skyrė:");
            foreach (var contributor in topContributors)
            {
                Console.WriteLine($"{contributor.Name} {contributor.Surname} skyrė {contributor.Contribution:C}");
            }
        }
    }
}
