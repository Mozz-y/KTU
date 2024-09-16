using System;
namespace Introduction.Greeting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Įveskite savo vardą: ");
            string name = Console.ReadLine();

            string correctName = name;
            string ending = name.Length > 1 ? name.Substring(name.Length - 2) : "";


            switch (ending)
            {
                case "as":
                    correctName = name.Substring(0, name.Length - 2) + "ai";
                    Console.WriteLine($"Labas, {correctName}!");
                    break;
                case "is":
                    correctName = name.Substring(0, name.Length - 2) + "i";
                    Console.WriteLine($"Labas, {correctName}!");
                    break;
                case "ys":
                    correctName = name.Substring(0, name.Length - 2) + "y";
                    Console.WriteLine($"Labas, {correctName}!");
                    break;
                case "a":
                    correctName = name + "!";
                    Console.WriteLine($"Labas, {correctName}!");
                    break;
                case "ė":
                    correctName = name.Substring(0, name.Length - 1) + "e";
                    Console.WriteLine($"Labas, {correctName}!");
                    break;
                default:
                    correctName = name;
                    Console.WriteLine($"Labas, {correctName}!");
                    break;
            }
        }
    }
}