using System;
namespace Introduction.If.Step2
{
    class Program
    {
        static void Main(string[] args)
        {
            char character;
            Console.WriteLine("Įveskite spausdinamą simbolį");
            character = Console.ReadLine()[0];

            // Print 10 lines in total (50/5 = 10)
            for (int i = 0; i < 10; i++)
            {
                /// Print 5 symbols per line
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(character);
                }
                /// Move to the next line
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
