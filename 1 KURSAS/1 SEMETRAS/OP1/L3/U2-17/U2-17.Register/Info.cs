using System.Collections.Generic;


namespace U2_17.Register
{
    public class Info
    {
        // Properties to hold question data
        public string Organization { get; set; }
        public string Theme { get; set; }
        public string Difficulty { get; set; }
        public string Author { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public string Correct { get; set; }
        public int Points { get; set; }

        // Constructor to initialize Info object
        public Info(string organization, string theme, string difficulty, string author, string question, List<string> answers, string correctAnswer, int points)
        {
            Organization = organization; // Set the organization name
            Theme = theme;
            Difficulty = difficulty;
            Author = author;
            Question = question;
            Answers = answers;
            Correct = correctAnswer;
            Points = points;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Info other = (Info)obj;

            // Generate a composite key for both objects and compare
            return GenerateCompositeKey() == other.GenerateCompositeKey();
        }

        // Helper method to generate a unique composite key based on all fields
        private string GenerateCompositeKey()
        {
            string answersKey = Answers != null ? string.Join(",", Answers) : "";
            return $"{Organization}|{Theme}|{Difficulty}|{Author}|{Question}|{Correct}|{Points}|{answersKey}";
        }

        public override int GetHashCode()
        {
            return GenerateCompositeKey().GetHashCode();
        }

        public override string ToString()
        {
            // Format the Info object
            return $"Organizacija: {Organization}\n" +
                   $"Tema: {Theme}\n" +
                   $"Sunkumas: {Difficulty}\n" +
                   $"Autorius: {Author}\n" +
                   $"Klausimas: {Question}\n" +
                   $"Atsakymai: {string.Join(", ", Answers)}\n" +
                   $"Teisingas atsakymas: {Correct}\n" +
                   $"Taškai: {Points}\n";
        }

        public static bool operator ==(Info a, string difficulty)
        {
            return a.Difficulty == difficulty;
        }

        public static bool operator !=(Info a, string difficulty)
        {
            return !(a == difficulty);
        }
    }
}
