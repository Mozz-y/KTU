using System;
using System.Text.RegularExpressions;
namespace Introduction.Function
{
    class Program
    {
        static void Main(string[] args)
        {
            double functionResult;
            double x;
            Console.WriteLine("Įveskite x reikšmę:");
            x = double.Parse(Console.ReadLine());
            if (x >= 0 && x <= -1)
                functionResult = Math.Cos(Math.Pow(x, 2));
            else if (x < 1 && x > 0)
                functionResult = 4 * Math.Pow(x, 2) + 3;
            else
                functionResult = Math.Sqrt(Math.Pow(x, 2) * x + 4);
            Console.WriteLine(" fx = {0}", functionResult);
            Console.ReadKey();
        }
    }
}