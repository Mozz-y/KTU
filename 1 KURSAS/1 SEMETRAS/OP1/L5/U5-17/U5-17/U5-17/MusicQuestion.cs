namespace U5_17
{
    public class MusicQuestion : Question
    {
        public string FileName { get; set; }

        public override string GetAdditionalInfo()
        {
            return FileName; // File name for music questions
        }

        public override string ToString()
        {
            return base.ToString(); // Reuse the base class ToString
        }
    }
}
