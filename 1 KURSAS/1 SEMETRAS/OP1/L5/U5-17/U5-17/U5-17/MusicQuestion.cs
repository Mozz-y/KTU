namespace U5_17
{
    public class MusicQuestion : Question
    {
        public string FileName { get; set; }

        public override string GetAdditionalInfo()
        {
            return FileName; // File name for music questions
        }
    }
}
