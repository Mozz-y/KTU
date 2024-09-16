using System;
namespace Introduction.Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Įveskite pirma skaičių (a):");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Įveskite antra skaičių (b):");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Įveskite pageidaujamos matematinės operacijos ženklą (+, -, *, /):");
            char operation = Console.ReadKey().KeyChar;

            Console.WriteLine(); // Blank line for space

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
                        Console.WriteLine("Dalyba iš nulio negalima");
                    }
                    break;
                default:
                    Console.WriteLine("Netinkamas operacijos ženklas. Pasirinkite +, -, *, arba /");
                    break;
            }
        }
    }
}