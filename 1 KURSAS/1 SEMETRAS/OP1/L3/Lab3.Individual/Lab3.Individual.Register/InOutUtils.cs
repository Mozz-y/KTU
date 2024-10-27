using System;
using System.IO;
using System.Text;

namespace Lab3.Exercises.Register
{
    static class InOutUtils
    {
        /// <summary>
        /// Reads students from a file and returns a StudentsContainer.
        /// </summary>
        /// <param name="fileName">The name of the file to read from.</param>
        /// <returns>A container filled with students.</returns>
        public static StudentsContainer ReadStudents(string fileName)
        {
            StudentsContainer students = new StudentsContainer();
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                string lastName = values[0];
                string firstName = values[1];
                string group = values[2];
                int gradesCount = int.Parse(values[3]);
                int[] grades = new int[gradesCount];

                for (int i = 0; i < gradesCount; i++)
                {
                    grades[i] = int.Parse(values[4 + i]);
                }

                Student student = new Student(lastName, firstName, group, grades);
                students.Add(student);
            }
            return students;
        }

        /// <summary>
        /// Prints the students in the container.
        /// </summary>
        /// <param name="label">Label for the printed data.</param>
        /// <param name="students">Container of students to print.</param>
        public static void PrintStudents(string label, StudentsContainer students)
        {
            Console.WriteLine(new string('-', 80));
            Console.WriteLine("| {0,-70} |", label);
            Console.WriteLine(new string('-', 80));
            Console.WriteLine("| {0,-20} | {1,-20} | {2,-10} | {3,-10} |",
                              "Pavardė", "Vardas", "Grupė", "Vidurkis");
            Console.WriteLine(new string('-', 80));

            for (int i = 0; i < students.Count; i++)
            {
                Student student = students.Get(i);
                Console.WriteLine("| {0,-20} | {1,-20} | {2,-10} | {3,-10:F2} |",
                                  student.LastName, student.FirstName, student.Group, student.Average);
            }
            Console.WriteLine(new string('-', 80));
        }
    }
}
