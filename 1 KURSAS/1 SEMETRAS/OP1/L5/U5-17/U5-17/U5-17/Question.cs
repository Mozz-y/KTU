namespace U5_17
{
    public class Question
    {
        public string Theme { get; set; }
        public string Difficulty { get; set; }
        public string Author { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }
        public int Points { get; set; }

        // Method for formatted table output
        public virtual string ToTableRow()
        {
            return $"{Theme,-15} | {Difficulty,-12} | {Author,-10} | {QuestionText,-50} | {CorrectAnswer,-25} | {Points,-10} | {GetAdditionalInfo()}";
        }

        public virtual string GetAdditionalInfo()
        {
            return "-"; // Default value for OPEN questions
        }
    }
}
