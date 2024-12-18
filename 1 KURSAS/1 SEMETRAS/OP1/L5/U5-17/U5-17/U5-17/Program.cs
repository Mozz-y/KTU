using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace U5_17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            File.Delete("rezultatai.txt");

            // File paths
            string file1 = "Org1.csv";
            string file2 = "Org2.csv";
            string file3 = "Org3.csv";

            // Create registers for each organization
            Register registerOne = InOutUtils.ReadData(file1);
            Register registerTwo = InOutUtils.ReadData(file2);
            Register registerThree = InOutUtils.ReadData(file3);

            // Print data for each organization
            InOutUtils.PrintData(registerOne.GetOrganizationName(), registerOne.GetQuestions(), "rezultatai.txt");
            InOutUtils.PrintData(registerTwo.GetOrganizationName(), registerTwo.GetQuestions(), "rezultatai.txt");
            InOutUtils.PrintData(registerThree.GetOrganizationName(), registerThree.GetQuestions(), "rezultatai.txt");

            // Compute total difficulties across all registers
            var totalDifficulties = registerOne.FindTotalDifficulties(registerTwo, registerThree);
            InOutUtils.PrintTotalDifficulties(totalDifficulties);

            // Find top authors for each register
            var topAuthorsOne = registerOne.GetTopAuthors();
            var topAuthorsTwo = registerTwo.GetTopAuthors();
            var topAuthorsThree = registerThree.GetTopAuthors();
            InOutUtils.PrintTopAuthors(new Dictionary<string, Dictionary<string, int>>
            {
                { registerOne.GetOrganizationName(), topAuthorsOne },
                { registerTwo.GetOrganizationName(), topAuthorsTwo },
                { registerThree.GetOrganizationName(), topAuthorsThree }
            });

            // Find all music questions with difficulty III across all registers
            var allDifficultMusicQuestions = registerOne.FindDifficultMusicQuestions(registerTwo, registerThree);
            InOutUtils.PrintMostDifficultMusicQuestions(allDifficultMusicQuestions, "SudėtingiMuzikiniai.csv");

            // Find all difficult questions across all registers
            var allDifficultQuestions = registerOne.FindAllDifficultQuestions(registerTwo, registerThree);
            InOutUtils.PrintMostDifficultQuestions(allDifficultQuestions, "SudėtingiBendri.csv");

            // Find "Linksmasis" questions across all registers
            var funQuestions = registerOne.GetFunQuestions(registerTwo, registerThree);
            InOutUtils.PrintFunQuestions(funQuestions, "Linksmieji.csv");
        }
    }
}
