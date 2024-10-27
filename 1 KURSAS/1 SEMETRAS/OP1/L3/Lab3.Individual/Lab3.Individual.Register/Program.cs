namespace Lab3.Exercises.Register
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read students from the file
            StudentsContainer studentsContainer = InOutUtils.ReadStudents("Students.csv");

            // Print initial data
            InOutUtils.PrintStudents("Pradiniai duomenys", studentsContainer);

            // Sort by average descending and last name ascending
            studentsContainer.Sort();

            // Save sorted results to a file (optional)
            InOutUtils.PrintStudents("Rezultatai", studentsContainer);
        }
    }
}
