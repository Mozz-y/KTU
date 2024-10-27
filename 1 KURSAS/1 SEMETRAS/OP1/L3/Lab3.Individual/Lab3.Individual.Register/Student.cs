namespace Lab3.Exercises.Register
{
    /// <summary>
    /// Class representing a student.
    /// </summary>
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Group { get; set; }
        public int[] Grades { get; set; }

        public Student(string lastName, string firstName, string group, int[] grades)
        {
            LastName = lastName;
            FirstName = firstName;
            Group = group;
            Grades = grades;
        }

        /// <summary>
        /// Calculates the average of the grades.
        /// </summary>
        public double Average
        {
            get
            {
                if (Grades.Length == 0)
                    return 0;
                double sum = 0;
                foreach (int grade in Grades)
                {
                    sum += grade;
                }
                return sum / Grades.Length;
            }
        }
    }
}
