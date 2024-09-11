using System;
namespace Introduction.Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iveskite pirmaa skaiciu (a):");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Iveskite antra skaiciu (b):");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Iveskite pageidaujamos matematines operacijos zenkla (+, -, *, /):");
            char operation = Console.ReadKey().KeyChar;

            Console.WriteLine(); // Adding a blank line for readability

            // Switch case to handle different operations
            switch (operation)
            {
                case '+':
                    Console.WriteLine($"Rezultatas: {a} + {b} = {a + b}");
                    break;
                case '-':
                    Console.WriteLine($"Rezultatas: {a} - {b} = {a - b}");
                    break;
                case '*':
                    Console.WriteLine($"Rezultatas: {a} * {b} = {a * b}");
                    break;
                case '/':
                    if (b != 0)
                    {
                        Console.WriteLine($"Rezultatas: {a} / {b} = {a / b}");
                    }
                    else
                    {
                        Console.WriteLine("Dalyba is nulio negalima");
                    }
                    break;
                default:
                    Console.WriteLine("Netinkamas operacijos zenklas. Pasirinkite +, -, *, arba /");
                    break;
            }
        }
    }
}
