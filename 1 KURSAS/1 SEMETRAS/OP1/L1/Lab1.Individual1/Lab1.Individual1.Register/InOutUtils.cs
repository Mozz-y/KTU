using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1.Individual1.Register
{
    static class InOutUtils
    {
        public static List<Info> ReadData(string fileName)
        {
            List<Info> Data = new List<Info>();
            string[] Lines = File.ReadAllLines(fileName, Encoding.UTF8);
            foreach (string Line in Lines)
            {
                string[] Values = Line.Split(';');
                string name = Values[0];
                string surname = Values[1];
                decimal money = decimal.Parse(Values[2]);

                Info info = new Info(name, surname, money);
                Data.Add(info);
            }
            return Data;
        }
    }
}
